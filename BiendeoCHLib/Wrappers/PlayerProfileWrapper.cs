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

		//? Different enum, there's more fields there
		public InstrumentType Instrument {
			get => (InstrumentType)instrumentField(PlayerProfile);
			set => instrumentField(PlayerProfile) = (sbyte)value;
		}
		[WrapperField("\u030E\u031C\u0314\u031B\u030E\u031A\u0313\u030D\u0314\u0310\u031A")]
		private static readonly AccessTools.FieldRef<object, sbyte> instrumentField;

		public Difficulty Difficulty {
			get => (Difficulty)difficultyField(PlayerProfile);
			set => difficultyField(PlayerProfile) = (sbyte)value;
		}
		[WrapperField("\u030E\u0310\u0312\u031C\u0314\u031A\u030E\u031A\u0312\u0318\u030E")]
		private static readonly AccessTools.FieldRef<object, sbyte> difficultyField;

		public NoteWrapper.Modifier Modifiers {
			get => (NoteWrapper.Modifier)modifiersField(PlayerProfile);
			set => modifiersField(PlayerProfile) = (int)value;
		}
		[WrapperField("\u030E\u0315\u0317\u030F\u0312\u0316\u0313\u0311\u030E\u030D\u0318")]
		private static readonly AccessTools.FieldRef<object, int> modifiersField;

		public SongWrapper.Instrument SongInstrument {
			get => (SongWrapper.Instrument)songInstrumentField(PlayerProfile);
			set => songInstrumentField(PlayerProfile) = (int)value;
		}
		[WrapperField("\u0314\u0313\u0319\u0319\u030E\u0312\u031B\u0310\u0311\u030F\u0319")]
		private static readonly AccessTools.FieldRef<object, int> songInstrumentField;

		public SongWrapper.Difficulty SongDifficulty {
			get => (SongWrapper.Difficulty)songDifficultyField(PlayerProfile);
			set => songDifficultyField(PlayerProfile) = (int)value;
		}
		[WrapperField("\u0310\u030F\u0312\u031A\u0311\u031A\u0310\u0316\u0316\u0316\u0318")]
		private static readonly AccessTools.FieldRef<object, int> songDifficultyField;

		public GameSettingWrapper NoteSpeed {
			get => GameSettingWrapper.Wrap(noteSpeedField(PlayerProfile));
			set => noteSpeedField(PlayerProfile) = value.GameSetting;
		}
		[WrapperField("\u0313\u030E\u0314\u0318\u0317\u031A\u0315\u031C\u0313\u0316\u0312")]
		private static readonly AccessTools.FieldRef<object, object> noteSpeedField;

		public GameSettingWrapper Tilt {
			get => GameSettingWrapper.Wrap(tiltField(PlayerProfile));
			set => tiltField(PlayerProfile) = value.GameSetting;
		}
		[WrapperField("\u030E\u031A\u031A\u0313\u031B\u0316\u0318\u031C\u0317\u031B\u031A")]
		private static readonly AccessTools.FieldRef<object, object> tiltField;

		public GameSettingWrapper LeftyFlip {
			get => GameSettingWrapper.Wrap(leftyFlipField(PlayerProfile));
			set => leftyFlipField(PlayerProfile) = value.GameSetting;
		}
		[WrapperField("\u0312\u0315\u0312\u0312\u0318\u0314\u0319\u0319\u031C\u030D\u031A")]
		private static readonly AccessTools.FieldRef<object, object> leftyFlipField;

		public GameSettingWrapper GamepadMode {
			get => GameSettingWrapper.Wrap(gamepadModeField(PlayerProfile));
			set => gamepadModeField(PlayerProfile) = value.GameSetting;
		}
		[WrapperField("\u0310\u031B\u0318\u0314\u0316\u031B\u0316\u0315\u031C\u0315\u0317")]
		private static readonly AccessTools.FieldRef<object, object> gamepadModeField;

		public GameSettingWrapper Bot {
			get => GameSettingWrapper.Wrap(botField(PlayerProfile));
			set => botField(PlayerProfile) = value.GameSetting;
		}
		[WrapperField("\u0310\u030E\u031C\u030D\u030E\u0311\u030F\u030D\u031A\u030E\u0317")]
		private static readonly AccessTools.FieldRef<object, object> botField;

		public GameSettingWrapper DisplayName {
			get => GameSettingWrapper.Wrap(displayNameField(PlayerProfile));
			set => displayNameField(PlayerProfile) = value.GameSetting;
		}
		[WrapperField("\u030F\u0310\u0315\u031A\u0314\u030F\u0318\u030D\u030F\u031C\u031C")]
		private static readonly AccessTools.FieldRef<object, object> displayNameField;

		public GameSettingWrapper HighwayIndex {
			get => GameSettingWrapper.Wrap(highwayIndexField(PlayerProfile));
			set => highwayIndexField(PlayerProfile) = value.GameSetting;
		}
		[WrapperField("\u031C\u030F\u0310\u031B\u0319\u031B\u031C\u030E\u030E\u0314\u030E")]
		private static readonly AccessTools.FieldRef<object, object> highwayIndexField;

		public GameSettingWrapper Controller {
			get => GameSettingWrapper.Wrap(controllerField(PlayerProfile));
			set => controllerField(PlayerProfile) = value.GameSetting;
		}
		[WrapperField("\u030D\u031B\u0318\u0310\u031B\u0310\u0318\u031C\u0313\u030E\u0311")]
		private static readonly AccessTools.FieldRef<object, object> controllerField;

		public bool IsGuest {
			get => isGuestField(PlayerProfile);
			set => isGuestField(PlayerProfile) = value;
		}
		[WrapperField("\u0316\u0316\u0315\u031C\u031B\u0313\u0319\u030E\u0310\u0314\u0319")]
		private static readonly AccessTools.FieldRef<object, bool> isGuestField;

		#endregion

		#region Properties

		public string ControllerTypeString => (string)controllerTypeStringProperty.GetValue(PlayerProfile);
		[WrapperProperty("\u030F\u0319\u0312\u0310\u0316\u0314\u0318\u0318\u0310\u0312\u0312")]
		private static readonly PropertyInfo controllerTypeStringProperty;

		// An unreferenced property that just returns true.
		public bool True => (bool)trueProperty.GetValue(PlayerProfile);
		[WrapperProperty("\u031B\u0315\u0314\u0311\u0318\u0313\u030D\u0315\u031A\u030D\u0312")]
		private static readonly PropertyInfo trueProperty;

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
