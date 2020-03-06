using Common.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace ExtraSongUI.Settings {
	[Serializable]
	public class Config {
		public int Version;

		public float ConfigX;
		public float ConfigY;
		public KeyBind ConfigKeyBind;

		public bool Enabled;
		public KeyBind EnabledKeyBind;

		public FormattableColorablePositionableLabel TimeName;
		public FormattableColorablePositionableLabel SongTime;
		public FormattableColorablePositionableLabel SongLength;

		public FormattableColorablePositionableLabel CurrentStarProgressName;
		public FormattableColorablePositionableLabel CurrentStarProgressScore;
		public FormattableColorablePositionableLabel CurrentStarProgressEndScore;
		public FormattableColorablePositionableLabel CurrentStarProgressPercentage;

		public FormattableColorablePositionableLabel SevenStarProgressName;
		public FormattableColorablePositionableLabel SevenStarProgressScore;
		public FormattableColorablePositionableLabel SevenStarProgressEndScore;
		public FormattableColorablePositionableLabel SevenStarProgressPercentage;

		public FormattableColorablePositionableLabel NotesName;
		public FormattableColorablePositionableLabel NotesHitCounter;
		public FormattableColorablePositionableLabel NotesPassedCounter;
		public FormattableColorablePositionableLabel TotalNotesCounter;
		public FormattableColorablePositionableLabel NotesHitPercentage;
		public FormattableColorablePositionableLabel NotesMissedCounter;

		public FormattableColorablePositionableLabel StarPowerName;
		public FormattableColorablePositionableLabel StarPowersGottenCounter;
		public FormattableColorablePositionableLabel TotalStarPowersCounter;
		public FormattableColorablePositionableLabel StarPowerPercentage;

		public FormattableColorablePositionableLabel ComboName;
		public FormattableColorablePositionableLabel CurrentComboCounter;
		public FormattableColorablePositionableLabel HighestComboCounter;

		[XmlIgnore]
		public bool DraggableLabelsEnabled;
		[XmlIgnore]
		public bool ConfigWindowEnabled;

		public Config() {
			// These original numbers were designed with 1440p in mind so this'll sort it out.
			float widthScale = Screen.width / 2560.0f;
			float heightScale = Screen.height / 1440.0f;
			int smallFontSize = (int)(30 * widthScale);
			int largeFontSize = (int)(50 * widthScale);
			int extraLargeFontSize = (int)(150 * widthScale);

			Version = 2;

			ConfigX = 400.0f;
			ConfigY = 400.0f;
			ConfigKeyBind = new KeyBind {
				Key = KeyCode.F5,
				Ctrl = true,
				Alt = false,
				Shift = true
			};

			Enabled = true;
			EnabledKeyBind = new KeyBind {
				Key = KeyCode.F5,
				Ctrl = false,
				Alt = false,
				Shift = false
			};

			TimeName = new FormattableColorablePositionableLabel {
				Format = "Time: ",
				X = (int)(100.0f * widthScale),
				Y = (int)(750.0f * heightScale),
				Size = smallFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			SongTime = new FormattableColorablePositionableLabel {
				Format = "{0} /",
				X = (int)(400.0f * widthScale),
				Y = (int)(750.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			SongLength = new FormattableColorablePositionableLabel {
				Format = "{0}",
				X = (int)(670.0f * widthScale),
				Y = (int)(750.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			CurrentStarProgressName = new FormattableColorablePositionableLabel {
				Format = "{0} → {1}:",
				X = (int)(100.0f * widthScale),
				Y = (int)(810.0f * heightScale),
				Size = smallFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			CurrentStarProgressScore = new FormattableColorablePositionableLabel {
				Format = "{0} /",
				X = (int)(400.0f * widthScale),
				Y = (int)(810.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			CurrentStarProgressEndScore = new FormattableColorablePositionableLabel {
				Format = "{0}",
				X = (int)(670.0f * widthScale),
				Y = (int)(810.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			CurrentStarProgressPercentage = new FormattableColorablePositionableLabel {
				Format = "({0}%)",
				X = (int)(780.0f * widthScale),
				Y = (int)(810.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerLeft,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			SevenStarProgressName = new FormattableColorablePositionableLabel {
				Format = "0 → 7:",
				X = (int)(100.0f * widthScale),
				Y = (int)(870.0f * heightScale),
				Size = smallFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			SevenStarProgressScore = new FormattableColorablePositionableLabel {
				Format = "{0} /",
				X = (int)(400.0f * widthScale),
				Y = (int)(870.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			SevenStarProgressEndScore = new FormattableColorablePositionableLabel {
				Format = "{0}",
				X = (int)(670.0f * widthScale),
				Y = (int)(870.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			SevenStarProgressPercentage = new FormattableColorablePositionableLabel {
				Format = "({0}%)",
				X = (int)(780.0f * widthScale),
				Y = (int)(870.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerLeft,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			NotesName = new FormattableColorablePositionableLabel {
				Format = "Notes:",
				X = (int)(100.0f * widthScale),
				Y = (int)(930.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			NotesHitCounter = new FormattableColorablePositionableLabel {
				Format = "{0} /",
				X = (int)(330.0f * widthScale),
				Y = (int)(930.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			NotesPassedCounter = new FormattableColorablePositionableLabel {
				Format = "{0} /",
				X = (int)(530.0f * widthScale),
				Y = (int)(930.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			TotalNotesCounter = new FormattableColorablePositionableLabel {
				Format = "{0}",
				X = (int)(680.0f * widthScale),
				Y = (int)(930.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			NotesHitPercentage = new FormattableColorablePositionableLabel {
				Format = "({0}%)",
				X = (int)(780.0 * widthScale),
				Y = (int)(930.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerLeft,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			NotesMissedCounter = new FormattableColorablePositionableLabel {
				Format = "{0}",
				X = (int)(780.0f * widthScale),
				Y = (int)(1070.0f * heightScale),
				Size = extraLargeFontSize,
				Alignment = TextAnchor.MiddleRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			StarPowerName = new FormattableColorablePositionableLabel {
				Format = "SP:",
				X = (int)(100.0f * widthScale),
				Y = (int)(990.0f * heightScale),
				Size = smallFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			StarPowersGottenCounter = new FormattableColorablePositionableLabel {
				Format = "{0} /",
				X = (int)(330.0f * widthScale),
				Y = (int)(990.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			TotalStarPowersCounter = new FormattableColorablePositionableLabel {
				Format = "{0}",
				X = (int)(510.0f * widthScale),
				Y = (int)(990.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			StarPowerPercentage = new FormattableColorablePositionableLabel {
				Format = "({0}%)",
				X = (int)(700.0f * widthScale),
				Y = (int)(990.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerLeft,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			ComboName = new FormattableColorablePositionableLabel {
				Format = "Combo:",
				X = (int)(100.0f * widthScale),
				Y = (int)(1050.0f * heightScale),
				Size = smallFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			CurrentComboCounter = new FormattableColorablePositionableLabel {
				Format = "{0} /",
				X = (int)(330.0f * widthScale),
				Y = (int)(1050.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};

			HighestComboCounter = new FormattableColorablePositionableLabel {
				Format = "{0}",
				X = (int)(510.0f * widthScale),
				Y = (int)(1050.0f * heightScale),
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				Visible = true,
				Color = new ColorARGB(Color.white)
			};
		}

		private FormattableColorablePositionableLabel NewLabelFromOld(EditableLabelSettings label) {
			return new FormattableColorablePositionableLabel {
				Format = label.Content,
				X = (int)label.X,
				Y = (int)label.Y,
				Size = label.Size,
				Alignment = label.Alignment,
				Bold = label.Bold,
				Italic = label.Italic,
				Visible = label.Visible,
				Color = new ColorARGB(label.ColorARGB)
			};
		}

		private FormattableColorablePositionableLabel NewLabelFromOld(FormattedLabelSettings label) {
			return new FormattableColorablePositionableLabel {
				Format = label.Format,
				X = (int)label.X,
				Y = (int)label.Y,
				Size = label.Size,
				Alignment = label.Alignment,
				Bold = label.Bold,
				Italic = label.Italic,
				Visible = label.Visible,
				Color = new ColorARGB(label.ColorARGB)
			};
		}

		public Config(OldConfig oldConfig) {
			// These original numbers were designed with 1440p in mind so this'll sort it out.
			float widthScale = Screen.width / 2560.0f;
			float heightScale = Screen.height / 1440.0f;
			int smallFontSize = (int)(30 * widthScale);
			int largeFontSize = (int)(50 * widthScale);
			int extraLargeFontSize = (int)(150 * widthScale);

			Version = 2;

			ConfigX = oldConfig.ConfigX;
			ConfigY = oldConfig.ConfigY;
			ConfigKeyBind = new KeyBind {
				Key = KeyCode.F5,
				Ctrl = true,
				Alt = false,
				Shift = true
			};

			Enabled = oldConfig.HideAll;
			EnabledKeyBind = new KeyBind {
				Key = KeyCode.F5,
				Ctrl = false,
				Alt = false,
				Shift = false
			};

			TimeName = NewLabelFromOld(oldConfig.TimeName);
			SongTime = NewLabelFromOld(oldConfig.SongTime);
			SongLength = NewLabelFromOld(oldConfig.SongLength);
			CurrentStarProgressName = NewLabelFromOld(oldConfig.CurrentStarProgressName);
			CurrentStarProgressScore = NewLabelFromOld(oldConfig.CurrentStarProgressScore);
			CurrentStarProgressEndScore = NewLabelFromOld(oldConfig.CurrentStarProgressEndScore);
			CurrentStarProgressPercentage = NewLabelFromOld(oldConfig.CurrentStarProgressPercentage);
			SevenStarProgressName = NewLabelFromOld(oldConfig.SevenStarProgressName);
			SevenStarProgressScore = NewLabelFromOld(oldConfig.SevenStarProgressScore);
			SevenStarProgressEndScore = NewLabelFromOld(oldConfig.SevenStarProgressEndScore);
			SevenStarProgressPercentage = NewLabelFromOld(oldConfig.SevenStarProgressPercentage);
			NotesName = NewLabelFromOld(oldConfig.NotesName);
			NotesHitCounter = NewLabelFromOld(oldConfig.NotesHitCounter);
			NotesPassedCounter = NewLabelFromOld(oldConfig.NotesPassedCounter);
			TotalNotesCounter = NewLabelFromOld(oldConfig.TotalNotesCounter);
			NotesHitPercentage = NewLabelFromOld(oldConfig.NotesHitPercentage);
			NotesMissedCounter = NewLabelFromOld(oldConfig.NotesMissedCounter);
			StarPowerName = NewLabelFromOld(oldConfig.StarPowerName);
			StarPowersGottenCounter = NewLabelFromOld(oldConfig.StarPowersGottenCounter);
			TotalStarPowersCounter = NewLabelFromOld(oldConfig.TotalStarPowersCounter);
			StarPowerPercentage = NewLabelFromOld(oldConfig.StarPowerPercentage);
			ComboName = NewLabelFromOld(oldConfig.ComboName);
			CurrentComboCounter = NewLabelFromOld(oldConfig.CurrentComboCounter);
			HighestComboCounter = NewLabelFromOld(oldConfig.HighestComboCounter);
		}

		public static Config LoadConfig() {
			var configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "ExtraSongUIConfig.xml"));
			if (configFilePath.Exists) {
				// Determine if it's the old version. Without a version field, this is slightly tricky.
				var configString = File.ReadAllText(configFilePath.FullName);
				if (configString.Contains("<HideAll>")) {
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
			var configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "ExtraSongUIConfig.xml"));
			var serializer = new XmlSerializer(typeof(Config));
			using (var configOut = configFilePath.Open(FileMode.Create)) {
				serializer.Serialize(configOut, this);
			}
		}

		public void HandleInput() {
			if (ConfigKeyBind.IsPressed && !ConfigKeyBind.JustSet) {
				ConfigWindowEnabled = !ConfigWindowEnabled;
			}
			if (EnabledKeyBind.IsPressed && !EnabledKeyBind.JustSet) {
				Enabled = !Enabled;
			}
			ConfigKeyBind.JustSet = false;
			EnabledKeyBind.JustSet = false;
		}

		public void DrawLabelWindows() {
			if (DraggableLabelsEnabled) {
				TimeName.DrawLabelWindow(187000002);
				SongTime.DrawLabelWindow(187000003);
				SongLength.DrawLabelWindow(187000004);
				CurrentStarProgressName.DrawLabelWindow(187000005);
				CurrentStarProgressScore.DrawLabelWindow(187000006);
				CurrentStarProgressEndScore.DrawLabelWindow(187000007);
				CurrentStarProgressPercentage.DrawLabelWindow(187000008);
				SevenStarProgressName.DrawLabelWindow(187000009);
				SevenStarProgressScore.DrawLabelWindow(187000010);
				SevenStarProgressEndScore.DrawLabelWindow(187000011);
				SevenStarProgressPercentage.DrawLabelWindow(187000012);
				NotesName.DrawLabelWindow(187000013);
				NotesHitCounter.DrawLabelWindow(187000014);
				NotesPassedCounter.DrawLabelWindow(187000015);
				TotalNotesCounter.DrawLabelWindow(187000016);
				NotesHitPercentage.DrawLabelWindow(187000017);
				NotesMissedCounter.DrawLabelWindow(187000018);
				StarPowerName.DrawLabelWindow(187000019);
				StarPowersGottenCounter.DrawLabelWindow(187000020);
				TotalStarPowersCounter.DrawLabelWindow(187000021);
				StarPowerPercentage.DrawLabelWindow(187000022);
				ComboName.DrawLabelWindow(187000023);
				CurrentComboCounter.DrawLabelWindow(187000024);
				HighestComboCounter.DrawLabelWindow(187000025);
			}
		}
	}
}
