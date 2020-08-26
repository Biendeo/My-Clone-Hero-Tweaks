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

namespace PerfectMode.Settings {
	[Serializable]
	public class Config : IGUIConfigurable {
		public int Version;
		public string TweakVersion;
		public bool SilenceUpdates;

		public float ConfigX;
		public float ConfigY;
		public KeyBind ConfigKeyBind;

		public bool Enabled;
		public KeyBind EnabledKeyBind;

		public bool FC;
		public int NotesMissed;

		public float FailDelay;

		public ColorablePositionableLabel DisplayImage;
		public ColorablePositionableLabel RemainingNotesLeft;
		public ColorablePositionableLabel RestartIndicator;

		[XmlIgnore]
		public bool LayoutTest;
		[XmlIgnore]
		private bool DraggableLabelsEnabled;
		[XmlIgnore]
		public bool ConfigWindowEnabled;
		[XmlIgnore]
		public bool SeenChangelog;
		[XmlIgnore]
		private bool wasMouseVisible;

		public Config() {
			Version = 2;
			TweakVersion = "0.0.0";
			SilenceUpdates = false;

			ConfigX = 200.0f;
			ConfigY = 200.0f;
			ConfigKeyBind = new KeyBind {
				Key = KeyCode.F6,
				Ctrl = true,
				Alt = false,
				Shift = true
			};

			Enabled = false;
			EnabledKeyBind = new KeyBind {
				Key = KeyCode.F6,
				Ctrl = false,
				Alt = false,
				Shift = false
			};

			FC = true;
			NotesMissed = 0;
			FailDelay = 2.0f;

			DisplayImage = new ColorablePositionableLabel {
				Visible = true,
				X = (int)(30.0f * Screen.width / 1440.0f),
				Y = (int)(1415.0f * Screen.height / 1440.0f),
				Size = Screen.height * 50 / 1440,
				Bold = true,
				Italic = false,
				Alignment = TextAnchor.MiddleLeft,
				Color = new ColorARGB(Color.white)
			};

			RemainingNotesLeft = new ColorablePositionableLabel {
				Visible = true,
				X = (int)(30.0f * Screen.width / 1440.0f),
				Y = (int)(1365.0f * Screen.height / 1440.0f),
				Size = Screen.height * 40 / 1440,
				Bold = true,
				Italic = true,
				Alignment = TextAnchor.MiddleLeft,
				Color = new ColorARGB(Color.white)
			};

			RestartIndicator = new ColorablePositionableLabel {
				Visible = true,
				X = Screen.width / 2,
				Y = (int)(1200.0f * Screen.height / 1440.0f),
				Size = Screen.height * 50 / 1440,
				Bold = true,
				Italic = false,
				Alignment = TextAnchor.MiddleCenter,
				Color = new ColorARGB(Color.white)
			};
		}

		public Config(OldConfig oldConfig) {
			Version = 2;
			TweakVersion = "0.0.0";
			SilenceUpdates = false;

			ConfigX = oldConfig.ConfigX;
			ConfigY = oldConfig.ConfigY;
			ConfigKeyBind = new KeyBind {
				Key = KeyCode.F6,
				Ctrl = true,
				Alt = false,
				Shift = true
			};

			Enabled = oldConfig.Enabled;
			EnabledKeyBind = new KeyBind {
				Key = KeyCode.F6,
				Ctrl = false,
				Alt = false,
				Shift = false
			};

			FC = oldConfig.FC;
			NotesMissed = oldConfig.NotesMissed;
			FailDelay = oldConfig.FailDelay;

			DisplayImage = new ColorablePositionableLabel {
				Visible = oldConfig.DisplayImage,
				X = (int)oldConfig.DisplayImageX,
				Y = (int)oldConfig.DisplayImageY,
				Size = oldConfig.DisplayImageScale,
				Bold = oldConfig.DisplayImageBold,
				Italic = oldConfig.DisplayImageItalic,
				Alignment = TextAnchor.MiddleLeft,
				Color = new ColorARGB(oldConfig.DisplayImageColorARGB)
			};

			RemainingNotesLeft = new ColorablePositionableLabel {
				Visible = oldConfig.RemainingNotesLeft,
				X = (int)oldConfig.RemainingNotesLeftX,
				Y = (int)oldConfig.RemainingNotesLeftY,
				Size = oldConfig.RemainingNotesLeftScale,
				Bold = oldConfig.RemainingNotesLeftBold,
				Italic = oldConfig.RemainingNotesLeftItalic,
				Alignment = TextAnchor.MiddleLeft,
				Color = new ColorARGB(oldConfig.RemainingNotesLeftColorARGB)
			};

			RestartIndicator = new ColorablePositionableLabel {
				Visible = true,
				X = Screen.width / 2,
				Y = (int)(1360.0f * Screen.height / 1440.0f),
				Size = Screen.height * 50 / 1440,
				Bold = true,
				Italic = false,
				Alignment = TextAnchor.MiddleCenter,
				Color = new ColorARGB(Color.white)
			};
		}

		public static Config LoadConfig() {
			var configFilePath = new FileInfo(Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "ExtraSongUIConfig.xml"));
			if (configFilePath.Exists) {
				// Determine if it's the old version. Without a version field, this is slightly tricky.
				var configString = File.ReadAllText(configFilePath.FullName);
				if (configString.Contains("<DisplayImageBold>")) {
					configString = configString.Replace("<Config xmlns:", "<OldConfig xmlns:");
					configString = configString.Replace("</Config>", "</OldConfig>");
					var oldSerializer = new XmlSerializer(typeof(OldConfig));
					using (var oldConfigIn = new MemoryStream(Encoding.Unicode.GetBytes(configString))) {
						var oldConfig = oldSerializer.Deserialize(oldConfigIn) as OldConfig;
						var newConfig = new Config(oldConfig);
						newConfig.SaveConfig();
						return newConfig;
					}
				} else {
					var serializer = new XmlSerializer(typeof(Config));
					using (var configIn = new MemoryStream(Encoding.Unicode.GetBytes(configString))) {
						return serializer.Deserialize(configIn) as Config;
					}
				}
			} else {
				var c = new Config();
				c.SaveConfig();
				return c;
			}
		}

		public void SaveConfig() {
			var configFilePath = new FileInfo(Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "ExtraSongUIConfig.xml"));
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
			if (EnabledKeyBind.IsPressed && !EnabledKeyBind.JustSet) {
				Enabled = !Enabled;
			}
			ConfigKeyBind.JustSet = false;
			EnabledKeyBind.JustSet = false;
		}

		public void DrawLabelWindows() {
			if (DraggableLabelsEnabled) {
				DisplayImage.DrawLabelWindow(187001002);
				RemainingNotesLeft.DrawLabelWindow(187001003);
				RestartIndicator.DrawLabelWindow(187001004);
			}
		}

		public void ConfigureGUI(GUIConfigurationStyles styles) {
			GUILayout.Label("Settings", styles.LargeLabel);
			Enabled = GUILayout.Toggle(Enabled, "Enabled", styles.Toggle);
			FC = GUILayout.Toggle(FC, "FC Mode", styles.Toggle);
			GUILayout.Label("Note Miss Limit (inclusive)", styles.SmallLabel);
			NotesMissed = (int)GUILayout.HorizontalSlider(NotesMissed, 0.0f, 100.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (int.TryParse(GUILayout.TextField(NotesMissed.ToString(), styles.TextField), out int notesMissed)) NotesMissed = notesMissed;
			GUILayout.Label("Fail Delay Before Restart", styles.SmallLabel);
			FailDelay = GUILayout.HorizontalSlider(FailDelay, 0.0f, 10.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(FailDelay.ToString(), styles.TextField), out float failDelay)) FailDelay = failDelay;

			GUILayout.Space(25.0f);
			GUILayout.Label("Enable/Disable Keybind", styles.LargeLabel);
			EnabledKeyBind.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Configuration Keybind", styles.LargeLabel);
			ConfigKeyBind.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Debugging Tools", styles.LargeLabel);
			LayoutTest = GUILayout.Toggle(LayoutTest, "Test Layout", styles.Toggle);
			GUILayout.Label("Prevents the labels from fading out", new GUIStyle(styles.SmallLabel) {
				fontStyle = FontStyle.Italic
			});
			DraggableLabelsEnabled = GUILayout.Toggle(DraggableLabelsEnabled, "Draggable Labels", styles.Toggle);
			GUILayout.Label("Allows you to drag each label with a window", new GUIStyle(styles.SmallLabel) {
				fontStyle = FontStyle.Italic
			});
			GUILayout.Label("(this disables some options in this window)", new GUIStyle(styles.SmallLabel) {
				fontStyle = FontStyle.Italic
			});
			DisplayImage.DraggableWindowsEnabled = DraggableLabelsEnabled;
			RemainingNotesLeft.DraggableWindowsEnabled = DraggableLabelsEnabled;
			RestartIndicator.DraggableWindowsEnabled = DraggableLabelsEnabled;

			GUILayout.Space(25.0f);
			GUILayout.Label("Active Display Indicator", styles.LargeLabel);
			DisplayImage.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Remaining Notes Indicator", styles.LargeLabel);
			RemainingNotesLeft.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Restart Time Indicator", styles.LargeLabel);
			RestartIndicator.ConfigureGUI(styles);


			GUILayout.Space(25.0f);
			if (GUILayout.Button("Save Config", styles.Button)) {
				SaveConfig();
			}
		}
	}
}
