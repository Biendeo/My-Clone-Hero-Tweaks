using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Common.Settings {
	[Serializable]
	public class ColorablePositionableLabel : PositionableLabel {
		public ColorARGB Color;

		public override void ConfigureGUI(GUIConfigurationStyles styles) {
			base.ConfigureGUI(styles);
			Color.ConfigureGUI(styles);
		}

		public override GUIStyle Style => new GUIStyle {
			fontSize = Size,
			alignment = Alignment,
			fontStyle = (Bold ? FontStyle.Bold : FontStyle.Normal) | (Italic ? FontStyle.Italic : FontStyle.Normal),
			normal = new GUIStyleState {
				textColor = Color.Color
			}
		};
	}
}
