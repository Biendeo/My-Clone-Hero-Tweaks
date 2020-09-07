using BiendeoCHLib.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("\u031C\u0317\u030E\u0310\u030F\u0319\u0316\u030D\u0319\u030D\u0314")]
	public struct MoonNoteWrapper {
		public object MoonNote { get; private set; }

		public static MoonNoteWrapper Wrap(object moonNote) => new MoonNoteWrapper {
			MoonNote = moonNote
		};

		public override bool Equals(object obj) => MoonNote.Equals(obj);

		public override int GetHashCode() => MoonNote.GetHashCode();

		public bool IsNull() => MoonNote == null;

		#region Enumerations

		// \u0317\u0312\u031C\u0314\u030E\u0311\u0319\u0313\u0317\u0317\u0317
		public enum FretType {
			Green,
			Red,
			Yellow,
			Blue,
			Orange,
			Open,
			W1,
			W2,
			W3,
			B1,
			B2,
			B3
		}

		// \u031B\u031B\u0316\u030D\u0310\u0318\u0311\u0312\u031B\u0311\u030D
		public enum NoteType {
			Natural,
			Strum,
			Hopo,
			Tap,
			None,
			Open,
			Mirror,
			Shuffle
		}

		// \u0317\u0310\u031B\u030E\u0314\u0313\u0313\u030D\u0311\u0317\u031A
		[Flags]
		public enum Flags {
			None = 0,
			Forced = 1,
			Tap = 2
		}

		#endregion
	}
}
