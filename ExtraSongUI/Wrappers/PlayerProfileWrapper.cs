using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtraSongUI.Wrappers {
	internal class PlayerProfileWrapper : WrapperBase {
		public readonly object playerProfile;
		public static Type PlayerProfileType;

		public string PlayerName => (string)playerNameField.GetValue(playerProfile);
		private static FieldInfo playerNameField;
		private const string playerNameFieldName = "\u031A\u0311\u0312\u030D\u0315\u0310\u0311\u0316\u030D\u0311\u0312";

		public sbyte Instrument => (sbyte)instrumentField.GetValue(playerProfile);
		private static FieldInfo instrumentField;
		private const string instrumentFieldName = "\u030E\u031C\u0314\u031B\u030E\u031A\u0313\u030D\u0314\u0310\u031A";

		public sbyte Difficulty => (sbyte)difficultyField.GetValue(playerProfile);
		private static FieldInfo difficultyField;
		private const string difficultyFieldName = "\u030E\u0310\u0312\u031C\u0314\u031A\u030E\u031A\u0312\u0318\u030E";

		public PlayerProfileWrapper(object playerProfile) {
			this.playerProfile = playerProfile;
		}

		public static void InitializeSingletonFields() {
			PlayerProfileType = Assembly.Load("Assembly-CSharp.dll").GetType("\u0311\u0316\u0315\u031B\u0310\u0314\u0316\u030E\u0311\u0315\u031B");
			RegisterField(ref playerNameField, PlayerProfileType, playerNameFieldName);
			RegisterField(ref instrumentField, PlayerProfileType, instrumentFieldName);
			RegisterField(ref difficultyField, PlayerProfileType, difficultyFieldName);
		}
	}
}
