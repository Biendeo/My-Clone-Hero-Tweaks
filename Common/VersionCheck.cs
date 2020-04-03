﻿using Common.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Common {
	internal class VersionCheck {
		private int windowId;
		private Rect updateWindowRect;
		private string latestVersion;

		private GUIStyle labelStyle;
		private GUIStyle buttonStyle;

		public bool HasVersionBeenChecked { get; private set; }
		public bool IsShowingUpdateWindow { get; private set; }

		public VersionCheck(int windowId) {
			this.windowId = windowId;
			updateWindowRect = new Rect(Screen.width / 2 - 150.0f, Screen.height / 2 - 100.0f, 300.0f, 200.0f);
			HasVersionBeenChecked = false;
			IsShowingUpdateWindow = false;
			labelStyle = null;
			buttonStyle = null;
		}

		public void CheckVersion(string intendedVersion) {
			using (var wc = new WebClient()) {
				string versionsText = wc.DownloadString("https://raw.githubusercontent.com/Biendeo/My-Clone-Hero-Tweaks/master/versions.txt");
				var versions = versionsText.Split('\n').Select(l => l.Split('='));
				latestVersion = versions.Single(l => l[0] == intendedVersion)[1];

				HasVersionBeenChecked = true;
				IsShowingUpdateWindow = latestVersion != string.Join(".", FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion.Split('.').Take(3));
			}
		}

		public void DrawUpdateWindow(GUIStyle windowStyle, GUIStyle labelStyle, GUIStyle buttonStyle) {
			this.labelStyle = labelStyle;
			this.buttonStyle = buttonStyle;
			var r = GUILayout.Window(windowId, updateWindowRect, DrawWindow, new GUIContent($"Tweak Updated Required!"), windowStyle);
			updateWindowRect.x = r.x;
			updateWindowRect.y = r.y;
		}

		private void DrawWindow(int id) {
			var largeLabelStyle = new GUIStyle(labelStyle) {
				fontSize = 23,
				alignment = TextAnchor.MiddleCenter,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label($"{new FileInfo(Assembly.GetExecutingAssembly().Location).Name} has an update available!", largeLabelStyle) ;
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
