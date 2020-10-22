using BiendeoCHLib.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace ComboIndicator.Settings {
	[Serializable]
	public class Config : IGUIConfigurable {
		public int Version;
		public string TweakVersion;
		public bool SilenceUpdates;

		public float ConfigX;
		public float ConfigY;
		public KeyBind ConfigKeyBind;

		public bool NoteStreakEnabled;
		public bool HotStartEnabled;
		public bool StarPowerActiveEnabled;

		public ColorablePositionableLabel Indicator;

		[XmlIgnore]
		public bool ConfigWindowEnabled;
		[XmlIgnore]
		public bool SeenChangelog;
		[XmlIgnore]
		private bool wasMouseVisible;

		public Config() {
			Version = 1;
			TweakVersion = "0.0.0";
			SilenceUpdates = false;

			ConfigX = 300.0f;
			ConfigY = 200.0f;
			ConfigKeyBind = new KeyBind {
				Key = KeyCode.F8,
				Ctrl = true,
				Alt = false,
				Shift = true
			};

			NoteStreakEnabled = true;
			HotStartEnabled = true;
			StarPowerActiveEnabled = true;

			Indicator = new ColorablePositionableLabel {
				Visible = true,
				X = 0,
				Y = 0,
				Size = 100,
				Bold = true,
				Italic = false,
				Alignment = TextAnchor.MiddleCenter,
				Color = new ColorARGB(Color.white)
			};
		}

		public static Config LoadConfig(string configPath) {
			var configFilePath = new FileInfo(configPath);
			if (configFilePath.Exists) {
				var configString = File.ReadAllText(configFilePath.FullName);
				var serializer = new XmlSerializer(typeof(Config));
				using (var configIn = new MemoryStream(Encoding.Unicode.GetBytes(configString))) {
					return serializer.Deserialize(configIn) as Config;
				}
			} else {
				var c = new Config();
				c.SaveConfig(configPath);
				return c;
			}
		}

		public void ReloadConfig(string configPath) {
			var configFilePath = new FileInfo(configPath);
			if (configFilePath.Exists) {
				var configString = File.ReadAllText(configFilePath.FullName);
				var serializer = new XmlSerializer(typeof(Config));
				using (var configIn = new MemoryStream(Encoding.Unicode.GetBytes(configString))) {
					var newConfig = serializer.Deserialize(configIn) as Config;
					ConfigKeyBind = newConfig.ConfigKeyBind;
					NoteStreakEnabled = newConfig.NoteStreakEnabled;
					HotStartEnabled = newConfig.HotStartEnabled;
					StarPowerActiveEnabled = newConfig.StarPowerActiveEnabled;
					Indicator = newConfig.Indicator;
				}
			}
		}

		public void SaveConfig(string configPath) {
			var configFilePath = new FileInfo(configPath);
			var serializer = new XmlSerializer(typeof(Config));
			using (var configOut = configFilePath.Open(FileMode.Create)) {
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
			GUILayout.Space(25.0f);
			GUILayout.Label("Configuration Keybind", styles.LargeLabel);
			ConfigKeyBind.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Enable Features", styles.LargeLabel);
			NoteStreakEnabled = GUILayout.Toggle(NoteStreakEnabled, "Note Streak", styles.Toggle);
			HotStartEnabled = GUILayout.Toggle(HotStartEnabled, "Hot Start", styles.Toggle);
			StarPowerActiveEnabled = GUILayout.Toggle(StarPowerActiveEnabled, "Star Power Active", styles.Toggle);
			GUILayout.Label("Prevents the labels from fading out", new GUIStyle(styles.SmallLabel) {
				fontStyle = FontStyle.Italic
			});

			if (GUILayout.Button("Test Indicator", styles.Button)) {
				ComboIndicator.Instance.CreateDancingText("Here is a test!", true);
			}

			GUILayout.Space(25.0f);
			Indicator.ConfigureGUI(styles);
		}
	}
}
