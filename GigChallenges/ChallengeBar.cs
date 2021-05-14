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
