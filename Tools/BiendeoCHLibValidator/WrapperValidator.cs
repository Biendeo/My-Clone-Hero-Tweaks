using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLibValidator {
	/// <summary>
	/// Validates the wrapper bound fields.
	/// </summary>
	class WrapperValidator : IValidator {
		private ManualLogSource logger;
		private bool valid;

		public WrapperValidator(ManualLogSource logger) {
			this.logger = logger;
			valid = true;
		}

		public bool AssertWorkingDirectory() {
			return true;
		}

		public bool Validate() {
			return valid;
		}
	}
}
