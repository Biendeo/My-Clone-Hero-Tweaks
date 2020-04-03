using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrappers {
	[Wrapper(typeof(GlobalVariables))]
	internal class GlobalVariablesWrapper {
		public readonly GlobalVariables globalVariables;

		public GlobalVariablesWrapper(GlobalVariables globalVariables) {
			this.globalVariables = globalVariables;
		}

		#region Fields

		public static GlobalVariablesWrapper instance => new GlobalVariablesWrapper((GlobalVariables)instanceField.GetValue(null));
		[WrapperField("\u0312\u0313\u0310\u0315\u030E\u0319\u030D\u0318\u0313\u030E\u031A")]
		private static readonly FieldInfo instanceField;

		public static bool isSaving => (bool)isSavingField.GetValue(null);
		[WrapperField("\u0314\u0314\u0310\u031A\u031A\u0317\u0319\u0310\u0313\u030E\u0316")]
		private static readonly FieldInfo isSavingField;

		public static int profileFileVersion => (int)profileFileVersionField.GetValue(null);
		[WrapperField("\u0311\u031C\u0313\u031A\u0314\u0311\u031C\u031C\u0310\u0312\u0315")]
		private static readonly FieldInfo profileFileVersionField;

		public string screenshotsPath => (string)screenshotsPathField.GetValue(globalVariables);
		[WrapperField("\u030D\u030D\u0315\u0318\u031A\u0313\u031A\u0315\u0311\u0310\u0318")]
		private static readonly FieldInfo screenshotsPathField;

		public string settingsPath => (string)settingsPathField.GetValue(globalVariables);
		[WrapperField("\u031C\u0317\u030F\u030E\u030D\u030F\u031C\u0318\u031C\u0318\u0315")]
		private static readonly FieldInfo settingsPathField;

		public List<string> songPaths => (List<string>)songPathsField.GetValue(globalVariables);
		[WrapperField("\u0310\u031A\u0313\u030D\u031C\u0319\u0310\u0316\u0310\u0319\u0312")]
		private static readonly FieldInfo songPathsField;

		public List<string> uniqueSongPaths => (List<string>)uniqueSongPathsField.GetValue(globalVariables);
		[WrapperField("\u031B\u0310\u0313\u0315\u0313\u0318\u0313\u0318\u0319\u030D\u0313")]
		private static readonly FieldInfo uniqueSongPathsField;

		public string customSongExport => (string)customSongExportField.GetValue(globalVariables);
		[WrapperField("\u031C\u0317\u030F\u030E\u030D\u030F\u031C\u0318\u031C\u0318\u0315")]
		private static readonly FieldInfo customSongExportField;

		public string buildVersion => (string)buildVersionField.GetValue(globalVariables);
		[WrapperField("\u030E\u030E\u0313\u030D\u0319\u031A\u030E\u0319\u0314\u0318\u031C")]
		private static readonly FieldInfo buildVersionField;

		public string[] languages => (string[])languagesField.GetValue(globalVariables);
		[WrapperField("\u0312\u0319\u0318\u0315\u031A\u031A\u0310\u030D\u0313\u0318\u031A")]
		private static readonly FieldInfo languagesField;

		// Either sound device or SetWhammyFast?
		public GameSettingWrapper volumeDevice => new GameSettingWrapper(volumeDeviceField.GetValue(globalVariables));
		[WrapperField("\u030D\u0314\u0319\u030F\u0311\u031A\u0315\u0313\u0311\u030F\u031A")]
		private static readonly FieldInfo volumeDeviceField;

		public GameSettingWrapper lowLatencyMode => new GameSettingWrapper(lowLatencyModeField.GetValue(globalVariables));
		[WrapperField("\u030D\u0316\u0311\u0311\u031C\u0319\u0312\u0318\u030F\u031C\u0318")]
		private static readonly FieldInfo lowLatencyModeField;

		public GameSettingWrapper streamerHighwayPlacement => new GameSettingWrapper(streamerHighwayPlacementField.GetValue(globalVariables));
		[WrapperField("\u030E\u030D\u030D\u0311\u030F\u031A\u0319\u031B\u031B\u0316\u0313")]
		private static readonly FieldInfo streamerHighwayPlacementField;

		public GameSettingWrapper gameShowBotScore => new GameSettingWrapper(gameShowBotScoreField.GetValue(globalVariables));
		[WrapperField("\u030E\u0310\u030E\u0312\u030F\u0311\u0311\u031B\u0316\u0319\u0316")]
		private static readonly FieldInfo gameShowBotScoreField;

		public GameSettingWrapper gameMuteOnMiss => new GameSettingWrapper(gameMuteOnMissField.GetValue(globalVariables));
		[WrapperField("\u030E\u0317\u030F\u0315\u0313\u0312\u0313\u0317\u0319\u030F\u0313")]
		private static readonly FieldInfo gameMuteOnMissField;

		public GameSettingWrapper gameFullPlaylist => new GameSettingWrapper(gameFullPlaylistField.GetValue(globalVariables));
		[WrapperField("\u030F\u030F\u0310\u0312\u0312\u031B\u0315\u0310\u0316\u031A\u0314")]
		private static readonly FieldInfo gameFullPlaylistField;

		public GameSettingWrapper videoHighwayLightning => new GameSettingWrapper(videoHighwayLightningField.GetValue(globalVariables));
		[WrapperField("\u030F\u0310\u0314\u0314\u031C\u0313\u0319\u031C\u030E\u030F\u030E")]
		private static readonly FieldInfo videoHighwayLightningField;

		public GameSettingWrapper videoFrameRate => new GameSettingWrapper(videoFrameRateField.GetValue(globalVariables));
		[WrapperField("\u030F\u0313\u0310\u0314\u0319\u030D\u0315\u0318\u0315\u0314\u0316")]
		private static readonly FieldInfo videoFrameRateField;

		public GameSettingWrapper gameEnableCursor => new GameSettingWrapper(gameEnableCursorField.GetValue(globalVariables));
		[WrapperField("\u0310\u030E\u030E\u0318\u0311\u0314\u0314\u0310\u0314\u0317\u0312")]
		private static readonly FieldInfo gameEnableCursorField;

		public GameSettingWrapper videoVsync => new GameSettingWrapper(videoVsyncField.GetValue(globalVariables));
		[WrapperField("\u0310\u030F\u030F\u0314\u0315\u0311\u030E\u030F\u0313\u031C\u031C")]
		private static readonly FieldInfo videoVsyncField;

		public GameSettingWrapper onlineShowRemoteNames => new GameSettingWrapper(onlineShowRemoteNamesField.GetValue(globalVariables));
		[WrapperField("\u0310\u0312\u030F\u030D\u0314\u0315\u0319\u0316\u030F\u031A\u030E")]
		private static readonly FieldInfo onlineShowRemoteNamesField;

		public GameSettingWrapper volumePreviewVolume => new GameSettingWrapper(volumePreviewVolumeField.GetValue(globalVariables));
		[WrapperField("\u0310\u0315\u031A\u0310\u030D\u0315\u0311\u0314\u0317\u0315\u0318")]
		private static readonly FieldInfo volumePreviewVolumeField;

		public GameSettingWrapper customSongBackgrounds => new GameSettingWrapper(customSongBackgroundsField.GetValue(globalVariables));
		[WrapperField("\u0310\u0315\u031B\u0316\u031B\u0316\u0315\u0316\u030F\u0310\u030F")]
		private static readonly FieldInfo customSongBackgroundsField;

		public GameSettingWrapper gameEnableLyrics => new GameSettingWrapper(gameEnableLyricsField.GetValue(globalVariables));
		[WrapperField("\u0310\u0319\u031A\u0317\u0312\u0313\u031A\u030E\u0316\u030F\u031C")]
		private static readonly FieldInfo gameEnableLyricsField;

		public GameSettingWrapper gameLanguage => new GameSettingWrapper(gameLanguageField.GetValue(globalVariables));
		[WrapperField("\u0310\u031C\u030D\u031C\u030F\u0319\u031B\u0313\u0310\u0318\u0319")]
		private static readonly FieldInfo gameLanguageField;

		public GameSettingWrapper gameHighwayShake => new GameSettingWrapper(gameHighwayShakeField.GetValue(globalVariables));
		[WrapperField("\u0311\u0310\u0310\u0313\u031A\u030E\u031C\u0318\u030F\u0310\u031B")]
		private static readonly FieldInfo gameHighwayShakeField;

		public GameSettingWrapper volumeBackend => new GameSettingWrapper(volumeBackendField.GetValue(globalVariables));
		[WrapperField("\u0311\u0315\u030F\u0315\u030F\u0312\u031B\u0315\u0317\u0315\u031A")]
		private static readonly FieldInfo volumeBackendField;

		public GameSettingWrapper offsetsVideo => new GameSettingWrapper(offsetsVideoField.GetValue(globalVariables));
		[WrapperField("\u0311\u0316\u0316\u030F\u031B\u0314\u0310\u030E\u031A\u0312\u031B")]
		private static readonly FieldInfo offsetsVideoField;

		public GameSettingWrapper onlineLowSongSpeed => new GameSettingWrapper(onlineLowSongSpeedField.GetValue(globalVariables));
		[WrapperField("\u0311\u031A\u030D\u0312\u030F\u0316\u031A\u031A\u0319\u0310\u030F")]
		private static readonly FieldInfo onlineLowSongSpeedField;

		public GameSettingWrapper videoStarAnimation => new GameSettingWrapper(videoStarAnimationField.GetValue(globalVariables));
		[WrapperField("\u0312\u0311\u0318\u0316\u030F\u031B\u031A\u0313\u031A\u030F\u0311")]
		private static readonly FieldInfo videoStarAnimationField;

		public GameSettingWrapper offsetsAudio => new GameSettingWrapper(offsetsAudioField.GetValue(globalVariables));
		[WrapperField("\u0312\u0314\u031B\u0318\u0318\u0312\u0319\u0311\u0315\u031C\u030E")]
		private static readonly FieldInfo offsetsAudioField;

		public GameSettingWrapper videoMsaa => new GameSettingWrapper(videoMsaaField.GetValue(globalVariables));
		[WrapperField("\u0312\u0319\u0314\u0316\u0313\u0314\u030D\u0317\u031C\u030D\u0311")]
		private static readonly FieldInfo videoMsaaField;

		public GameSettingWrapper videoDebugFPS => new GameSettingWrapper(videoDebugFPSField.GetValue(globalVariables));
		[WrapperField("\u0312\u0319\u0318\u031B\u031B\u0311\u031C\u0319\u031A\u0315\u0311")]
		private static readonly FieldInfo videoDebugFPSField;

		public GameSettingWrapper volumeSounds => new GameSettingWrapper(volumeSoundsField.GetValue(globalVariables));
		[WrapperField("\u0312\u031B\u0319\u0310\u0317\u0316\u030F\u031A\u0318\u031B\u031A")]
		private static readonly FieldInfo volumeSoundsField;

		public GameSettingWrapper songSpeed => new GameSettingWrapper(songSpeedField.GetValue(globalVariables));
		[WrapperField("\u0313\u030E\u0310\u031C\u0319\u0313\u0313\u031B\u0314\u0312\u0314")]
		private static readonly FieldInfo songSpeedField;

		public GameSettingWrapper onlineHighwayPlacement => new GameSettingWrapper(onlineHighwayPlacementField.GetValue(globalVariables));
		[WrapperField("\u0313\u030F\u030E\u0311\u0313\u031C\u0314\u0313\u0310\u0317\u031B")]
		private static readonly FieldInfo onlineHighwayPlacementField;

		public GameSettingWrapper gameMenuMusic => new GameSettingWrapper(gameMenuMusicField.GetValue(globalVariables));
		[WrapperField("\u0313\u0310\u0316\u0319\u031B\u031C\u030D\u030E\u030F\u0315\u0316")]
		private static readonly FieldInfo gameMenuMusicField;

		public GameSettingWrapper gameSortFilter => new GameSettingWrapper(gameSortFilterField.GetValue(globalVariables));
		[WrapperField("\u0313\u0315\u0310\u0318\u031A\u0316\u0315\u0319\u0319\u0319\u030D")]
		private static readonly FieldInfo gameSortFilterField;

		public GameSettingWrapper volumeMuteVolume => new GameSettingWrapper(volumeMuteVolumeField.GetValue(globalVariables));
		[WrapperField("\u0313\u0315\u0312\u030F\u031C\u0311\u0313\u0317\u0319\u0317\u0311")]
		private static readonly FieldInfo volumeMuteVolumeField;

		public GameSettingWrapper customBackgroundImage => new GameSettingWrapper(customBackgroundImageField.GetValue(globalVariables));
		[WrapperField("\u0313\u0318\u030E\u0315\u0319\u030E\u030E\u030E\u031C\u0319\u031A")]
		private static readonly FieldInfo customBackgroundImageField;

		public GameSettingWrapper gameShowHitWindow => new GameSettingWrapper(gameShowHitWindowField.GetValue(globalVariables));
		[WrapperField("\u0314\u0312\u030F\u030E\u030F\u031C\u031A\u0316\u031C\u0317\u0317")]
		private static readonly FieldInfo gameShowHitWindowField;

		public GameSettingWrapper gamePauseOnFocusLost => new GameSettingWrapper(gamePauseOnFocusLostField.GetValue(globalVariables));
		[WrapperField("\u0314\u031B\u0313\u0310\u0318\u030F\u0311\u030E\u031A\u0316\u0319")]
		private static readonly FieldInfo gamePauseOnFocusLostField;

		public GameSettingWrapper videoHighwaySP => new GameSettingWrapper(videoHighwaySPField.GetValue(globalVariables));
		[WrapperField("\u0315\u0315\u0312\u0317\u0314\u031A\u031A\u030E\u0319\u0316\u031C")]
		private static readonly FieldInfo videoHighwaySPField;

		public GameSettingWrapper gameGemSize => new GameSettingWrapper(gameGemSizeField.GetValue(globalVariables));
		[WrapperField("\u0316\u030D\u030D\u0310\u0313\u0317\u030F\u0314\u0310\u0319\u0314")]
		private static readonly FieldInfo gameGemSizeField;

		public GameSettingWrapper videoFlames => new GameSettingWrapper(videoFlamesField.GetValue(globalVariables));
		[WrapperField("\u0316\u0310\u030D\u030F\u030D\u031A\u031B\u0312\u0315\u0314\u0315")]
		private static readonly FieldInfo videoFlamesField;

		public GameSettingWrapper videoParticles => new GameSettingWrapper(videoParticlesField.GetValue(globalVariables));
		[WrapperField("\u0316\u0311\u030E\u0314\u0314\u0310\u0313\u0313\u0316\u0314\u0315")]
		private static readonly FieldInfo videoParticlesField;

		public GameSettingWrapper gamePollRate => new GameSettingWrapper(gamePollRateField.GetValue(globalVariables));
		[WrapperField("\u0316\u0318\u0312\u0314\u0317\u0318\u030D\u0312\u0311\u0314\u0318")]
		private static readonly FieldInfo gamePollRateField;

		public GameSettingWrapper gameAllowDuplicateSongs => new GameSettingWrapper(gameAllowDuplicateSongsField.GetValue(globalVariables));
		[WrapperField("\u0317\u0317\u0319\u0318\u030D\u0314\u0319\u0316\u031A\u0316\u0318")]
		private static readonly FieldInfo gameAllowDuplicateSongsField;

		public GameSettingWrapper volumeMasterVolume => new GameSettingWrapper(volumeMasterVolumeField.GetValue(globalVariables));
		[WrapperField("\u0318\u030D\u0313\u031B\u0315\u030F\u030D\u0315\u030D\u0317\u030D")]
		private static readonly FieldInfo volumeMasterVolumeField;

		public GameSettingWrapper videoNoteAnimation => new GameSettingWrapper(videoNoteAnimationField.GetValue(globalVariables));
		[WrapperField("\u0318\u030F\u030E\u030D\u0310\u0319\u0315\u030D\u0315\u0314\u0315")]
		private static readonly FieldInfo videoNoteAnimationField;

		public GameSettingWrapper streamerSongExport => new GameSettingWrapper(streamerSongExportField.GetValue(globalVariables));
		[WrapperField("\u0319\u030D\u031A\u0314\u0315\u0316\u031B\u0312\u0316\u031A\u031A")]
		private static readonly FieldInfo streamerSongExportField;

		public GameSettingWrapper gameSongPreview => new GameSettingWrapper(gameSongPreviewField.GetValue(globalVariables));
		[WrapperField("\u0319\u0310\u030E\u030F\u030D\u0312\u0316\u0312\u031A\u0317\u0313")]
		private static readonly FieldInfo gameSongPreviewField;

		public GameSettingWrapper customSongVideos => new GameSettingWrapper(customSongVideosField.GetValue(globalVariables));
		[WrapperField("\u0319\u0318\u031B\u0313\u0318\u031B\u0313\u030F\u030E\u030E\u030E")]
		private static readonly FieldInfo customSongVideosField;

		public GameSettingWrapper volumeMenuVolume => new GameSettingWrapper(volumeMenuVolumeField.GetValue(globalVariables));
		[WrapperField("\u0319\u031B\u0319\u0317\u0310\u0316\u0319\u030F\u0317\u0316\u0319")]
		private static readonly FieldInfo volumeMenuVolumeField;

		public GameSettingWrapper customBackgroundVideo => new GameSettingWrapper(customBackgroundVideoField.GetValue(globalVariables));
		[WrapperField("\u031A\u030E\u0312\u0310\u0314\u0318\u0311\u030D\u030E\u030E\u030E")]
		private static readonly FieldInfo customBackgroundVideoField;

		public GameSettingWrapper gameNoFail => new GameSettingWrapper(gameNoFailField.GetValue(globalVariables));
		[WrapperField("\u031A\u0312\u0312\u0313\u0316\u0315\u031B\u0318\u0316\u0318\u0311")]
		private static readonly FieldInfo gameNoFailField;

		public GameSettingWrapper onlineSongsPerClient => new GameSettingWrapper(onlineSongsPerClientField.GetValue(globalVariables));
		[WrapperField("\u031A\u0317\u031B\u0312\u030E\u0314\u030D\u031C\u0312\u030D\u031B")]
		private static readonly FieldInfo onlineSongsPerClientField;

		public GameSettingWrapper onlineClientRemoveSongs => new GameSettingWrapper(onlineClientRemoveSongsField.GetValue(globalVariables));
		[WrapperField("\u031A\u031B\u030F\u0312\u030F\u030E\u0317\u0319\u0311\u030E\u0316")]
		private static readonly FieldInfo onlineClientRemoveSongsField;

		public GameSettingWrapper videoMenuBackground => new GameSettingWrapper(videoMenuBackgroundField.GetValue(globalVariables));
		[WrapperField("\u031A\u031C\u0319\u0319\u031B\u0315\u030D\u0310\u0318\u0318\u0314")]
		private static readonly FieldInfo videoMenuBackgroundField;

		public GameSettingWrapper videoSongDisplay => new GameSettingWrapper(videoSongDisplayField.GetValue(globalVariables));
		[WrapperField("\u031B\u0316\u031B\u0312\u0318\u030E\u0316\u0318\u031B\u0310\u031B")]
		private static readonly FieldInfo videoSongDisplayField;

		public GameSettingWrapper onlineMaxSongSpeed => new GameSettingWrapper(onlineMaxSongSpeedField.GetValue(globalVariables));
		[WrapperField("\u031B\u0317\u0311\u031C\u0311\u0316\u0319\u030D\u0312\u0312\u0311")]
		private static readonly FieldInfo onlineMaxSongSpeedField;

		public GameSettingWrapper onlineServerTickRate => new GameSettingWrapper(onlineServerTickRateField.GetValue(globalVariables));
		[WrapperField("\u031B\u0318\u0311\u0311\u031C\u031A\u030F\u0316\u0311\u0316\u0310")]
		private static readonly FieldInfo onlineServerTickRateField;

		public GameSettingWrapper[] volumeStems => ((object[])volumeStemsField.GetValue(globalVariables)).Select(bp => new GameSettingWrapper(bp)).ToArray();
		[WrapperField("\u031B\u0315\u031B\u0319\u030D\u0313\u0319\u0317\u031A\u0318\u0312")]
		private static readonly FieldInfo volumeStemsField;

		public bool isInPracticeMode => (bool)isInPracticeModeField.GetValue(globalVariables);
		[WrapperField("\u0318\u030E\u0310\u0315\u0313\u0318\u031C\u0318\u030E\u0313\u031A")]
		private static readonly FieldInfo isInPracticeModeField;

		public string cachePath => (string)cachePathField.GetValue(globalVariables);
		[WrapperField("\u0315\u0312\u0315\u0314\u0319\u0316\u0312\u030D\u030D\u031A\u030E")]
		private static readonly FieldInfo cachePathField;

		#endregion

		#region Methods

		public void LoadSettings() => loadSettingsMethod.Invoke(globalVariables, Array.Empty<object>());
		[WrapperMethod("\u0317\u0313\u0316\u0311\u031B\u0318\u0317\u0314\u031C\u031A\u0319")]
		private static readonly MethodInfo loadSettingsMethod;

		// This version is really wrong and never gets called anywhere.
		public void ObfuscatedLoadSettings() => obfuscatedLoadSettingsMethod.Invoke(globalVariables, Array.Empty<object>());
		[WrapperMethod("\u031A\u031C\u0315\u031B\u0313\u030E\u0314\u0312\u0318\u0319\u031A")]
		private static readonly MethodInfo obfuscatedLoadSettingsMethod;

		public void WriteSettings() => writeSettingsMethod.Invoke(globalVariables, Array.Empty<object>());
		[WrapperMethod("\u0318\u031C\u0313\u0312\u0317\u0315\u030D\u0312\u030D\u031A\u0317")]
		private static readonly MethodInfo writeSettingsMethod;

		#endregion
	}
}
