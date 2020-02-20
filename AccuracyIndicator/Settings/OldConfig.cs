using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AccuracyIndicator.Settings {
	[Serializable]
	public class OldConfig {
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

		public uint ColorPerfectARGB;
		public uint ColorSlightlyEarlyARGB;
		public uint ColorEarlyARGB;
		public uint ColorVeryEarlyARGB;
		public uint ColorSlightlyLateARGB;
		public uint ColorLateARGB;
		public uint ColorVeryLateARGB;
		public uint ColorMissedARGB;

		public bool AccuracyTime;
		public float AccuracyTimeX;
		public float AccuracyTimeY;
		public int AccuracyTimeScale;
		public bool AccuracyTimeBold;
		public bool AccuracyTimeItalic;

		public bool AccuracyMessage;
		public float AccuracyMessageX;
		public float AccuracyMessageY;
		public int AccuracyMessageScale;
		public bool AccuracyMessageBold;
		public bool AccuracyMessageItalic;

		public bool AverageAccuracy;
		public float AverageAccuracyX;
		public float AverageAccuracyY;
		public int AverageAccuracyScale;
		public bool AverageAccuracyBold;
		public bool AverageAccuracyItalic;


		public OldConfig() {
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

			ColorPerfectARGB = ColorToARGB(Color.white);
			ColorSlightlyEarlyARGB = ColorToARGB(Color.green);
			ColorEarlyARGB = ColorToARGB(Color.yellow);
			ColorVeryEarlyARGB = ColorToARGB(Color.red);
			ColorSlightlyLateARGB = ColorToARGB(Color.green);
			ColorLateARGB = ColorToARGB(Color.yellow);
			ColorVeryLateARGB = ColorToARGB(Color.red);
			ColorMissedARGB = ColorToARGB(Color.grey);

			AccuracyTime = true;
			AccuracyTimeX = Screen.width * 0.8f;
			AccuracyTimeY = Screen.height * (1270.0f / 1440.0f);
			AccuracyTimeScale = Screen.height * 50 / 1440;
			AccuracyTimeBold = true;
			AccuracyTimeItalic = false;

			AccuracyMessage = true;
			AccuracyMessageX = Screen.width * 0.8f;
			AccuracyMessageY = Screen.height * (1330.0f / 1440.0f);
			AccuracyMessageScale = Screen.height * 50 / 1440;
			AccuracyMessageBold = true;
			AccuracyMessageItalic = false;

			AverageAccuracy = false;
			AverageAccuracyX = Screen.width * 0.8f;
			AverageAccuracyY = Screen.height * (1390.0f / 1440.0f);
			AverageAccuracyScale = Screen.height * 50 / 1440;
			AverageAccuracyBold = true;
			AverageAccuracyItalic = false;

		}

		public static uint ColorToARGB(Color color) => (((uint)(color.a * 255.0f) & 0xFF) << 24) | (((uint)(color.r * 255.0f) & 0xFF) << 16) | (((uint)(color.g * 255.0f) & 0xFF) << 8) | ((uint)(color.b * 255.0f) & 0xFF);

		public static Color ARGBToColor(uint color) => new Color(((color >> 16) & 0xFF) / 255.0f, ((color >> 8) & 0xFF) / 255.0f, (color & 0xFF) / 255.0f, ((color >> 24) & 0xFF) / 255.0f);
	}
}
