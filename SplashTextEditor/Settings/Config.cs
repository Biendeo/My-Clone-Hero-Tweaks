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

		public List<string> Messages;

		[XmlIgnore]
		public bool ConfigWindowEnabled;
		[XmlIgnore]
		public bool SeenChangelog;
		[XmlIgnore]
		public Action<string> ChangeActiveTextFunction;
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
			GUILayout.Label("Settings", styles.LargeLabel);
			Enabled = GUILayout.Toggle(Enabled, "Enabled", styles.Toggle);

			GUILayout.Space(25.0f);
			GUILayout.Label("Configuration Keybind", styles.LargeLabel);
			ConfigKeyBind.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Splash Messages", styles.LargeLabel);
			bool isMainMenuActive = SceneManager.GetActiveScene().name == "Main Menu";
			for (int i = 0; i < Messages.Count; ++i) {
				GUILayout.Label($"#{i + 1}", styles.SmallLabel);
				Messages[i] = GUILayout.TextField(Messages[i], styles.TextField);
				if (isMainMenuActive) {
					if (GUILayout.Button("Preview message")) {
						ChangeActiveTextFunction(Messages[i]);
					}
				}
				GUILayout.BeginHorizontal();
				if (i > 0 && GUILayout.Button("Shift up")) {
					string temp = Messages[i - 1];
					Messages[i - 1] = Messages[i];
					Messages[i] = temp;
				}
				if (i < Messages.Count - 1 && GUILayout.Button("Shift down")) {
					string temp = Messages[i + 1];
					Messages[i + 1] = Messages[i];
					Messages[i] = temp;
				}
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				if (GUILayout.Button("Delete")) {
					Messages.RemoveAt(i);
					--i;
				}
				if (GUILayout.Button("Insert new")) {
					Messages.Insert(i + 1, "Type your splash message here!");
				}
				GUILayout.EndHorizontal();
				GUILayout.Space(15.0f);
			}
			if (Messages.Count == 0) {
				if (GUILayout.Button("Insert")) {
					Messages.Add("Type your splash message here!");
				}
			}

			GUILayout.Space(25.0f);
			if (GUILayout.Button("Save Config", styles.Button))
			{
				SaveConfig();
			}
		}
	}
}
