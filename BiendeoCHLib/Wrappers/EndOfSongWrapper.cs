using BiendeoCHLib.Wrappers.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("EndOfSong")]
	public struct EndOfSongWrapper {
		public readonly object endOfSong;

		public EndOfSongWrapper(object endOfSong) {
			this.endOfSong = endOfSong;
		}

		public override bool Equals(object obj) => endOfSong.Equals(obj);

		public override int GetHashCode() => endOfSong.GetHashCode();

		#region Constructors

		#endregion

		#region Fields

		// Seems to just refer to an object (probably a prefab) called "BotNoSave".
		// This is in the Main Canvas where all the text elements are drawn.
		// It only has the canvas renderer and the text mesh pro object which indicates either
		// if a bot was running, or if a high score was achieved. The component is disabled if neither.
		public GameObject BotNoSaveObject {
			get => (GameObject)botNoSaveObjectField.GetValue(endOfSong);
			set => botNoSaveObjectField.SetValue(endOfSong, value);
		}
		[WrapperField("\u0316\u0316\u030E\u030D\u0316\u0319\u031A\u0312\u030F\u0316\u031A")]
		private static readonly FieldInfo botNoSaveObjectField;

		// An unused value that starts at 0 and is incremented by Time.deltaTime in Update until it's greater than 1.
		// Probably a precursor to the fade behaviour left in code. No other uses.
		public float FadeFloat {
			get => (float)fadeFloatField.GetValue(endOfSong);
			set => fadeFloatField.SetValue(endOfSong, value);
		}
		[WrapperField("\u0312\u031B\u0317\u030D\u031A\u0318\u0315\u030E\u0310\u0312\u0317")]
		private static readonly FieldInfo fadeFloatField;

		// A true false based on a global variables value.
		public bool SomeBool {
			get => (bool)someBoolField.GetValue(endOfSong);
			set => someBoolField.SetValue(endOfSong, value);
		}
		[WrapperField("\u0314\u030F\u0317\u0311\u0310\u0318\u030F\u031A\u0317\u0317\u0317")]
		private static readonly FieldInfo someBoolField;

		#endregion

		#region Properties

		#endregion

		#region Methods

		#endregion
	}
}
