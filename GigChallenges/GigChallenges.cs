using BepInEx;
using BepInEx.Logging;
using BiendeoCHLib;
using BiendeoCHLib.Patches;
using BiendeoCHLib.Patches.Attributes;
using BiendeoCHLib.Wrappers;
using GigChallenges.Challenges;
using GigChallenges.Interfaces;
using HarmonyLib;
using Rewired;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GigChallenges {
	[HarmonyCHPatch(typeof(GameManagerWrapper), nameof(GameManagerWrapper.OnControllerDisconnected))]
	public class OnControllerDisconnectedHandler {
		[HarmonyCHPostfix]
		static void Postfix(ControllerStatusChangedEventArgs __0) {
			if (!__0.controller.isConnected) {
				GigChallenges.InstanceLogger.LogDebug("DING!");
			}
		}
	}

	[HarmonyCHPatch(typeof(HealthContainerWrapper), nameof(HealthContainerWrapper.Start))]
	public class TemporaryHealthContainerLatch {
		[HarmonyCHPostfix]
		static void Postfix() {
			GigChallenges.Instance.SetupHealthBar();
		}
	}

	[BepInPlugin("com.biendeo.gigchallenges", "Gig Challenges", "1.5.1")]
	[BepInDependency("com.biendeo.biendeochlib")]
	public class GigChallenges : BaseUnityPlugin {
		public static GigChallenges Instance { get; private set; }
		public static ManualLogSource InstanceLogger => Instance.Logger;

		private bool sceneChanged;
		private bool isGameplay;
		private bool hasSetupChallenge;

		private VersionCheck versionCheck;
		private Rect changelogRect;

		private Harmony Harmony;

		private IChallenge activeChallenge;

		private HealthContainerWrapper healthContainer;
		private SPBarWrapper spBar;
		private ChallengeBar challengeBar;

		public GigChallenges() {
			Instance = this;
			Harmony = new Harmony("com.biendeo.gigchallenges");
			PatchBase.InitializePatches(Harmony, Assembly.GetExecutingAssembly(), Logger);
		}

		public void Start() {
			SceneManager.activeSceneChanged += delegate (Scene _, Scene destination) {
				sceneChanged = true;
				isGameplay = destination.name == "Gameplay";
				hasSetupChallenge = false;
			};
		}

		public void Awake() {
			versionCheck = gameObject.AddComponent<VersionCheck>();
			versionCheck.InitializeSettings(Assembly.GetExecutingAssembly(), Config);
		}

		public void LateUpdate() {
			if (sceneChanged) {
				sceneChanged = false;
				if (isGameplay) {
					var gameManager = GameManagerWrapper.Wrap(GameObject.Find("Game Manager").GetComponent<GameManager>());
					var challengeFactory = new ChallengeFactory();
					activeChallenge = challengeFactory.CreateChallenge(gameManager);
					Logger.LogDebug(activeChallenge.ChallengeGoal);
					healthContainer = gameManager.BasePlayers[0].HealthContainer;
					healthContainer.CastToMonoBehaviour().gameObject.SetActive(true);
					Logger.LogDebug("Updated!");

					var clonedSPBar = Instantiate(GameObject.Find("SPBar"));
					clonedSPBar.transform.localPosition = new Vector3(0.016f, 0.0f, 5.0f);
					clonedSPBar.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
					clonedSPBar.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
					Destroy(clonedSPBar.transform.GetChild(4).gameObject);
					clonedSPBar.transform.GetChild(6).GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.5f, 0.6f);
					clonedSPBar.transform.GetChild(7).GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.5f, 0.6f);
					spBar = SPBarWrapper.Wrap(clonedSPBar.GetComponent<SPBar>());
					challengeBar = ChallengeBar.InstantiatePrefab(GameObject.Find("SPBar").transform.parent);
					
				} else {
					activeChallenge = null;
				}
			}
			if (isGameplay && hasSetupChallenge) {
				activeChallenge.Update();
				//Logger.LogDebug($"Just before I run SetState, I have value {Mathf.Clamp(activeChallenge.PercentageToGold, 0.00001f, 1.0f)}, last {healthContainer.LastHealth}, RYT {healthContainer.RedYellowThreshold}, YGT {healthContainer.YellowGreenThreshold}");
				healthContainer.SetState(activeChallenge.PercentageToGold);
				spBar.SetFill(activeChallenge.PercentageToBronze, false);
			}
		}
		
		public void SetupHealthBar() {
			healthContainer.LastHealth = activeChallenge.PercentageOfGoldIsSilver;
			healthContainer.RedYellowThreshold = activeChallenge.PercentageOfGoldIsBronze;
			healthContainer.YellowGreenThreshold = activeChallenge.PercentageOfGoldIsSilver;
			Logger.LogDebug("Setup!");
			hasSetupChallenge = true;
		}
	}
}
