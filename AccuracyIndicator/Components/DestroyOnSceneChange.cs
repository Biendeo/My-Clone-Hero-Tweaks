using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AccuracyIndicator.Components {
	public class DestroyOnSceneChange : MonoBehaviour {
		private bool sceneChanged;

		void Start() {
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		void Update() {
			if (sceneChanged) {
				Destroy(gameObject);
			}
		}
	}
}
