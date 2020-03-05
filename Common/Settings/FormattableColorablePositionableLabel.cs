using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Common.Settings {
	[Serializable]
	public class FormattableColorablePositionableLabel : ColorablePositionableLabel {
		public string Format;

		public override void ConfigureGUI(GUIConfigurationStyles styles) {
			GUILayout.Label("Format:", styles.SmallLabel);
			Format = GUILayout.TextField(Format, styles.TextField);
			base.ConfigureGUI(styles);
		}
	}
}
