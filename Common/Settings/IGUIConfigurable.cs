using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Settings {
	interface IGUIConfigurable {
		void ConfigureGUI(GUIConfigurationStyles styles);
	}
}
