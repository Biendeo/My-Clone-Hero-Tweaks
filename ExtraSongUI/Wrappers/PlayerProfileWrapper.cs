using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtraSongUI.Wrappers {
	internal class PlayerProfileWrapper : WrapperBase {
		//! \u0311\u0316\u0315\u031B\u0310\u0314\u0316\u030E\u0311\u0315\u031B
		public readonly object playerProfile;
		public static Type PlayerProfileType => Assembly.Load("Assembly-CSharp.dll").GetType("̛̛̖̖̑̐̔̎̑̕̕");

		public string PlayerName => (string)playerNameField.GetValue(playerProfile);
		private static FieldInfo playerNameField;
		private const string playerNameFieldName = "̖̑̒̍̐̑̍̑̒̚̕";

		public sbyte Instrument => (sbyte)instrumentField.GetValue(playerProfile);
		private static FieldInfo instrumentField;
		private const string instrumentFieldName = "̛̜̎̔̎̓̍̔̐̚̚";

		public sbyte Difficulty => (sbyte)difficultyField.GetValue(playerProfile);
		private static FieldInfo difficultyField;
		private const string difficultyFieldName = "̜̘̎̐̒̔̎̒̎̚̚";

		public PlayerProfileWrapper(object playerProfile) {
			InitializeSingletonFields();
			this.playerProfile = playerProfile;
		}

		private static void InitializeSingletonFields() {
			RegisterField(ref playerNameField, PlayerProfileType, playerNameFieldName);
			RegisterField(ref instrumentField, PlayerProfileType, instrumentFieldName);
			RegisterField(ref difficultyField, PlayerProfileType, difficultyFieldName);
		}
	}
}
