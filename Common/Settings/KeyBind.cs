using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Common.Settings {
	[Serializable]
	public class KeyBind : IGUIConfigurable {
		public KeyCode Key;
		public bool Shift;
		public bool Ctrl;
		public bool Alt;

		[XmlIgnore]
		private bool WaitingForKey;
		[XmlIgnore]
		public bool JustSet;

		public bool IsPressed => Input.GetKeyDown(Key) && (Shift == (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) && (Ctrl == (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) && (Alt == (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)));

		public void ConfigureGUI(GUIConfigurationStyles styles) {
			if (WaitingForKey) {
				foreach (KeyCode key in Enum.GetValues(typeof(KeyCode))) {
					if (Input.GetKeyDown(key)) {
						if (key != KeyCode.Escape) {
							Key = key;
							JustSet = true;
						}
						WaitingForKey = false;
						break;
					}
				}
			}
			WaitingForKey |= GUILayout.Button(WaitingForKey ? "Press key to bind or ESC to cancel" : $"Keybind: {Key.ToString()}", styles.Button);
			Shift = GUILayout.Toggle(Shift, "Shift", styles.Toggle);
			Ctrl = GUILayout.Toggle(Ctrl, "Ctrl", styles.Toggle);
			Alt = GUILayout.Toggle(Alt, "Alt", styles.Toggle);
		}
	}
}
