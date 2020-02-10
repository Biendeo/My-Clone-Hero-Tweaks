using Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PerfectMode
{
	public class Loader {
		public void LoadTweak() {
			WrapperBase.InitializeLoaders();
			if (this.gameObject != null) {
				return;
			}
			this.gameObject = new GameObject(string.Empty, new Type[]
			{
				typeof(PerfectMode)
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
