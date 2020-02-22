using Common.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace AccuracyIndicator.Settings {
	[Serializable]
	public class Config : IGUIConfigurable {
		public int Version;

		public float ConfigX;
		public float ConfigY;

		public bool Enabled;
		public bool LayoutTest;

		public float TimeOnScreen;

		public float CutoffSlightlyEarly;
		public float CutoffEarly;
		public float CutoffVeryEarly;
		public float CutoffSlightlyLate;
		public float CutoffLate;
		public float CutoffVeryLate;

		public ColorARGB ColorPerfect;
		public ColorARGB ColorSlightlyEarly;
		public ColorARGB ColorEarly;
		public ColorARGB ColorVeryEarly;
		public ColorARGB ColorSlightlyLate;
		public ColorARGB ColorLate;
		public ColorARGB ColorVeryLate;
		public ColorARGB ColorMissed;

		public PositionableLabel AccuracyTime;
		public PositionableLabel AccuracyMessage;
		public PositionableLabel AverageAccuracy;


		public Config() {
			Version = 2;

			ConfigX = 300.0f;
			ConfigY = 200.0f;

			Enabled = true;
			LayoutTest = false;

			TimeOnScreen = 0.75f;

			CutoffSlightlyEarly = 0.01f;
			CutoffEarly = 0.02f;
			CutoffVeryEarly = 0.03f;
			CutoffSlightlyLate = 0.01f;
			CutoffLate = 0.02f;
			CutoffVeryLate = 0.03f;

			//TODO: Blues and reds are probably better.
			ColorPerfect = new ColorARGB(Color.white);
			ColorSlightlyEarly = new ColorARGB(Color.green);
			ColorEarly = new ColorARGB(Color.yellow);
			ColorVeryEarly = new ColorARGB(Color.red);
			ColorSlightlyLate = new ColorARGB(Color.green);
			ColorLate = new ColorARGB(Color.yellow);
			ColorVeryLate = new ColorARGB(Color.red);
			ColorMissed = new ColorARGB(Color.grey);

			AccuracyTime = new PositionableLabel {
				Visible = true,
				X = (int)(Screen.width * 0.8f),
				Y = (int)(Screen.height * (1270.0f / 1440.0f)),
				Size = Screen.height * 50 / 1440,
				Bold = true,
				Italic = false,
				Alignment = TextAnchor.MiddleCenter
			};

			AccuracyMessage = new PositionableLabel {
				Visible = true,
				X = (int)(Screen.width * 0.8f),
				Y = (int)(Screen.height * (1330.0f / 1440.0f)),
				Size = Screen.height * 50 / 1440,
				Bold = true,
				Italic = false,
				Alignment = TextAnchor.MiddleCenter
			};

			AverageAccuracy = new PositionableLabel {
				Visible = false,
				X = (int)(Screen.width * 0.8f),
				Y = (int)(Screen.height * (1390.0f / 1440.0f)),
				Size = Screen.height * 50 / 1440,
				Bold = true,
				Italic = false,
				Alignment = TextAnchor.MiddleCenter
			};
		}

		public Config(OldConfig oldConfig) {
			Version = 2;

			ConfigX = oldConfig.ConfigX;
			ConfigY = oldConfig.ConfigY;

			Enabled = oldConfig.Enabled;
			LayoutTest = oldConfig.LayoutTest;

			TimeOnScreen = oldConfig.TimeOnScreen;

			CutoffSlightlyEarly = oldConfig.CutoffSlightlyEarly;
			CutoffEarly = oldConfig.CutoffEarly;
			CutoffVeryEarly = oldConfig.CutoffVeryEarly;
			CutoffSlightlyLate = oldConfig.CutoffSlightlyLate;
			CutoffLate = oldConfig.CutoffLate;
			CutoffVeryLate = oldConfig.CutoffVeryLate;

			ColorPerfect = new ColorARGB(oldConfig.ColorPerfectARGB);
			ColorSlightlyEarly = new ColorARGB(oldConfig.ColorSlightlyEarlyARGB);
			ColorEarly = new ColorARGB(oldConfig.ColorEarlyARGB);
			ColorVeryEarly = new ColorARGB(oldConfig.ColorVeryEarlyARGB);
			ColorSlightlyLate = new ColorARGB(oldConfig.ColorSlightlyLateARGB);
			ColorLate = new ColorARGB(oldConfig.ColorLateARGB);
			ColorVeryLate = new ColorARGB(oldConfig.ColorVeryLateARGB);
			ColorMissed = new ColorARGB(oldConfig.ColorMissedARGB);

			AccuracyTime = new PositionableLabel {
				Visible = oldConfig.AccuracyTime,
				X = (int)oldConfig.AccuracyTimeX,
				Y = (int)oldConfig.AccuracyTimeY,
				Size = oldConfig.AccuracyTimeScale,
				Bold = oldConfig.AccuracyTimeBold,
				Italic = oldConfig.AccuracyTimeItalic,
				Alignment = TextAnchor.MiddleCenter
			};

			AccuracyMessage = new PositionableLabel {
				Visible = oldConfig.AccuracyMessage,
				X = (int)oldConfig.AccuracyMessageX,
				Y = (int)oldConfig.AccuracyMessageY,
				Size = oldConfig.AccuracyMessageScale,
				Bold = oldConfig.AccuracyMessageBold,
				Italic = oldConfig.AccuracyMessageItalic,
				Alignment = TextAnchor.MiddleCenter
			};

			AverageAccuracy = new PositionableLabel {
				Visible = oldConfig.AverageAccuracy,
				X = (int)oldConfig.AverageAccuracyX,
				Y = (int)oldConfig.AverageAccuracyY,
				Size = oldConfig.AverageAccuracyScale,
				Bold = oldConfig.AverageAccuracyBold,
				Italic = oldConfig.AverageAccuracyItalic,
				Alignment = TextAnchor.MiddleCenter
			};
		}

		public static Config LoadConfig() {
			var configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "AccuracyIndicatorConfig.xml"));
			if (configFilePath.Exists) {
				// Determine if it's the old version. Without a version field, this is slightly tricky.
				var configString = File.ReadAllText(configFilePath.FullName);
				if (configString.Contains("<ColorPerfectARGB>")) {
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
			var configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "AccuracyIndicatorConfig.xml"));
			if (configFilePath.Exists) configFilePath.Delete();
			var serializer = new XmlSerializer(typeof(Config));
			using (var configOut = configFilePath.OpenWrite()) {
				serializer.Serialize(configOut, this);
			}
		}

		public void DrawLabelWindows() {
			AccuracyTime.DrawLabelWindow(100002);
			AccuracyMessage.DrawLabelWindow(100003);
			AverageAccuracy.DrawLabelWindow(100004);
		}

		public void ConfigureGUI(GUIConfigurationStyles styles) {
			GUILayout.Label("Settings", styles.LargeLabel);
			Enabled = GUILayout.Toggle(Enabled, "Enabled", styles.Toggle);
			LayoutTest = GUILayout.Toggle(LayoutTest, "Test Layout (for helping you)", styles.Toggle);
			GUILayout.Label("Time on-screen", styles.SmallLabel);
			TimeOnScreen = GUILayout.HorizontalSlider(TimeOnScreen, 0.0f, 5.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(TimeOnScreen.ToString(), styles.TextField), out float timeOnScreen)) TimeOnScreen = timeOnScreen;

			GUILayout.Space(25.0f);
			GUILayout.Label("Accuracy Time Indicator", styles.LargeLabel);
			AccuracyTime.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Accuracy Message Indicator", styles.LargeLabel);
			AccuracyMessage.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Average Accuracy Indicator", styles.LargeLabel);
			GUILayout.Label("(this is mostly for testing latency)", new GUIStyle(styles.SmallLabel) {
				fontStyle = FontStyle.Italic
			});
			AverageAccuracy.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Perfect", styles.LargeLabel);
			ColorPerfect.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Missed", styles.LargeLabel);
			ColorMissed.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Slightly Early", styles.LargeLabel);
			GUILayout.Label("Cutoff time", styles.SmallLabel);
			CutoffSlightlyEarly = GUILayout.HorizontalSlider(CutoffSlightlyEarly, 0.0f, 0.07f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(CutoffSlightlyEarly.ToString(), styles.TextField), out float cutoffSlightlyEarly)) CutoffSlightlyEarly = cutoffSlightlyEarly;
			ColorSlightlyEarly.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Early", styles.LargeLabel);
			GUILayout.Label("Cutoff time", styles.SmallLabel);
			CutoffEarly = GUILayout.HorizontalSlider(CutoffEarly, 0.0f, 0.07f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(CutoffEarly.ToString(), styles.TextField), out float cutoffEarly)) CutoffEarly = cutoffEarly;
			ColorEarly.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Very Early", styles.LargeLabel);
			GUILayout.Label("Cutoff time", styles.SmallLabel);
			CutoffVeryEarly = GUILayout.HorizontalSlider(CutoffVeryEarly, 0.0f, 0.07f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(CutoffVeryEarly.ToString(), styles.TextField), out float cutoffVeryEarly)) CutoffVeryEarly = cutoffVeryEarly;
			ColorVeryEarly.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Slightly Late", styles.LargeLabel);
			GUILayout.Label("Cutoff time", styles.SmallLabel);
			CutoffSlightlyLate = GUILayout.HorizontalSlider(CutoffSlightlyLate, 0.0f, 0.07f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(CutoffSlightlyLate.ToString(), styles.TextField), out float cutoffSlightlyLate)) CutoffSlightlyLate = cutoffSlightlyLate;
			ColorSlightlyLate.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Late", styles.LargeLabel);
			GUILayout.Label("Cutoff time", styles.SmallLabel);
			CutoffLate = GUILayout.HorizontalSlider(CutoffLate, 0.0f, 0.07f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(CutoffLate.ToString(), styles.TextField), out float cutoffLate)) CutoffLate = cutoffLate;
			ColorLate.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Very Late", styles.LargeLabel);
			GUILayout.Label("Cutoff time", styles.SmallLabel);
			CutoffVeryLate = GUILayout.HorizontalSlider(CutoffVeryLate, 0.0f, 0.07f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(CutoffVeryLate.ToString(), styles.TextField), out float cutoffVeryLate)) CutoffVeryLate = cutoffVeryLate;
			ColorVeryLate.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			if (GUILayout.Button("Save Config", styles.Button)) {
				SaveConfig();
			}
		}
	}
}
