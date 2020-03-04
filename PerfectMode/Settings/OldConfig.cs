using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PerfectMode.Settings {
	[Serializable]
	public class OldConfig {
		public float ConfigX;
		public float ConfigY;

		public bool Enabled;

		public bool FC;
		public int NotesMissed;

		public float FailDelay;

		public bool DisplayImage;
		public float DisplayImageX;
		public float DisplayImageY;
		public int DisplayImageScale;
		public uint DisplayImageColorARGB;
		public bool DisplayImageBold;
		public bool DisplayImageItalic;

		public bool RemainingNotesLeft;
		public float RemainingNotesLeftX;
		public float RemainingNotesLeftY;
		public int RemainingNotesLeftScale;
		public uint RemainingNotesLeftColorARGB;
		public bool RemainingNotesLeftBold;
		public bool RemainingNotesLeftItalic;

		public OldConfig() {
			ConfigX = 200.0f;
			ConfigY = 200.0f;
			Enabled = false;
			FC = true;
			NotesMissed = 0;
			FailDelay = 2.0f;

			DisplayImage = true;
			DisplayImageX = 30.0f;
			DisplayImageY = Screen.height - 80.0f;
			DisplayImageBold = true;
			DisplayImageItalic = false;
			DisplayImageColorARGB = OldConfig.ColorToARGB(Color.white);
			DisplayImageScale = 50;

			RemainingNotesLeft = true;
			RemainingNotesLeftX = 30.0f;
			RemainingNotesLeftY = Screen.height - 130.0f;
			RemainingNotesLeftBold = false;
			RemainingNotesLeftItalic = true;
			RemainingNotesLeftColorARGB = OldConfig.ColorToARGB(Color.white);
			RemainingNotesLeftScale = 40;
		}

		public static uint ColorToARGB(Color color) => (((uint)(color.a * 255.0f) & 0xFF) << 24) | (((uint)(color.r * 255.0f) & 0xFF) << 16) | (((uint)(color.g * 255.0f) & 0xFF) << 8) | ((uint)(color.b * 255.0f) & 0xFF);

		public static Color ARGBToColor(uint color) => new Color(((color >> 16) & 0xFF) / 255.0f, ((color >> 8) & 0xFF) / 255.0f, (color & 0xFF) / 255.0f, ((color >> 24) & 0xFF) / 255.0f);
	}
}
