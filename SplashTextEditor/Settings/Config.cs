using Common.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SplashTextEditor.Settings {
	[Serializable]
	public class Config {
		public int Version;
		public string TweakVersion;
		public bool SilenceUpdates;

		public float ConfigX;
		public float ConfigY;
		public KeyBind ConfigKeyBind;

		public bool Enabled;
		public bool VanillaSplashMessages;
		public bool DragonforceOverride;
		public bool AprilFoolsSplashes;
		public float CycleTime;

		public List<string> Messages;

		[XmlIgnore]
		public bool ConfigWindowEnabled;
		[XmlIgnore]
		public bool SeenChangelog;
		[XmlIgnore]
		public Action ResetSplashes;
		[XmlIgnore]
		public Action<int> InsertMessage;
		[XmlIgnore]
		public Action<int> DeleteMessage;
		[XmlIgnore]
		public Action<int> ShiftUpMessage;
		[XmlIgnore]
		public Action<int> ShiftDownMessage;
		[XmlIgnore]
		public Action<int> PreviewMessage;
		[XmlIgnore]
		private bool wasMouseVisible;

		public Config() {
			Version = 1;
			TweakVersion = "0.0.0";
			SilenceUpdates = false;

			ConfigX = 200.0f;
			ConfigY = 200.0f;
			ConfigKeyBind = new KeyBind {
				Key = KeyCode.F9,
				Ctrl = true,
				Alt = false,
				Shift = true
			};

			Enabled = true;
			VanillaSplashMessages = true;
			DragonforceOverride = true;
			AprilFoolsSplashes = true;
			CycleTime = 15.0f;

			Messages = new List<string>();
			
			ConfigWindowEnabled = false;
			SeenChangelog = false;
		}

		public static Config LoadConfig() {
			var configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "SplashTextEditorConfig.xml"));
			if (configFilePath.Exists) {
				var configString = File.ReadAllText(configFilePath.FullName);
				var serializer = new XmlSerializer(typeof(Config));
				using (var configIn = new MemoryStream(Encoding.Unicode.GetBytes(configString))) {
					return serializer.Deserialize(configIn) as Config;
				}
			} else {
				var c = new Config();
				c.SaveConfig();
				return c;
			}
		}

		public void ReloadConfig() {
			var configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "SplashTextEditorConfig.xml"));
			if (configFilePath.Exists) {
				var configString = File.ReadAllText(configFilePath.FullName);
				var serializer = new XmlSerializer(typeof(Config));
				using (var configIn = new MemoryStream(Encoding.Unicode.GetBytes(configString))) {
					var newConfig = serializer.Deserialize(configIn) as Config;
					ConfigKeyBind = newConfig.ConfigKeyBind;
					Enabled = newConfig.Enabled;
					VanillaSplashMessages = newConfig.VanillaSplashMessages;
					DragonforceOverride = newConfig.DragonforceOverride;
					AprilFoolsSplashes = newConfig.AprilFoolsSplashes;
					CycleTime = 15.0f;
					Messages = newConfig.Messages;
					ResetSplashes();
				}
			}
		}

		public void SaveConfig() {
			var configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "SplashTextEditorConfig.xml"));
			if (configFilePath.Exists) configFilePath.Delete();
			var serializer = new XmlSerializer(typeof(Config));
			using (var configOut = configFilePath.OpenWrite()) {
				serializer.Serialize(configOut, this);
			}
		}

		public void HandleInput() {
			if (ConfigKeyBind.IsPressed && !ConfigKeyBind.JustSet) {
				ConfigWindowEnabled = !ConfigWindowEnabled;
				if (ConfigWindowEnabled) {
					wasMouseVisible = Cursor.visible;
					Cursor.visible = true;
				} else {
					if (!wasMouseVisible) Cursor.visible = false;
				}
			}
			ConfigKeyBind.JustSet = false;
		}

		public void ConfigureGUI(GUIConfigurationStyles styles) {
			var noteLabelStyle = styles.SmallLabel;
			noteLabelStyle.fontStyle = FontStyle.Italic;

			GUILayout.Label("Settings", styles.LargeLabel);
			Enabled = GUILayout.Toggle(Enabled, "Enabled", styles.Toggle);
			VanillaSplashMessages = GUILayout.Toggle(VanillaSplashMessages, "Vanilla Splash Messages", styles.Toggle);
			DragonforceOverride = GUILayout.Toggle(Enabled, "DragonForce Override", styles.Toggle);
			AprilFoolsSplashes = GUILayout.Toggle(Enabled, "April Fools Splashes", styles.Toggle);
			GUILayout.Label("Cycle Time", styles.SmallLabel);
			CycleTime = (float)GUILayout.HorizontalSlider(CycleTime, 0.016f, 120.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(CycleTime.ToString(), styles.TextField), out float cycleTime)) CycleTime = cycleTime;
			if (GUILayout.Button("Randomly select new splash")) {
				ResetSplashes();
			}

			GUILayout.Space(25.0f);
			GUILayout.Label("Configuration Keybind", styles.LargeLabel);
			ConfigKeyBind.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Splash Messages", styles.LargeLabel);
			GUILayout.Label("Note: Please drop out any user profiles\nbefore typing into these text areas as\ninput will be passed to the background.", noteLabelStyle);
			GUILayout.Space(15.0f);
			bool isMainMenuActive = SceneManager.GetActiveScene().name == "Main Menu";
			for (int i = 0; i < Messages.Count; ++i) {
				GUILayout.Label($"#{i + 1}", styles.SmallLabel);
				Messages[i] = GUILayout.TextArea(Messages[i], styles.TextArea);
				if (isMainMenuActive) {
					if (GUILayout.Button("Preview message")) {
						PreviewMessage(i);
					}
				}
				GUILayout.BeginHorizontal();
				if (i > 0 && GUILayout.Button("Shift up")) {
					string temp = Messages[i - 1];
					Messages[i - 1] = Messages[i];
					Messages[i] = temp;
					ShiftUpMessage(i);
				}
				if (i < Messages.Count - 1 && GUILayout.Button("Shift down")) {
					string temp = Messages[i + 1];
					Messages[i + 1] = Messages[i];
					Messages[i] = temp;
					ShiftDownMessage(i);
				}
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				if (GUILayout.Button("Delete")) {
					Messages.RemoveAt(i);
					DeleteMessage(i--);
				}
				if (GUILayout.Button("Insert new")) {
					Messages.Insert(i + 1, "Type your splash message here!");
					InsertMessage(i);
				}
				GUILayout.EndHorizontal();
				GUILayout.Space(15.0f);
			}
			if (Messages.Count == 0) {
				if (GUILayout.Button("Insert")) {
					Messages.Add("Type your splash message here!");
					InsertMessage(-1);
				}
			}

			GUILayout.Space(25.0f);
			if (GUILayout.Button("Reload Config", styles.Button)) {
				ReloadConfig();
			}
			if (GUILayout.Button("Save Config", styles.Button)) {
				SaveConfig();
			}
		}
	}
}
