using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Settings {
	[Serializable]
	public class OldConfig {
		public EditableLabelSettings TimeName;
		public FormattedLabelSettings SongTime;
		public FormattedLabelSettings SongLength;

		public FormattedLabelSettings CurrentStarProgressName;
		public FormattedLabelSettings CurrentStarProgressScore;
		public FormattedLabelSettings CurrentStarProgressEndScore;
		public FormattedLabelSettings CurrentStarProgressPercentage;

		public FormattedLabelSettings SevenStarProgressName;
		public FormattedLabelSettings SevenStarProgressScore;
		public FormattedLabelSettings SevenStarProgressEndScore;
		public FormattedLabelSettings SevenStarProgressPercentage;

		public EditableLabelSettings NotesName;
		public FormattedLabelSettings NotesHitCounter;
		public FormattedLabelSettings NotesPassedCounter;
		public FormattedLabelSettings TotalNotesCounter;
		public FormattedLabelSettings NotesHitPercentage;
		public FormattedLabelSettings NotesMissedCounter;

		public EditableLabelSettings StarPowerName;
		public FormattedLabelSettings StarPowersGottenCounter;
		public FormattedLabelSettings TotalStarPowersCounter;
		public FormattedLabelSettings StarPowerPercentage;

		public EditableLabelSettings ComboName;
		public FormattedLabelSettings CurrentComboCounter;
		public FormattedLabelSettings HighestComboCounter;

		public float ConfigX;
		public float ConfigY;

		public bool HideAll;

		public OldConfig() {
			// These original numbers were designed with 1440p in mind so this'll sort it out.
			float widthScale = Screen.width / 2560.0f;
			float heightScale = Screen.height / 1440.0f;
			int smallFontSize = (int)(30 * widthScale);
			int largeFontSize = (int)(50 * widthScale);
			int extraLargeFontSize = (int)(150 * widthScale);

			TimeName = new EditableLabelSettings {
				X = 100.0f * widthScale,
				Y = 750.0f * heightScale,
				Size = smallFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Content = "Time:",
			};
			SongTime = new FormattedLabelSettings {
				X = 400.0f * widthScale,
				Y = 750.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0} /"
			};
			SongLength = new FormattedLabelSettings {
				X = 670.0f * widthScale,
				Y = 750.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0}"
			};
			CurrentStarProgressName = new FormattedLabelSettings {
				X = 100.0f * widthScale,
				Y = 810.0f * heightScale,
				Size = smallFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0} → {1}:"
			};
			CurrentStarProgressScore = new FormattedLabelSettings {
				X = 400.0f * widthScale,
				Y = 810.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0} /"
			};
			CurrentStarProgressEndScore = new FormattedLabelSettings {
				X = 670.0f * widthScale,
				Y = 810.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0}"
			};
			CurrentStarProgressPercentage = new FormattedLabelSettings {
				X = 700.0f * widthScale,
				Y = 810.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerLeft,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "({0}%)"
			};
			SevenStarProgressName = new FormattedLabelSettings {
				X = 100.0f * widthScale,
				Y = 870.0f * heightScale,
				Size = smallFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0} → {1}:"
			};
			SevenStarProgressScore = new FormattedLabelSettings {
				X = 400.0f * widthScale,
				Y = 870.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0} /"
			};
			SevenStarProgressEndScore = new FormattedLabelSettings {
				X = 670.0f * widthScale,
				Y = 870.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0}"
			};
			SevenStarProgressPercentage = new FormattedLabelSettings {
				X = 700.0f * widthScale,
				Y = 870.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerLeft,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "({0}%)"
			};
			NotesName = new EditableLabelSettings {
				X = 100.0f * widthScale,
				Y = 930.0f * heightScale,
				Size = smallFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Content = "Notes:"
			};
			NotesHitCounter = new FormattedLabelSettings {
				X = 330.0f * widthScale,
				Y = 930.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0} /"
			};
			NotesPassedCounter = new FormattedLabelSettings {
				X = 530.0f * widthScale,
				Y = 930.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0} /"
			};
			TotalNotesCounter = new FormattedLabelSettings {
				X = 680.0f * widthScale,
				Y = 930.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0}"
			};
			NotesHitPercentage = new FormattedLabelSettings {
				X = 700.0f * widthScale,
				Y = 930.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerLeft,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "({0}%)"
			};
			NotesMissedCounter = new FormattedLabelSettings {
				X = 780.0f * widthScale,
				Y = 1070.0f * heightScale,
				Size = extraLargeFontSize,
				Alignment = TextAnchor.MiddleRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0}"
			};
			StarPowerName = new EditableLabelSettings {
				X = 100.0f * widthScale,
				Y = 990.0f * heightScale,
				Size = smallFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Content = "SP:"
			};
			StarPowersGottenCounter = new FormattedLabelSettings {
				X = 330.0f * widthScale,
				Y = 990.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0} /"
			};
			TotalStarPowersCounter = new FormattedLabelSettings {
				X = 510.0f * widthScale,
				Y = 990.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0}"
			};
			StarPowerPercentage = new FormattedLabelSettings {
				X = 700.0f * widthScale,
				Y = 990.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerLeft,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "({0}%)"
			};
			ComboName = new EditableLabelSettings {
				X = 100.0f * widthScale,
				Y = 1050.0f * heightScale,
				Size = smallFontSize / 5 * 4,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Content = "Combo:"
			};
			CurrentComboCounter = new FormattedLabelSettings {
				X = 330.0f * widthScale,
				Y = 1050.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0} /"
			};
			HighestComboCounter = new FormattedLabelSettings {
				X = 510.0f * widthScale,
				Y = 1050.0f * heightScale,
				Size = largeFontSize,
				Alignment = TextAnchor.LowerRight,
				Bold = true,
				Italic = false,
				ColorARGB = LabelSettings.ColorToARGB(Color.white),
				Visible = true,
				Format = "{0}"
			};
			ConfigX = Screen.width - 350.0f;
			ConfigY = 100.0f * heightScale;
			HideAll = false;
		}
	}
}
