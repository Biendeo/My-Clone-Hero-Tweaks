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

namespace ExtraSongUI.Settings {
	[Serializable]
	public class Config {
		public int Version;
		public string TweakVersion;
		public bool SilenceUpdates;

		public float ConfigX;
		public float ConfigY;
		public KeyBind ConfigKeyBind;

		public bool Enabled;
		public KeyBind EnabledKeyBind;

		public List<SongUILabel> Layout;

		[XmlIgnore]
		public bool DraggableLabelsEnabled;
		[XmlIgnore]
		public bool ConfigWindowEnabled;
		[XmlIgnore]
		public bool SeenChangelog;
		[XmlIgnore]
		private bool wasMouseVisible;

		public Config() {

			Version = 3;
			TweakVersion = "0.0.0";

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

			Layout = new List<SongUILabel>();
		}

		private static List<SongUILabel> DefaultLayout {
			get {
				// These original numbers were designed with 1440p in mind so this'll sort it out.
				float widthScale = Screen.width / 2560.0f;
				float heightScale = Screen.height / 1440.0f;
				int smallFontSize = (int)(30 * widthScale);
				int largeFontSize = (int)(50 * widthScale);
				int extraLargeFontSize = (int)(150 * widthScale);

				return new List<SongUILabel>() {
					new SongUILabel {
						Name = "Time Name",
						Format = "Time:",
						X = (int)(60.0f * widthScale),
						Y = (int)(750.0f * heightScale),
						Size = smallFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Song Time",
						Format = @"{songtime:m\:ss\.fff} /",
						X = (int)(350.0f * widthScale),
						Y = (int)(750.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Song Length",
						Format = @"{songlength:m\:ss\.fff}",
						X = (int)(620.0f * widthScale),
						Y = (int)(750.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Song Time Percentage",
						Format = "({songtimepercentage:0.00}%)",
						X = (int)(750.0f * widthScale),
						Y = (int)(750.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerLeft,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Current Star Progress Name",
						Format = "{currentstar} → {nextstar}:",
						X = (int)(60.0f * widthScale),
						Y = (int)(810.0f * heightScale),
						Size = smallFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Current Star Progress Score",
						Format = "{currentstarscore} /",
						X = (int)(350.0f * widthScale),
						Y = (int)(810.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Current Star Progress End Score",
						Format = "{nextstarscore}",
						X = (int)(620.0f * widthScale),
						Y = (int)(810.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Current Star Progress Percentage",
						Format = "({currentstarpercentage:0.00}%)",
						X = (int)(750.0f * widthScale),
						Y = (int)(810.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerLeft,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Seven Star Progress Name",
						Format = "0 → 7:",
						X = (int)(60.0f * widthScale),
						Y = (int)(870.0f * heightScale),
						Size = smallFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Seven Star Progress Score",
						Format = "{currentscore} /",
						X = (int)(350.0f * widthScale),
						Y = (int)(870.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Seven Star Progress End Score",
						Format = "{sevenstarscore}",
						X = (int)(620.0f * widthScale),
						Y = (int)(870.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Seven Star Progress Percentage",
						Format = "({sevenstarpercentage:0.00}%)",
						X = (int)(750.0f * widthScale),
						Y = (int)(870.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerLeft,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Notes Name",
						Format = "Notes:",
						X = (int)(60.0f * widthScale),
						Y = (int)(930.0f * heightScale),
						Size = smallFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Notes Hit Counter",
						Format = "{hitnotes} /",
						X = (int)(280.0f * widthScale),
						Y = (int)(930.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Notes Passed Counter",
						Format = "{seennotes} /",
						X = (int)(470.0f * widthScale),
						Y = (int)(930.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Total Notes Counter",
						Format = "{totalnotes}",
						X = (int)(640.0f * widthScale),
						Y = (int)(930.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Notes Hit Percentage",
						Format = "({hitnotespercentage:0.00}%)",
						X = (int)(750.0f * widthScale),
						Y = (int)(930.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerLeft,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Notes Missed Counter",
						Format = "{fcindicator}",
						X = (int)(750.0f * widthScale),
						Y = (int)(1100.0f * heightScale),
						Size = extraLargeFontSize,
						Alignment = TextAnchor.MiddleRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Star Power Name",
						Format = "SP:",
						X = (int)(60.0f * widthScale),
						Y = (int)(990.0f * heightScale),
						Size = smallFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Star Powers Gotten Counter",
						Format = "{starpowersgotten} /",
						X = (int)(280.0f * widthScale),
						Y = (int)(990.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Total Star Powers Counter",
						Format = "{totalstarpowers}",
						X = (int)(430.0f * widthScale),
						Y = (int)(990.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Star Power Percentage",
						Format = "({starpowerpercentage:0.00}%)",
						X = (int)(750.0f * widthScale),
						Y = (int)(990.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerLeft,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Current Star Power",
						Format = "({currentstarpower:0.00}%)",
						X = (int)(1825.0f * widthScale),
						Y = (int)(1125.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerLeft,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Combo Name",
						Format = "Combo:",
						X = (int)(60.0f * widthScale),
						Y = (int)(1050.0f * heightScale),
						Size = smallFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Current Combo Counter",
						Format = "{currentcombo} /",
						X = (int)(280.0f * widthScale),
						Y = (int)(1050.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					},
					new SongUILabel {
						Name = "Highest Combo Counter",
						Format = "{highestcombo}",
						X = (int)(430.0f * widthScale),
						Y = (int)(1050.0f * heightScale),
						Size = largeFontSize,
						Alignment = TextAnchor.LowerRight,
						Bold = true,
						Italic = false,
						Visible = true,
						Color = new ColorARGB(Color.white)
					}
				};
			}
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
				c.Layout = DefaultLayout;
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
					Enabled = newConfig.Enabled;
					EnabledKeyBind = newConfig.EnabledKeyBind;
					Layout = newConfig.Layout;
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
			if (EnabledKeyBind.IsPressed && !EnabledKeyBind.JustSet) {
				Enabled = !Enabled;
			}
			ConfigKeyBind.JustSet = false;
			EnabledKeyBind.JustSet = false;
		}

		public void DrawLabelWindows() {
			if (DraggableLabelsEnabled) {
				foreach (var label in Layout) {
					label.DrawLabelWindow(label.WindowId);
				}
			}
		}
	}
}
