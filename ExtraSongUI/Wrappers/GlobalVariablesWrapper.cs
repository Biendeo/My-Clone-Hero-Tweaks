using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraSongUI.Wrappers {
	internal class GlobalVariablesWrapper : WrapperBase {
		public readonly GlobalVariables globalVariables;

		public GlobalVariablesWrapper(GlobalVariables globalVariables) {
			InitializeSingletonFields();
			this.globalVariables = globalVariables;
		}

		private static void InitializeSingletonFields() {

		}
	}
}
