using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace CloneHeroTest {
	public class Loader {
		public void LoadTweak() {
			File.WriteAllText(@"%temp%\poop.txt", $"poop!\n{DateTime.Now.ToString()}");
			if (this.gameObject != null) {
				return;
			}
			this.gameObject = new GameObject(string.Empty, new Type[]
			{
				typeof(Class1)
			});
			UnityEngine.Object.DontDestroyOnLoad(this.gameObject);
			this.gameObject.SetActive(true);
		}

		public void UnloadTweak() {
			if (this.gameObject != null) {
				UnityEngine.Object.DestroyImmediate(this.gameObject);
				this.gameObject = null;
			}
		}

		private GameObject gameObject;
	}
}
