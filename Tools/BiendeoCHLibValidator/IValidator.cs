using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLibValidator {
	interface IValidator {
		bool AssertWorkingDirectory();
		bool Validate();
	}
}
