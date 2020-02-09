using ExtraSongUI.Wrappers;
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
			InitializeLoaders();
			if (this.gameObject != null) {
				return;
			}
			this.gameObject = new GameObject(string.Empty, new Type[]
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

		private void InitializeLoaders() {
			//TODO: If there was a neat way to do this in one go with reflection instead of manually listing every type, sweet. Otherwise, darn.
			BasePlayerWrapper.InitializeSingletonFields();
			CHPlayerWrapper.InitializeSingletonFields();
			FrameRateWrapper.InitializeSingletonFields();
			GameManagerWrapper.InitializeSingletonFields();
			GlobalVariablesWrapper.InitializeSingletonFields();
			MoonChartWrapper.InitializeSingletonFields();
			//MoonNoteWrapper.InitializeSingletonFields();
			NoteWrapper.InitializeSingletonFields();
			PlayerProfileWrapper.InitializeSingletonFields();
			PracticeUIWrapper.InitializeSingletonFields();
			SongWrapper.InitializeSingletonFields();
			StarPowerWrapper.InitializeSingletonFields();
			StarProgressWrapper.InitializeSingletonFields();
		}

		private GameObject gameObject;
	}
}
