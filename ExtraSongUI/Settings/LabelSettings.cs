using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Settings {
	[Serializable]
	public class LabelSettings {
		public float X;
		public float Y;
		public int Size;
		public TextAnchor Alignment;
		public bool Bold;
		public bool Italic;
		public uint ColorARGB;
		public bool Visible;

		public Rect Rect => new Rect(X, Y, 0.1f, 0.1f);
		public GUIStyle Style(Font font) => new GUIStyle {
			font = font,
			fontSize = Size,
			fontStyle = (Bold ? FontStyle.Bold : FontStyle.Normal) | (Italic ? FontStyle.Italic : FontStyle.Normal),
			alignment = Alignment,
			normal = new GUIStyleState {
				textColor = ARGBToColor(ColorARGB)
			}
		};

		public static uint ColorToARGB(Color color) => (((uint)(color.a * 255.0f) & 0xFF) << 24) | (((uint)(color.r * 255.0f) & 0xFF) << 16) | (((uint)(color.g * 255.0f) & 0xFF) << 8) | ((uint)(color.b * 255.0f) & 0xFF);

		public static Color ARGBToColor(uint color) => new Color(((color >> 16) & 0xFF) / 255.0f, ((color >> 8) & 0xFF) / 255.0f, (color & 0xFF) / 255.0f, ((color >> 24) & 0xFF) / 255.0f);
	}
}
