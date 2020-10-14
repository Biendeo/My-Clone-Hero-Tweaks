using BepInEx.Configuration;
using BiendeoCHLib.Settings;
using BiendeoCHLib.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace BiendeoCHLib {
	public class VersionCheck : MonoBehaviour {
		private readonly int windowId;
		private string assemblyName;
		public string AssemblyVersion;
		private Rect updateWindowRect;
		private static string latestVersion;

		private GUIStyle labelStyle;
		private GUIStyle buttonStyle;
		private GUIStyle windowStyle;

		public ConfigEntry<bool> SilenceUpdates;
		public bool HasVersionBeenChecked;
		public bool IsShowingUpdateWindow;

		public VersionCheck() {
			windowId = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			updateWindowRect = new Rect(Screen.width / 2 - 150.0f, Screen.height / 2 - 100.0f, 300.0f, 200.0f);
			HasVersionBeenChecked = false;
			IsShowingUpdateWindow = false;
			labelStyle = null;
			buttonStyle = null;
			windowStyle = null;
			latestVersion = null;
		}

		public void InitializeSettings(Assembly assembly, ConfigFile config) {
			AssemblyVersion = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
			assemblyName = new FileInfo(assembly.Location).Name;

			SilenceUpdates = config.Bind("VersionCheck", "SilenceUpdates", false, "Whether this mod prompts you for any available updates");
		}

		public void Start() {
			if (!SilenceUpdates.Value && latestVersion == null) {
				string intendedVersion = GlobalVariablesWrapper.Instance.BuildVersion;
				try {
					using (var wc = new WebClient()) {
						string versionsText = wc.DownloadString("https://raw.githubusercontent.com/Biendeo/My-Clone-Hero-Tweaks/master/versions.txt");
						var versions = versionsText.Split('\n').Select(l => l.Split('='));
						latestVersion = versions.Single(l => l[0] == intendedVersion)[1];

					}
				} catch (WebException) {
					// Any WebException could cause an error; since it's not really too vital for the tweak, it's simpler to just not prompt for an update.
				} catch (InvalidOperationException) {
					// This exception is thrown if the CH version isn't found in the versions list. Perhaps it should prompt the user that they're using an unsupported CH version?
				}
			}
			if (latestVersion != null) {
				IsShowingUpdateWindow = latestVersion != string.Join(".", AssemblyVersion.Split('.').Take(3));
			}
			HasVersionBeenChecked = true;
		}

		public void OnGUI() {
			if (labelStyle == null || buttonStyle == null || windowStyle == null) {
				labelStyle = new GUIStyle(GUI.skin.label);
				buttonStyle = new GUIStyle(GUI.skin.button);
				windowStyle = new GUIStyle(GUI.skin.window);
			}
			if (IsShowingUpdateWindow) {
				var r = GUILayout.Window(windowId, updateWindowRect, DrawWindow, new GUIContent($"Tweak Updated Required!"), windowStyle);
				updateWindowRect.x = r.x;
				updateWindowRect.y = r.y;
			}
		}

		private void DrawWindow(int id) {
			var largeLabelStyle = new GUIStyle(labelStyle) {
				fontSize = 23,
				alignment = TextAnchor.MiddleCenter,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label($"{assemblyName} has an update available!", largeLabelStyle) ;
			GUILayout.Label($"Please update to version {latestVersion}!", largeLabelStyle);
			if (GUILayout.Button("Open update page", buttonStyle)) {
				Application.OpenURL($"https://github.com/Biendeo/My-Clone-Hero-Tweaks/releases/tag/v{latestVersion}");
			}
			if (GUILayout.Button("Close this window", buttonStyle)) {
				IsShowingUpdateWindow = false;
			}
			GUI.DragWindow();
		}
	}
}
