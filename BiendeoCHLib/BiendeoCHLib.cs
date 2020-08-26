using BepInEx;
using BepInEx.Configuration;
using BiendeoCHLib.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib {
	[BepInPlugin("com.biendeo.biendeochlib", "Biendeo CH Lib", "1.5.0.0")]
	public class BiendeoCHLib : BaseUnityPlugin {
		public static BiendeoCHLib Instance { get; private set; }

		private readonly VersionCheck versionCheck;

		public BiendeoCHLib() {
			Instance = this;
			WrapperBase.InitializeWrappers(Logger);
			versionCheck = gameObject.AddComponent<VersionCheck>();
			versionCheck.InitializeSettings(Assembly.GetExecutingAssembly(), Config);
		}
	}
}
