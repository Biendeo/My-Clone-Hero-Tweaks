using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Common.Settings {
	[Serializable]
	public class PositionableLabel : IGUIConfigurable {
		public float X;
		public float Y;
		public int Size;
		public TextAnchor Alignment;
		public bool Bold;
		public bool Italic;
		public bool Visible;

		public void ConfigureGUI(GUIConfigurationStyles styles) {
			GUILayout.Label("X", styles.SmallLabel);
			X = GUILayout.HorizontalSlider(X, -3840.0f, 3840.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(X.ToString(), styles.TextField), out float x)) X = x;
			GUILayout.Label("Y", styles.SmallLabel);
			Y = GUILayout.HorizontalSlider(Y, -2160, 2160.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (float.TryParse(GUILayout.TextField(Y.ToString(), styles.TextField), out float y)) Y = y;
			GUILayout.Label("Size", styles.SmallLabel);
			Size = (int)GUILayout.HorizontalSlider(Size, 0.0f, 500.0f, styles.HorizontalSlider, styles.HorizontalSliderThumb);
			if (int.TryParse(GUILayout.TextField(Size.ToString(), styles.TextField), out int size)) Size = size;
			if (GUILayout.Button($"Alignment: {Alignment.ToString()}", styles.Button)) {
				Alignment = (TextAnchor)((int)(Alignment + 1) % 9);
			}
			Bold = GUILayout.Toggle(Bold, "Bold", styles.Toggle);
			Italic = GUILayout.Toggle(Italic, "Italic", styles.Toggle);
			Visible = GUILayout.Toggle(Visible, "Visible", styles.Toggle);
		}

		public Rect Rect => new Rect(X, Y, 0.1f, 0.1f);

		public GUIStyle Style => new GUIStyle {
			fontSize = Size,
			alignment = Alignment,
			fontStyle = (Bold ? FontStyle.Bold : FontStyle.Normal) | (Italic ? FontStyle.Italic : FontStyle.Normal)
		};
	}
}
