using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtraSongUI.Wrappers {
	internal class SongWrapper : WrapperBase {
		//! \u031B\u0317\u0310\u0316\u0319\u0313\u0312\u0316\u0313\u031B\u0310

		public readonly object playerProfile;
		public static Type PlayerProfileType => Assembly.Load("Assembly-CSharp.dll").GetType("̛̛̖̖̑̐̔̎̑̕̕");
	}
}
