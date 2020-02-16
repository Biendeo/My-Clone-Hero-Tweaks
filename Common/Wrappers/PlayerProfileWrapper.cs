using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrappers {
	[Wrapper("\u0311\u0316\u0315\u031B\u0310\u0314\u0316\u030E\u0311\u0315\u031B")]
	internal class PlayerProfileWrapper {
		public readonly object playerProfile;

		public PlayerProfileWrapper(object playerProfile) {
			this.playerProfile = playerProfile;
		}

		public string PlayerName => (string)playerNameField.GetValue(playerProfile);
		[WrapperField("\u031A\u0311\u0312\u030D\u0315\u0310\u0311\u0316\u030D\u0311\u0312")]
		private static readonly FieldInfo playerNameField;

		public ControllerType Instrument => (ControllerType)instrumentField.GetValue(playerProfile);
		[WrapperField("\u030E\u031C\u0314\u031B\u030E\u031A\u0313\u030D\u0314\u0310\u031A")]
		private static readonly FieldInfo instrumentField;

		public sbyte Difficulty => (sbyte)difficultyField.GetValue(playerProfile);
		[WrapperField("\u030E\u0310\u0312\u031C\u0314\u031A\u030E\u031A\u0312\u0318\u030E")]
		private static readonly FieldInfo difficultyField;

		#region Enumerations

		public enum ControllerType : byte {
			Guitar,
			GHLGuitar,
			Drums
		}

		#endregion
	}
}
