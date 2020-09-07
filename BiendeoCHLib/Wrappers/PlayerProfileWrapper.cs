using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("\u0311\u0316\u0315\u031B\u0310\u0314\u0316\u030E\u0311\u0315\u031B")]
	public struct PlayerProfileWrapper {
		public object PlayerProfile { get; private set; }

		public static PlayerProfileWrapper Wrap(object playerProfile) => new PlayerProfileWrapper {
			PlayerProfile = playerProfile
		};

		public override bool Equals(object obj) => PlayerProfile.Equals(obj);

		public override int GetHashCode() => PlayerProfile.GetHashCode();

		public bool IsNull() => PlayerProfile == null;

		#region Fields

		public string PlayerName {
			get => playerNameField(PlayerProfile);
			set => playerNameField(PlayerProfile) = value;
		}
		[WrapperField("\u031A\u0311\u0312\u030D\u0315\u0310\u0311\u0316\u030D\u0311\u0312")]
		private static readonly AccessTools.FieldRef<object, string> playerNameField;

		public ControllerType Instrument {
			get => (ControllerType)instrumentField(PlayerProfile);
			set => instrumentField(PlayerProfile) = value;
		}
		[WrapperField("\u030E\u031C\u0314\u031B\u030E\u031A\u0313\u030D\u0314\u0310\u031A")]
		private static readonly AccessTools.FieldRef<object, object> instrumentField;

		public Difficulty Difficulty {
			get => (Difficulty)difficultyField(PlayerProfile);
			set => difficultyField(PlayerProfile) = value;
		}
		[WrapperField("\u030E\u0310\u0312\u031C\u0314\u031A\u030E\u031A\u0312\u0318\u030E")]
		private static readonly AccessTools.FieldRef<object, object> difficultyField;

		#endregion

		#region Enumerations

		public enum ControllerType : byte {
			Guitar,
			GHLGuitar,
			Drums
		}

		#endregion
	}
}
