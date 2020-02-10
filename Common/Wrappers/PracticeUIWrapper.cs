using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrappers {
	internal class PracticeUIWrapper : WrapperBase {
		public readonly PracticeUI practiceUI;

		public PracticeUIWrapper(PracticeUI practiceUI) {
			this.practiceUI = practiceUI;
		}

		public static void InitializeSingletonFields() {

		}
	}
}
