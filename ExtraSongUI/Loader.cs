using Common.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ExtraSongUI {
	public class Loader {
		public void LoadTweak() {
			WrapperBase.InitializeLoaders();
			if (this.gameObject != null) {
				return;
			}
			this.gameObject = new GameObject("Biendeo Tweak - Extra Song UI", new Type[]
			{
				typeof(SongUI)
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
