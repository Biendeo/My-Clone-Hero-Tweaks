using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrappers {
	[Wrapper(typeof(GlobalVariables))]
	internal class GlobalVariablesWrapper : WrapperBase {
		public readonly GlobalVariables globalVariables;

		public GlobalVariablesWrapper(GlobalVariables globalVariables) {
			this.globalVariables = globalVariables;
		}
	}
}
