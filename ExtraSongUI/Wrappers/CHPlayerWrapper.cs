using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtraSongUI.Wrappers {
	internal class CHPlayerWrapper : WrapperBase {
		//! This is currently \u0315\u0313\u0315\u030F\u030E\u030D\u0314\u031C\u0313\u0316\u0313 in the codebase. We
		//! lose compile time checking in instantiation but hopefully this makes it simple to work with otherwise.
		public readonly object chPlayer;
		public static Type CHPlayerType => Assembly.Load("Assembly-CSharp.dll").GetType("̜̖̓̏̎̍̔̓̓̕̕");

		public PlayerProfileWrapper PlayerProfile => new PlayerProfileWrapper(playerProfileField.GetValue(chPlayer));
		private static FieldInfo playerProfileField;
		private const string playerProfileFieldName = "̘̖̗̗̓̔̓̒̔̚̚";

		//TODO: When I need Player.
		//public PlayerWrapper Player => new PlayerWrapper((Player)playerField.GetValue(chPlayer));
		//private static FieldInfo playerField;
		//private const string playerFieldName = "̗̙̖̙̘̎̎̑̚̚̚";

		public int PlayerIndex => (int)playerIndexField.GetValue(chPlayer);
		private static FieldInfo playerIndexField;
		private const string playerIndexFieldName = "̜̗̖̑̔̏̒̕̚̚̕";

		public bool IsModifyingProfile => (bool)isModifyingProfileField.GetValue(chPlayer);
		private static FieldInfo isModifyingProfileField;
		private const string isModifyingProfileFieldName = "̖̖̗̙̙̏̏̓̑̑̓";

		public CHPlayerWrapper(object chPlayer) {
			InitializeSingletonFields();
			this.chPlayer = chPlayer;
		}

		private static void InitializeSingletonFields() {
			RegisterField(ref playerProfileField, CHPlayerType, playerProfileFieldName);
			RegisterField(ref playerIndexField, CHPlayerType, playerIndexFieldName);
			RegisterField(ref isModifyingProfileField, CHPlayerType, isModifyingProfileFieldName);
		}
	}
}
