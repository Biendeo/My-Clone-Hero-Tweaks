using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Settings {
	[Serializable]
	public class Config {
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

		public float ConfigX;
		public float ConfigY;

		public bool HideAll;

		public Config() {
			TimeName = new EditableLabelSettings();
			SongTime = new FormattedLabelSettings();
			SongLength = new FormattedLabelSettings();
			CurrentStarProgressName = new FormattedLabelSettings();
			CurrentStarProgressScore = new FormattedLabelSettings();
			CurrentStarProgressEndScore = new FormattedLabelSettings();
			CurrentStarProgressPercentage = new FormattedLabelSettings();
			SevenStarProgressName = new FormattedLabelSettings();
			SevenStarProgressScore = new FormattedLabelSettings();
			SevenStarProgressEndScore = new FormattedLabelSettings();
			SevenStarProgressPercentage = new FormattedLabelSettings();
			NotesName = new EditableLabelSettings();
			NotesHitCounter = new FormattedLabelSettings();
			NotesPassedCounter = new FormattedLabelSettings();
			TotalNotesCounter = new FormattedLabelSettings();
			NotesHitPercentage = new FormattedLabelSettings();
			NotesMissedCounter = new FormattedLabelSettings();
			StarPowerName = new EditableLabelSettings();
			StarPowersGottenCounter = new FormattedLabelSettings();
			TotalStarPowersCounter = new FormattedLabelSettings();
			StarPowerPercentage = new FormattedLabelSettings();
		}
	}
}
