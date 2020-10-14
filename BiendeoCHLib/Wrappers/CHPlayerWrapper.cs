using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("\u0315\u0313\u0315\u030F\u030E\u030D\u0314\u031C\u0313\u0316\u0313")]
	public struct CHPlayerWrapper {
		public object CHPlayer { get; private set; }

		public static CHPlayerWrapper Wrap(object chPlayer) => new CHPlayerWrapper {
			CHPlayer = chPlayer
		};

		public override bool Equals(object obj) => CHPlayer.Equals(obj);

		public override int GetHashCode() => CHPlayer.GetHashCode();

		public bool IsNull() => CHPlayer == null;

		#region Fields

		public PlayerProfileWrapper PlayerProfile {
			get => PlayerProfileWrapper.Wrap(playerProfileField(CHPlayer));
			set => playerProfileField(CHPlayer) = value.PlayerProfile;
		}
		[WrapperField("\u0313\u0318\u0314\u0316\u0313\u0317\u0312\u0317\u031A\u0314\u031A")]
		private static readonly AccessTools.FieldRef<object, object> playerProfileField;

		public int PlayerIndex {
			get => playerIndexField(CHPlayer);
			set => playerIndexField(CHPlayer) = value;
		}
		[WrapperField("\u0311\u0315\u031C\u031A\u031A\u0314\u030F\u0317\u0312\u0315\u0316")]
		private static readonly AccessTools.FieldRef<object, int> playerIndexField;

		public bool IsModifyingProfile {
			get => isModifyingProfileField(CHPlayer);
			set => isModifyingProfileField(CHPlayer) = value;
		}
		[WrapperField("\u030F\u030F\u0316\u0316\u0313\u0311\u0311\u0317\u0319\u0313\u0319")]
		private static readonly AccessTools.FieldRef<object, bool> isModifyingProfileField;

		#endregion
	}
}
