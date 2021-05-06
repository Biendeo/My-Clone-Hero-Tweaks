using BepInEx;
using BiendeoCHLib;
using BiendeoCHLib.Patches;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GigChallenges {
	[BepInPlugin("com.biendeo.gigchallenges", "Gig Challenges", "1.5.1")]
	[BepInDependency("com.biendeo.biendeochlib")]
	public class GigChallenges : BaseUnityPlugin {
		public static GigChallenges Instance { get; private set; }

		private bool sceneChanged;
		private bool isGameplay;

		private VersionCheck versionCheck;
		private Rect changelogRect;

		private Harmony Harmony;

		public GigChallenges() {
			Instance = this;
			Harmony = new Harmony("com.biendeo.gigchallenges");
			PatchBase.InitializePatches(Harmony, Assembly.GetExecutingAssembly(), Logger);
		}

		public void Start() {
			SceneManager.activeSceneChanged += delegate (Scene _, Scene destination) {
				sceneChanged = true;
				isGameplay = destination.name == "Gameplay";
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

				}
			}
		}
	}
}
