using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PerfectMode.Settings {
	[Serializable]
	public class Config {
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

		public static uint ColorToARGB(Color color) => (((uint)(color.a * 255.0f) & 0xFF) << 24) | (((uint)(color.r * 255.0f) & 0xFF) << 16) | (((uint)(color.g * 255.0f) & 0xFF) << 8) | ((uint)(color.b * 255.0f) & 0xFF);

		public static Color ARGBToColor(uint color) => new Color(((color >> 16) & 0xFF) / 255.0f, ((color >> 8) & 0xFF) / 255.0f, (color & 0xFF) / 255.0f, ((color >> 24) & 0xFF) / 255.0f);
	}
}
