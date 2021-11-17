using GigChallenges.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GigChallenges {
	public class ChallengeBar : MonoBehaviour {
		private static Sprite spContainerSprite;
		private static Sprite barSprite;
		private SpriteRenderer challengeContainer;
		private SpriteRenderer lowerBar;

		private void Start() {
			if (spContainerSprite == null) {
				var go = GameObject.Find("spcontainer");
				if (go != null) {
					spContainerSprite = go.GetComponent<SpriteRenderer>().sprite;
				}
			}
			if (barSprite == null) {
				var go = GameObject.Find("lowerbar");
				if (go != null) {
					barSprite = go.GetComponent<SpriteRenderer>().sprite;
				}
			}

			transform.localPosition = new Vector3(-1.05f, 0.0f, 5.0f);
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			gameObject.layer = 9;

			challengeContainer.transform.localPosition = new Vector3(0.54f, 0.0f, -1.5f);
			challengeContainer.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 334.5f);
			challengeContainer.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			challengeContainer.gameObject.layer = 9;

			challengeContainer.sprite = spContainerSprite;
			challengeContainer.renderingLayerMask = 4294967295;
			challengeContainer.sortingOrder = -500;

			lowerBar.transform.localPosition = new Vector3(0.52f, 0.0f, -1.45f);
			lowerBar.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 333.0f);
			lowerBar.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			lowerBar.gameObject.layer = 9;

			lowerBar.sprite = barSprite;
			lowerBar.renderingLayerMask = 4294967295;
			lowerBar.color = Color.green;
		}

		public void SetState(IChallenge challenge) {
			var functionsToBar = new (Func<float> Func, SpriteRenderer LowerBar)[] {
				(() => challenge.PercentageToBronze, lowerBar)
			};
			foreach (var f in functionsToBar) {
				float fillAmount = f.Func();
				if (fillAmount <= 0.0f) {
					f.LowerBar.size = Vector2.zero;
					GigChallenges.InstanceLogger.LogDebug($"0.75 - 1 ({f.LowerBar.size})");
				} else if (fillAmount >= 0.75f) {
					float num = (fillAmount - 0.75f) * 4.0f;
					f.LowerBar.size = new Vector2(0.04f, 0.196f);
					GigChallenges.InstanceLogger.LogDebug($"0.75 - 1 ({f.LowerBar.size})");
				} else if (fillAmount >= 0.5f) {
					float num = (fillAmount - 0.5f) * 4.0f;
					f.LowerBar.size = new Vector2(0.04f, 0.196f);
					GigChallenges.InstanceLogger.LogDebug($"0.5 - 0.75 ({f.LowerBar.size})");
				} else if (fillAmount >= 0.25f) {
					float num = (fillAmount - 0.25f) * 4.0f;
					f.LowerBar.size = new Vector2(0.04f, 0.091f * num + 0.105f);
					GigChallenges.InstanceLogger.LogDebug($"0.25 - 0.5 ({f.LowerBar.size})");
				} else {
					float num = fillAmount * 4.0f;
					f.LowerBar.size = new Vector2(0.04f, 0.07f * num + 0.035f);
					GigChallenges.InstanceLogger.LogDebug($"0 - 0.25 ({f.LowerBar.size})");
				}
			}
		}

		public static ChallengeBar InstantiatePrefab(Transform parent) {
			var challengeBar = new GameObject("Challenge Bar").AddComponent<ChallengeBar>();
			challengeBar.transform.SetParent(parent);

			challengeBar.challengeContainer = new GameObject("Challenge Container").AddComponent<SpriteRenderer>();
			challengeBar.challengeContainer.transform.SetParent(challengeBar.transform);

			challengeBar.lowerBar = new GameObject("Lower Bar").AddComponent<SpriteRenderer>();
			challengeBar.lowerBar.transform.SetParent(challengeBar.transform);

			return challengeBar;
		}
	}
}
