using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrappers {
	[Wrapper("\u0315\u0313\u0315\u030F\u030E\u030D\u0314\u031C\u0313\u0316\u0313")]
	internal class CHPlayerWrapper {
		public readonly object chPlayer;

		public PlayerProfileWrapper PlayerProfile => new PlayerProfileWrapper(playerProfileField.GetValue(chPlayer));
		[WrapperField("\u0313\u0318\u0314\u0316\u0313\u0317\u0312\u0317\u031A\u0314\u031A")]
		private static readonly FieldInfo playerProfileField;

		//TODO: When I need Player.
		//public PlayerWrapper Player => new PlayerWrapper((Player)playerField.GetValue(chPlayer));
		//private static readonly FieldInfo playerField;
		//private const string playerFieldName = "\u0317\u0319\u0316\u030E\u031A\u030E\u031A\u031A\u0319\u0311\u0318";

		public int PlayerIndex => (int)playerIndexField.GetValue(chPlayer);
		[WrapperField("\u0311\u0315\u031C\u031A\u031A\u0314\u030F\u0317\u0312\u0315\u0316")]
		private static readonly FieldInfo playerIndexField;

		public bool IsModifyingProfile => (bool)isModifyingProfileField.GetValue(chPlayer);
		[WrapperField("\u030F\u030F\u0316\u0316\u0313\u0311\u0311\u0317\u0319\u0313\u0319")]
		private static readonly FieldInfo isModifyingProfileField;

		public CHPlayerWrapper(object chPlayer) {
			this.chPlayer = chPlayer;
		}
	}
}
