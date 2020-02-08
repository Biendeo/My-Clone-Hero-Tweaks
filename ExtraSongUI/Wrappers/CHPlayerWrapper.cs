using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtraSongUI.Wrappers {
	internal class CHPlayerWrapper : WrapperBase {
		public readonly object chPlayer;
		public static Type CHPlayerType;

		public PlayerProfileWrapper PlayerProfile => new PlayerProfileWrapper(playerProfileField.GetValue(chPlayer));
		private static FieldInfo playerProfileField;
		private const string playerProfileFieldName = "\u0313\u0318\u0314\u0316\u0313\u0317\u0312\u0317\u031A\u0314\u031A";

		//TODO: When I need Player.
		//public PlayerWrapper Player => new PlayerWrapper((Player)playerField.GetValue(chPlayer));
		//private static FieldInfo playerField;
		//private const string playerFieldName = "\u0317\u0319\u0316\u030E\u031A\u030E\u031A\u031A\u0319\u0311\u0318";

		public int PlayerIndex => (int)playerIndexField.GetValue(chPlayer);
		private static FieldInfo playerIndexField;
		private const string playerIndexFieldName = "\u0311\u0315\u031C\u031A\u031A\u0314\u030F\u0317\u0312\u0315\u0316";

		public bool IsModifyingProfile => (bool)isModifyingProfileField.GetValue(chPlayer);
		private static FieldInfo isModifyingProfileField;
		private const string isModifyingProfileFieldName = "\u030F\u030F\u0316\u0316\u0313\u0311\u0311\u0317\u0319\u0313\u0319";

		public CHPlayerWrapper(object chPlayer) {
			this.chPlayer = chPlayer;
		}

		public static void InitializeSingletonFields() {
			CHPlayerType = Assembly.Load("Assembly-CSharp.dll").GetType("\u0315\u0313\u0315\u030F\u030E\u030D\u0314\u031C\u0313\u0316\u0313");
			RegisterField(ref playerProfileField, CHPlayerType, playerProfileFieldName);
			RegisterField(ref playerIndexField, CHPlayerType, playerIndexFieldName);
			RegisterField(ref isModifyingProfileField, CHPlayerType, isModifyingProfileFieldName);
		}
	}
}
