using BiendeoCHLib.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace ExtraSongUI.Settings {
	public class SongUILabel : FormattableColorablePositionableLabel {
		[XmlIgnore]
		public int WindowId = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
		public string Name;

		public override void ConfigureGUI(GUIConfigurationStyles styles) {
			GUILayout.Label("Name:", styles.SmallLabel);
			Name = GUILayout.TextField(Name, styles.TextField);
			base.ConfigureGUI(styles);
		}
	}
}
