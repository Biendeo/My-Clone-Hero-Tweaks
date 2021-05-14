using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	// \u0311\u0318\u0315\u030D\u0312\u031B\u0313\u030D\u0311\u030F\u0311
	public enum InstrumentType : sbyte {
		None = -1,
		Guitar,
		Bass,
		Rhythm,
		GuitarCoop,
		GHLGuitar,
		GHLBass,
		Drums,
		Keys,
		Band
	}
}
