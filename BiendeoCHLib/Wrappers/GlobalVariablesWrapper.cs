using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(GlobalVariables))]
	public struct GlobalVariablesWrapper {
		public readonly GlobalVariables globalVariables;

		public GlobalVariablesWrapper(GlobalVariables globalVariables) {
			this.globalVariables = globalVariables;
		}

		#region Fields

		public static GlobalVariablesWrapper Instance => new GlobalVariablesWrapper((GlobalVariables)instanceField.GetValue(null));
		[WrapperField("\u0312\u0313\u0310\u0315\u030E\u0319\u030D\u0318\u0313\u030E\u031A")]
		private static readonly FieldInfo instanceField;

		public static bool IsSaving => (bool)isSavingField.GetValue(null);
		[WrapperField("\u0314\u0314\u0310\u031A\u031A\u0317\u0319\u0310\u0313\u030E\u0316")]
		private static readonly FieldInfo isSavingField;

		public static int ProfileFileVersion => (int)profileFileVersionField.GetValue(null);
		[WrapperField("\u0311\u031C\u0313\u031A\u0314\u0311\u031C\u031C\u0310\u0312\u0315")]
		private static readonly FieldInfo profileFileVersionField;

		public string ScreenshotsPath => (string)screenshotsPathField.GetValue(globalVariables);
		[WrapperField("\u030D\u030D\u0315\u0318\u031A\u0313\u031A\u0315\u0311\u0310\u0318")]
		private static readonly FieldInfo screenshotsPathField;

		public string SettingsPath => (string)settingsPathField.GetValue(globalVariables);
		[WrapperField("\u031C\u0317\u030F\u030E\u030D\u030F\u031C\u0318\u031C\u0318\u0315")]
		private static readonly FieldInfo settingsPathField;

		public List<string> SongPaths => (List<string>)songPathsField.GetValue(globalVariables);
		[WrapperField("\u0310\u031A\u0313\u030D\u031C\u0319\u0310\u0316\u0310\u0319\u0312")]
		private static readonly FieldInfo songPathsField;

		public List<string> UniqueSongPaths => (List<string>)uniqueSongPathsField.GetValue(globalVariables);
		[WrapperField("\u031B\u0310\u0313\u0315\u0313\u0318\u0313\u0318\u0319\u030D\u0313")]
		private static readonly FieldInfo uniqueSongPathsField;

		public string CustomSongExport => (string)customSongExportField.GetValue(globalVariables);
		[WrapperField("\u031C\u0317\u030F\u030E\u030D\u030F\u031C\u0318\u031C\u0318\u0315")]
		private static readonly FieldInfo customSongExportField;

		public string BuildVersion => (string)buildVersionField.GetValue(globalVariables);
		[WrapperField("\u030E\u030E\u0313\u030D\u0319\u031A\u030E\u0319\u0314\u0318\u031C")]
		private static readonly FieldInfo buildVersionField;

		public string[] Languages => (string[])languagesField.GetValue(globalVariables);
		[WrapperField("\u0312\u0319\u0318\u0315\u031A\u031A\u0310\u030D\u0313\u0318\u031A")]
		private static readonly FieldInfo languagesField;

		public string[] SplashMessages => (string[])splashMessagesField.GetValue(globalVariables);
		[WrapperField("\u0312\u0316\u031C\u0311\u030F\u030F\u030D\u030E\u0314\u0316\u0315")]
		private static readonly FieldInfo splashMessagesField;

		public bool AprilFoolsMode => (bool)aprilFoolsModeField.GetValue(globalVariables);
		[WrapperField("\u0318\u0319\u0316\u0315\u0315\u0313\u0310\u0311\u030F\u0313\u030E")]
		private static readonly FieldInfo aprilFoolsModeField;

		// Either sound device or SetWhammyFast?
		public GameSettingWrapper VolumeDevice => new GameSettingWrapper(volumeDeviceField.GetValue(globalVariables));
		[WrapperField("\u030D\u0314\u0319\u030F\u0311\u031A\u0315\u0313\u0311\u030F\u031A")]
		private static readonly FieldInfo volumeDeviceField;

		public GameSettingWrapper LowLatencyMode => new GameSettingWrapper(lowLatencyModeField.GetValue(globalVariables));
		[WrapperField("\u030D\u0316\u0311\u0311\u031C\u0319\u0312\u0318\u030F\u031C\u0318")]
		private static readonly FieldInfo lowLatencyModeField;

		public GameSettingWrapper StreamerHighwayPlacement => new GameSettingWrapper(streamerHighwayPlacementField.GetValue(globalVariables));
		[WrapperField("\u030E\u030D\u030D\u0311\u030F\u031A\u0319\u031B\u031B\u0316\u0313")]
		private static readonly FieldInfo streamerHighwayPlacementField;

		public GameSettingWrapper GameShowBotScore => new GameSettingWrapper(gameShowBotScoreField.GetValue(globalVariables));
		[WrapperField("\u030E\u0310\u030E\u0312\u030F\u0311\u0311\u031B\u0316\u0319\u0316")]
		private static readonly FieldInfo gameShowBotScoreField;

		public GameSettingWrapper GameMuteOnMiss => new GameSettingWrapper(gameMuteOnMissField.GetValue(globalVariables));
		[WrapperField("\u030E\u0317\u030F\u0315\u0313\u0312\u0313\u0317\u0319\u030F\u0313")]
		private static readonly FieldInfo gameMuteOnMissField;

		public GameSettingWrapper GameFullPlaylist => new GameSettingWrapper(gameFullPlaylistField.GetValue(globalVariables));
		[WrapperField("\u030F\u030F\u0310\u0312\u0312\u031B\u0315\u0310\u0316\u031A\u0314")]
		private static readonly FieldInfo gameFullPlaylistField;

		public GameSettingWrapper VideoHighwayLightning => new GameSettingWrapper(videoHighwayLightningField.GetValue(globalVariables));
		[WrapperField("\u030F\u0310\u0314\u0314\u031C\u0313\u0319\u031C\u030E\u030F\u030E")]
		private static readonly FieldInfo videoHighwayLightningField;

		public GameSettingWrapper VideoFrameRate => new GameSettingWrapper(videoFrameRateField.GetValue(globalVariables));
		[WrapperField("\u030F\u0313\u0310\u0314\u0319\u030D\u0315\u0318\u0315\u0314\u0316")]
		private static readonly FieldInfo videoFrameRateField;

		public GameSettingWrapper GameEnableCursor => new GameSettingWrapper(gameEnableCursorField.GetValue(globalVariables));
		[WrapperField("\u0310\u030E\u030E\u0318\u0311\u0314\u0314\u0310\u0314\u0317\u0312")]
		private static readonly FieldInfo gameEnableCursorField;

		public GameSettingWrapper VideoVsync => new GameSettingWrapper(videoVsyncField.GetValue(globalVariables));
		[WrapperField("\u0310\u030F\u030F\u0314\u0315\u0311\u030E\u030F\u0313\u031C\u031C")]
		private static readonly FieldInfo videoVsyncField;

		public GameSettingWrapper OnlineShowRemoteNames => new GameSettingWrapper(onlineShowRemoteNamesField.GetValue(globalVariables));
		[WrapperField("\u0310\u0312\u030F\u030D\u0314\u0315\u0319\u0316\u030F\u031A\u030E")]
		private static readonly FieldInfo onlineShowRemoteNamesField;

		public GameSettingWrapper VolumePreviewVolume => new GameSettingWrapper(volumePreviewVolumeField.GetValue(globalVariables));
		[WrapperField("\u0310\u0315\u031A\u0310\u030D\u0315\u0311\u0314\u0317\u0315\u0318")]
		private static readonly FieldInfo volumePreviewVolumeField;

		public GameSettingWrapper CustomSongBackgrounds => new GameSettingWrapper(customSongBackgroundsField.GetValue(globalVariables));
		[WrapperField("\u0310\u0315\u031B\u0316\u031B\u0316\u0315\u0316\u030F\u0310\u030F")]
		private static readonly FieldInfo customSongBackgroundsField;

		public GameSettingWrapper GameEnableLyrics => new GameSettingWrapper(gameEnableLyricsField.GetValue(globalVariables));
		[WrapperField("\u0310\u0319\u031A\u0317\u0312\u0313\u031A\u030E\u0316\u030F\u031C")]
		private static readonly FieldInfo gameEnableLyricsField;

		public GameSettingWrapper GameLanguage => new GameSettingWrapper(gameLanguageField.GetValue(globalVariables));
		[WrapperField("\u0310\u031C\u030D\u031C\u030F\u0319\u031B\u0313\u0310\u0318\u0319")]
		private static readonly FieldInfo gameLanguageField;

		public GameSettingWrapper GameHighwayShake => new GameSettingWrapper(gameHighwayShakeField.GetValue(globalVariables));
		[WrapperField("\u0311\u0310\u0310\u0313\u031A\u030E\u031C\u0318\u030F\u0310\u031B")]
		private static readonly FieldInfo gameHighwayShakeField;

		public GameSettingWrapper VolumeBackend => new GameSettingWrapper(volumeBackendField.GetValue(globalVariables));
		[WrapperField("\u0311\u0315\u030F\u0315\u030F\u0312\u031B\u0315\u0317\u0315\u031A")]
		private static readonly FieldInfo volumeBackendField;

		public GameSettingWrapper OffsetsVideo => new GameSettingWrapper(offsetsVideoField.GetValue(globalVariables));
		[WrapperField("\u0311\u0316\u0316\u030F\u031B\u0314\u0310\u030E\u031A\u0312\u031B")]
		private static readonly FieldInfo offsetsVideoField;

		public GameSettingWrapper OnlineLowSongSpeed => new GameSettingWrapper(onlineLowSongSpeedField.GetValue(globalVariables));
		[WrapperField("\u0311\u031A\u030D\u0312\u030F\u0316\u031A\u031A\u0319\u0310\u030F")]
		private static readonly FieldInfo onlineLowSongSpeedField;

		public GameSettingWrapper VideoStarAnimation => new GameSettingWrapper(videoStarAnimationField.GetValue(globalVariables));
		[WrapperField("\u0312\u0311\u0318\u0316\u030F\u031B\u031A\u0313\u031A\u030F\u0311")]
		private static readonly FieldInfo videoStarAnimationField;

		public GameSettingWrapper OffsetsAudio => new GameSettingWrapper(offsetsAudioField.GetValue(globalVariables));
		[WrapperField("\u0312\u0314\u031B\u0318\u0318\u0312\u0319\u0311\u0315\u031C\u030E")]
		private static readonly FieldInfo offsetsAudioField;

		public GameSettingWrapper VideoMsaa => new GameSettingWrapper(videoMsaaField.GetValue(globalVariables));
		[WrapperField("\u0312\u0319\u0314\u0316\u0313\u0314\u030D\u0317\u031C\u030D\u0311")]
		private static readonly FieldInfo videoMsaaField;

		public GameSettingWrapper VideoDebugFPS => new GameSettingWrapper(videoDebugFPSField.GetValue(globalVariables));
		[WrapperField("\u0312\u0319\u0318\u031B\u031B\u0311\u031C\u0319\u031A\u0315\u0311")]
		private static readonly FieldInfo videoDebugFPSField;

		public GameSettingWrapper VolumeSounds => new GameSettingWrapper(volumeSoundsField.GetValue(globalVariables));
		[WrapperField("\u0312\u031B\u0319\u0310\u0317\u0316\u030F\u031A\u0318\u031B\u031A")]
		private static readonly FieldInfo volumeSoundsField;

		public GameSettingWrapper SongSpeed => new GameSettingWrapper(songSpeedField.GetValue(globalVariables));
		[WrapperField("\u0313\u030E\u0310\u031C\u0319\u0313\u0313\u031B\u0314\u0312\u0314")]
		private static readonly FieldInfo songSpeedField;

		public GameSettingWrapper OnlineHighwayPlacement => new GameSettingWrapper(onlineHighwayPlacementField.GetValue(globalVariables));
		[WrapperField("\u0313\u030F\u030E\u0311\u0313\u031C\u0314\u0313\u0310\u0317\u031B")]
		private static readonly FieldInfo onlineHighwayPlacementField;

		public GameSettingWrapper GameMenuMusic => new GameSettingWrapper(gameMenuMusicField.GetValue(globalVariables));
		[WrapperField("\u0313\u0310\u0316\u0319\u031B\u031C\u030D\u030E\u030F\u0315\u0316")]
		private static readonly FieldInfo gameMenuMusicField;

		public GameSettingWrapper GameSortFilter => new GameSettingWrapper(gameSortFilterField.GetValue(globalVariables));
		[WrapperField("\u0313\u0315\u0310\u0318\u031A\u0316\u0315\u0319\u0319\u0319\u030D")]
		private static readonly FieldInfo gameSortFilterField;

		public GameSettingWrapper VolumeMuteVolume => new GameSettingWrapper(volumeMuteVolumeField.GetValue(globalVariables));
		[WrapperField("\u0313\u0315\u0312\u030F\u031C\u0311\u0313\u0317\u0319\u0317\u0311")]
		private static readonly FieldInfo volumeMuteVolumeField;

		public GameSettingWrapper CustomBackgroundImage => new GameSettingWrapper(customBackgroundImageField.GetValue(globalVariables));
		[WrapperField("\u0313\u0318\u030E\u0315\u0319\u030E\u030E\u030E\u031C\u0319\u031A")]
		private static readonly FieldInfo customBackgroundImageField;

		public GameSettingWrapper GameShowHitWindow => new GameSettingWrapper(gameShowHitWindowField.GetValue(globalVariables));
		[WrapperField("\u0314\u0312\u030F\u030E\u030F\u031C\u031A\u0316\u031C\u0317\u0317")]
		private static readonly FieldInfo gameShowHitWindowField;

		public GameSettingWrapper GamePauseOnFocusLost => new GameSettingWrapper(gamePauseOnFocusLostField.GetValue(globalVariables));
		[WrapperField("\u0314\u031B\u0313\u0310\u0318\u030F\u0311\u030E\u031A\u0316\u0319")]
		private static readonly FieldInfo gamePauseOnFocusLostField;

		public GameSettingWrapper VideoHighwaySP => new GameSettingWrapper(videoHighwaySPField.GetValue(globalVariables));
		[WrapperField("\u0315\u0315\u0312\u0317\u0314\u031A\u031A\u030E\u0319\u0316\u031C")]
		private static readonly FieldInfo videoHighwaySPField;

		public GameSettingWrapper GameGemSize => new GameSettingWrapper(gameGemSizeField.GetValue(globalVariables));
		[WrapperField("\u0316\u030D\u030D\u0310\u0313\u0317\u030F\u0314\u0310\u0319\u0314")]
		private static readonly FieldInfo gameGemSizeField;

		public GameSettingWrapper VideoFlames => new GameSettingWrapper(videoFlamesField.GetValue(globalVariables));
		[WrapperField("\u0316\u0310\u030D\u030F\u030D\u031A\u031B\u0312\u0315\u0314\u0315")]
		private static readonly FieldInfo videoFlamesField;

		public GameSettingWrapper VideoParticles => new GameSettingWrapper(videoParticlesField.GetValue(globalVariables));
		[WrapperField("\u0316\u0311\u030E\u0314\u0314\u0310\u0313\u0313\u0316\u0314\u0315")]
		private static readonly FieldInfo videoParticlesField;

		public GameSettingWrapper GamePollRate => new GameSettingWrapper(gamePollRateField.GetValue(globalVariables));
		[WrapperField("\u0316\u0318\u0312\u0314\u0317\u0318\u030D\u0312\u0311\u0314\u0318")]
		private static readonly FieldInfo gamePollRateField;

		public GameSettingWrapper GameAllowDuplicateSongs => new GameSettingWrapper(gameAllowDuplicateSongsField.GetValue(globalVariables));
		[WrapperField("\u0317\u0317\u0319\u0318\u030D\u0314\u0319\u0316\u031A\u0316\u0318")]
		private static readonly FieldInfo gameAllowDuplicateSongsField;

		public GameSettingWrapper VolumeMasterVolume => new GameSettingWrapper(volumeMasterVolumeField.GetValue(globalVariables));
		[WrapperField("\u0318\u030D\u0313\u031B\u0315\u030F\u030D\u0315\u030D\u0317\u030D")]
		private static readonly FieldInfo volumeMasterVolumeField;

		public GameSettingWrapper VideoNoteAnimation => new GameSettingWrapper(videoNoteAnimationField.GetValue(globalVariables));
		[WrapperField("\u0318\u030F\u030E\u030D\u0310\u0319\u0315\u030D\u0315\u0314\u0315")]
		private static readonly FieldInfo videoNoteAnimationField;

		public GameSettingWrapper StreamerSongExport => new GameSettingWrapper(streamerSongExportField.GetValue(globalVariables));
		[WrapperField("\u0319\u030D\u031A\u0314\u0315\u0316\u031B\u0312\u0316\u031A\u031A")]
		private static readonly FieldInfo streamerSongExportField;

		public GameSettingWrapper GameSongPreview => new GameSettingWrapper(gameSongPreviewField.GetValue(globalVariables));
		[WrapperField("\u0319\u0310\u030E\u030F\u030D\u0312\u0316\u0312\u031A\u0317\u0313")]
		private static readonly FieldInfo gameSongPreviewField;

		public GameSettingWrapper CustomSongVideos => new GameSettingWrapper(customSongVideosField.GetValue(globalVariables));
		[WrapperField("\u0319\u0318\u031B\u0313\u0318\u031B\u0313\u030F\u030E\u030E\u030E")]
		private static readonly FieldInfo customSongVideosField;

		public GameSettingWrapper VolumeMenuVolume => new GameSettingWrapper(volumeMenuVolumeField.GetValue(globalVariables));
		[WrapperField("\u0319\u031B\u0319\u0317\u0310\u0316\u0319\u030F\u0317\u0316\u0319")]
		private static readonly FieldInfo volumeMenuVolumeField;

		public GameSettingWrapper CustomBackgroundVideo => new GameSettingWrapper(customBackgroundVideoField.GetValue(globalVariables));
		[WrapperField("\u031A\u030E\u0312\u0310\u0314\u0318\u0311\u030D\u030E\u030E\u030E")]
		private static readonly FieldInfo customBackgroundVideoField;

		public GameSettingWrapper GameNoFail => new GameSettingWrapper(gameNoFailField.GetValue(globalVariables));
		[WrapperField("\u031A\u0312\u0312\u0313\u0316\u0315\u031B\u0318\u0316\u0318\u0311")]
		private static readonly FieldInfo gameNoFailField;

		public GameSettingWrapper OnlineSongsPerClient => new GameSettingWrapper(onlineSongsPerClientField.GetValue(globalVariables));
		[WrapperField("\u031A\u0317\u031B\u0312\u030E\u0314\u030D\u031C\u0312\u030D\u031B")]
		private static readonly FieldInfo onlineSongsPerClientField;

		public GameSettingWrapper OnlineClientRemoveSongs => new GameSettingWrapper(onlineClientRemoveSongsField.GetValue(globalVariables));
		[WrapperField("\u031A\u031B\u030F\u0312\u030F\u030E\u0317\u0319\u0311\u030E\u0316")]
		private static readonly FieldInfo onlineClientRemoveSongsField;

		public GameSettingWrapper VideoMenuBackground => new GameSettingWrapper(videoMenuBackgroundField.GetValue(globalVariables));
		[WrapperField("\u031A\u031C\u0319\u0319\u031B\u0315\u030D\u0310\u0318\u0318\u0314")]
		private static readonly FieldInfo videoMenuBackgroundField;

		public GameSettingWrapper VideoSongDisplay => new GameSettingWrapper(videoSongDisplayField.GetValue(globalVariables));
		[WrapperField("\u031B\u0316\u031B\u0312\u0318\u030E\u0316\u0318\u031B\u0310\u031B")]
		private static readonly FieldInfo videoSongDisplayField;

		public GameSettingWrapper OnlineMaxSongSpeed => new GameSettingWrapper(onlineMaxSongSpeedField.GetValue(globalVariables));
		[WrapperField("\u031B\u0317\u0311\u031C\u0311\u0316\u0319\u030D\u0312\u0312\u0311")]
		private static readonly FieldInfo onlineMaxSongSpeedField;

		public GameSettingWrapper OnlineServerTickRate => new GameSettingWrapper(onlineServerTickRateField.GetValue(globalVariables));
		[WrapperField("\u031B\u0318\u0311\u0311\u031C\u031A\u030F\u0316\u0311\u0316\u0310")]
		private static readonly FieldInfo onlineServerTickRateField;

		// The stem values are as followed:
		// [0]: Guitar volume
		// [1]: Bass volume
		// [2]: Rhythm volume
		// [3]/[4]/[5]: Vocals volume
		// [6]/[7]/[8]/[9]/[10]: Drums volume
		// [11]: Keys volume
		// [12]: Song volume
		// [13]: Crowd volume
		// Duplicate indicies are just shared references. I think they may be used in BassAudioManager only.
		public GameSettingWrapper[] VolumeStems => ((object[])volumeStemsField.GetValue(globalVariables)).Select(bp => new GameSettingWrapper(bp)).ToArray();
		[WrapperField("\u031B\u0315\u031B\u0319\u030D\u0313\u0319\u0317\u031A\u0318\u0312")]
		private static readonly FieldInfo volumeStemsField;

		public bool IsInPracticeMode => (bool)isInPracticeModeField.GetValue(globalVariables);
		[WrapperField("\u0318\u030E\u0310\u0315\u0313\u0318\u031C\u0318\u030E\u0313\u031A")]
		private static readonly FieldInfo isInPracticeModeField;

		public string CachePath => (string)cachePathField.GetValue(globalVariables);
		[WrapperField("\u0315\u0312\u0315\u0314\u0319\u0316\u0312\u030D\u030D\u031A\u030E")]
		private static readonly FieldInfo cachePathField;

		// Just duplicates SongEntry, so use that instead.
		public object SongEntryObject {
			get => songEntryObjectField.GetValue(globalVariables);
			set => songEntryObjectField.SetValue(globalVariables, value);
		}
		[WrapperField("\u0317\u0313\u0311\u0317\u031B\u0314\u0314\u0313\u031B\u0315\u0317")]
		private static readonly FieldInfo songEntryObjectField;

		// Changes when selecting the instrument after deciding a song.
		public SongEntryWrapper SongEntry {
			get => SongEntryWrapper.Wrap((SongEntry)songEntryField.GetValue(globalVariables));
			set => songEntryField.SetValue(globalVariables, value.SongEntry);
		}
		[WrapperField("\u0310\u030E\u0313\u0310\u0313\u0314\u030D\u031B\u0313\u031C\u0313")]
		private static readonly FieldInfo songEntryField;

		// Always true, but it seems to control background music on app focus.
		// There's an existing menu music setting though.
		//TODO: Is this an appropriate name?
		public bool IsBackgroundMusicEnabled {
			get => (bool)isBackgroundMusicEnabledField.GetValue(globalVariables);
			set => isBackgroundMusicEnabledField.SetValue(globalVariables, value);
		}
		[WrapperField("\u030E\u0318\u0314\u0314\u0313\u031C\u0316\u0314\u031B\u0310\u0313")]
		private static readonly FieldInfo isBackgroundMusicEnabledField;

		public int NumberOfPlayers {
			get => (int)numberOfPlayersField.GetValue(globalVariables);
			set => numberOfPlayersField.SetValue(globalVariables, value);
		}
		[WrapperField("\u0317\u030D\u0310\u030E\u030D\u0317\u0315\u031A\u0316\u0312\u030D")]
		private static readonly FieldInfo numberOfPlayersField;

		// Not too sure what this is but it's often 0 or 1, and is used as an index in GameManager.Awake(), which should
		// highlight its purpose.
		public int SomeInt {
			get => (int)someIntField.GetValue(globalVariables);
			set => someIntField.SetValue(globalVariables, value);
		}
		[WrapperField("\u0318\u0310\u0317\u0311\u031A\u030F\u0318\u0315\u0312\u0310\u0315")]
		private static readonly FieldInfo someIntField;

		#endregion

		#region Methods

		public void LoadSettings() => loadSettingsMethod(globalVariables);
		[WrapperMethod("\u0317\u0313\u0316\u0311\u031B\u0318\u0317\u0314\u031C\u031A\u0319")]
		private static readonly FastInvokeHandler loadSettingsMethod;

		public void WriteSettings() => writeSettingsMethod(globalVariables);
		[WrapperMethod("\u0318\u031C\u0313\u0312\u0317\u0315\u030D\u0312\u030D\u031A\u0317")]
		private static readonly FastInvokeHandler writeSettingsMethod;

		#endregion
	}
}
