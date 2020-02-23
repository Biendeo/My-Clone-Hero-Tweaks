using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Common.Settings {
	[Serializable]
	public class ColorARGB : IGUIConfigurable {
		public uint ARGB;

		private ColorARGB() {
			ARGB = 0;
		}

		public ColorARGB(uint argb) {
			ARGB = argb;
		}

		public ColorARGB(Color color) {
			ARGB = ColorToARGB(color);
		}

		public Color Color => ARGBToColor(ARGB);

		public static uint ColorToARGB(Color color) => (((uint)(color.a * 255.0f) & 0xFF) << 24) | (((uint)(color.r * 255.0f) & 0xFF) << 16) | (((uint)(color.g * 255.0f) & 0xFF) << 8) | ((uint)(color.b * 255.0f) & 0xFF);

		public static Color ARGBToColor(uint color) => new Color(((color >> 16) & 0xFF) / 255.0f, ((color >> 8) & 0xFF) / 255.0f, (color & 0xFF) / 255.0f, ((color >> 24) & 0xFF) / 255.0f);

		public void ConfigureGUI(GUIConfigurationStyles styles) {
			var color = ARGBToColor(ARGB);
			GUILayout.Label($"R = {(int)(color.r * 255.0f)}, G = {(int)(color.g * 255.0f)}, B = {(int)(color.b * 255.0f)}, A = {(int)(color.a * 255.0f)}", new GUIStyle(styles.SmallLabel) {
				normal = new GUIStyleState {
					textColor = new Color(color.r, color.g, color.b),
				},
				fontStyle = FontStyle.Bold
			});
			GUILayout.Label("Red", styles.SmallLabel);
			color.r = GUILayout.HorizontalSlider(color.r, 0.0f, 1.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			GUILayout.Label("Green", styles.SmallLabel);
			color.g = GUILayout.HorizontalSlider(color.g, 0.0f, 1.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			GUILayout.Label("Blue", styles.SmallLabel);
			color.b = GUILayout.HorizontalSlider(color.b, 0.0f, 1.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			GUILayout.Label("Alpha", styles.SmallLabel);
			color.a = GUILayout.HorizontalSlider(color.a, 0.0f, 1.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			ARGB = ColorToARGB(color);
		}

	}
}
