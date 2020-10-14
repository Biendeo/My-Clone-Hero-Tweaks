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
		public GlobalVariables GlobalVariables { get; private set; }

		public static GlobalVariablesWrapper Wrap(GlobalVariables globalVariables) => new GlobalVariablesWrapper {
			GlobalVariables = globalVariables
		};

		public override bool Equals(object obj) => GlobalVariables.Equals(obj);

		public override int GetHashCode() => GlobalVariables.GetHashCode();

		public bool IsNull() => GlobalVariables == null;

		#region Fields

		public static GlobalVariablesWrapper Instance {
			get => Wrap((GlobalVariables)instanceField.GetValue(null));
			set => instanceField.SetValue(null, value.GlobalVariables);
		}
		[WrapperField("\u0312\u0313\u0310\u0315\u030E\u0319\u030D\u0318\u0313\u030E\u031A")]
		private static readonly FieldInfo instanceField;

		public static bool IsSaving {
			get => (bool)isSavingField.GetValue(null);
			set => isSavingField.SetValue(null, value);
		}
		[WrapperField("\u0314\u0314\u0310\u031A\u031A\u0317\u0319\u0310\u0313\u030E\u0316")]
		private static readonly FieldInfo isSavingField;

		public static int ProfileFileVersion {
			get => (int)profileFileVersionField.GetValue(null);
			set => profileFileVersionField.SetValue(null, value);
		}
		[WrapperField("\u0311\u031C\u0313\u031A\u0314\u0311\u031C\u031C\u0310\u0312\u0315")]
		private static readonly FieldInfo profileFileVersionField;

		public string ScreenshotsPath {
			get => screenshotsPathField(GlobalVariables);
			set => screenshotsPathField(GlobalVariables) = value;
		}
		[WrapperField("\u030D\u030D\u0315\u0318\u031A\u0313\u031A\u0315\u0311\u0310\u0318")]
		private static readonly AccessTools.FieldRef<GlobalVariables, string> screenshotsPathField;

		public string SettingsPath {
			get => settingsPathField(GlobalVariables);
			set => settingsPathField(GlobalVariables) = value;
		}
		[WrapperField("\u031C\u0317\u030F\u030E\u030D\u030F\u031C\u0318\u031C\u0318\u0315")]
		private static readonly AccessTools.FieldRef<GlobalVariables, string> settingsPathField;

		public List<string> SongPaths {
			get => songPathsField(GlobalVariables);
			set => songPathsField(GlobalVariables) = value;
		}
		[WrapperField("\u0310\u031A\u0313\u030D\u031C\u0319\u0310\u0316\u0310\u0319\u0312")]
		private static readonly AccessTools.FieldRef<GlobalVariables, List<string>> songPathsField;

		public List<string> UniqueSongPaths {
			get => uniqueSongPathsField(GlobalVariables);
			set => uniqueSongPathsField(GlobalVariables) = value;
		}
		[WrapperField("\u031B\u0310\u0313\u0315\u0313\u0318\u0313\u0318\u0319\u030D\u0313")]
		private static readonly AccessTools.FieldRef<GlobalVariables, List<string>> uniqueSongPathsField;

		public string CustomSongExport {
			get => customSongExportField(GlobalVariables);
			set => customSongExportField(GlobalVariables) = value;
		}
		[WrapperField("\u031C\u0317\u030F\u030E\u030D\u030F\u031C\u0318\u031C\u0318\u0315")]
		private static readonly AccessTools.FieldRef<GlobalVariables, string> customSongExportField;

		public string BuildVersion {
			get => buildVersionField(GlobalVariables);
			set => buildVersionField(GlobalVariables) = value;
		}
		[WrapperField("\u030E\u030E\u0313\u030D\u0319\u031A\u030E\u0319\u0314\u0318\u031C")]
		private static readonly AccessTools.FieldRef<GlobalVariables, string> buildVersionField;

		public string[] Languages {
			get => languagesField(GlobalVariables);
			set => languagesField(GlobalVariables) = value;
		}
		[WrapperField("\u030F\u0313\u030E\u030D\u0318\u0311\u0312\u031C\u0313\u031C\u031A")]
		private static readonly AccessTools.FieldRef<GlobalVariables, string[]> languagesField;

		public string[] SplashMessages {
			get => splashMessagesField(GlobalVariables);
			set => splashMessagesField(GlobalVariables) = value;
		}
		[WrapperField("\u0312\u0316\u031C\u0311\u030F\u030F\u030D\u030E\u0314\u0316\u0315")]
		private static readonly AccessTools.FieldRef<GlobalVariables, string[]> splashMessagesField;

		public bool AprilFoolsMode {
			get => aprilFoolsModeField(GlobalVariables);
			set => aprilFoolsModeField(GlobalVariables) = value;
		}
		[WrapperField("\u0318\u0319\u0316\u0315\u0315\u0313\u0310\u0311\u030F\u0313\u030E")]
		private static readonly AccessTools.FieldRef<GlobalVariables, bool> aprilFoolsModeField;

		// Either sound device or SetWhammyFast?
		public GameSettingWrapper VolumeDevice {
			get => GameSettingWrapper.Wrap(volumeDeviceField(GlobalVariables));
			set => volumeDeviceField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u030D\u0314\u0319\u030F\u0311\u031A\u0315\u0313\u0311\u030F\u031A")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> volumeDeviceField;

		public GameSettingWrapper LowLatencyMode {
			get => GameSettingWrapper.Wrap(lowLatencyModeField(GlobalVariables));
			set => lowLatencyModeField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u030D\u0316\u0311\u0311\u031C\u0319\u0312\u0318\u030F\u031C\u0318")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> lowLatencyModeField;

		public GameSettingWrapper StreamerHighwayPlacement {
			get => GameSettingWrapper.Wrap(streamerHighwayPlacementField(GlobalVariables));
			set => streamerHighwayPlacementField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u030E\u030D\u030D\u0311\u030F\u031A\u0319\u031B\u031B\u0316\u0313")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> streamerHighwayPlacementField;

		public GameSettingWrapper GameShowBotScore {
			get => GameSettingWrapper.Wrap(gameShowBotScoreField(GlobalVariables));
			set => gameShowBotScoreField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u030E\u0310\u030E\u0312\u030F\u0311\u0311\u031B\u0316\u0319\u0316")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameShowBotScoreField;

		public GameSettingWrapper GameMuteOnMiss {
			get => GameSettingWrapper.Wrap(gameMuteOnMissField(GlobalVariables));
			set => gameMuteOnMissField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u030E\u0317\u030F\u0315\u0313\u0312\u0313\u0317\u0319\u030F\u0313")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameMuteOnMissField;

		public GameSettingWrapper GameFullPlaylist {
			get => GameSettingWrapper.Wrap(gameFullPlaylistField(GlobalVariables));
			set => gameFullPlaylistField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u030F\u030F\u0310\u0312\u0312\u031B\u0315\u0310\u0316\u031A\u0314")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameFullPlaylistField;

		public GameSettingWrapper VideoHighwayLightning {
			get => GameSettingWrapper.Wrap(videoHighwayLightningField(GlobalVariables));
			set => videoHighwayLightningField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u030F\u0310\u0314\u0314\u031C\u0313\u0319\u031C\u030E\u030F\u030E")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoHighwayLightningField;

		public GameSettingWrapper VideoFrameRate {
			get => GameSettingWrapper.Wrap(videoFrameRateField(GlobalVariables));
			set => videoFrameRateField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u030F\u0313\u0310\u0314\u0319\u030D\u0315\u0318\u0315\u0314\u0316")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoFrameRateField;

		public GameSettingWrapper GameEnableCursor {
			get => GameSettingWrapper.Wrap(gameEnableCursorField(GlobalVariables));
			set => gameEnableCursorField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0310\u030E\u030E\u0318\u0311\u0314\u0314\u0310\u0314\u0317\u0312")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameEnableCursorField;

		public GameSettingWrapper VideoVsync {
			get => GameSettingWrapper.Wrap(videoVsyncField(GlobalVariables));
			set => videoVsyncField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0310\u030F\u030F\u0314\u0315\u0311\u030E\u030F\u0313\u031C\u031C")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoVsyncField;

		public GameSettingWrapper OnlineShowRemoteNames {
			get => GameSettingWrapper.Wrap(onlineShowRemoteNamesField(GlobalVariables));
			set => onlineShowRemoteNamesField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0310\u0312\u030F\u030D\u0314\u0315\u0319\u0316\u030F\u031A\u030E")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> onlineShowRemoteNamesField;

		public GameSettingWrapper VolumePreviewVolume {
			get => GameSettingWrapper.Wrap(volumePreviewVolumeField(GlobalVariables));
			set => volumePreviewVolumeField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0310\u0315\u031A\u0310\u030D\u0315\u0311\u0314\u0317\u0315\u0318")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> volumePreviewVolumeField;

		public GameSettingWrapper CustomSongBackgrounds {
			get => GameSettingWrapper.Wrap(customSongBackgroundsField(GlobalVariables));
			set => customSongBackgroundsField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0310\u0315\u031B\u0316\u031B\u0316\u0315\u0316\u030F\u0310\u030F")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> customSongBackgroundsField;

		public GameSettingWrapper GameEnableLyrics {
			get => GameSettingWrapper.Wrap(gameEnableLyricsField(GlobalVariables));
			set => gameEnableLyricsField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0310\u0319\u031A\u0317\u0312\u0313\u031A\u030E\u0316\u030F\u031C")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameEnableLyricsField;

		public GameSettingWrapper GameLanguage {
			get => GameSettingWrapper.Wrap(gameLanguageField(GlobalVariables));
			set => gameLanguageField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0310\u031C\u030D\u031C\u030F\u0319\u031B\u0313\u0310\u0318\u0319")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameLanguageField;

		public GameSettingWrapper GameHighwayShake {
			get => GameSettingWrapper.Wrap(gameHighwayShakeField(GlobalVariables));
			set => gameHighwayShakeField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0311\u0310\u0310\u0313\u031A\u030E\u031C\u0318\u030F\u0310\u031B")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameHighwayShakeField;

		public GameSettingWrapper VolumeBackend {
			get => GameSettingWrapper.Wrap(volumeBackendField(GlobalVariables));
			set => volumeBackendField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0311\u0315\u030F\u0315\u030F\u0312\u031B\u0315\u0317\u0315\u031A")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> volumeBackendField;

		public GameSettingWrapper OffsetsVideo {
			get => GameSettingWrapper.Wrap(offsetsVideoField(GlobalVariables));
			set => offsetsVideoField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0311\u0316\u0316\u030F\u031B\u0314\u0310\u030E\u031A\u0312\u031B")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> offsetsVideoField;

		public GameSettingWrapper OnlineLowSongSpeed {
			get => GameSettingWrapper.Wrap(onlineLowSongSpeedField(GlobalVariables));
			set => onlineLowSongSpeedField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0311\u031A\u030D\u0312\u030F\u0316\u031A\u031A\u0319\u0310\u030F")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> onlineLowSongSpeedField;

		public GameSettingWrapper VideoStarAnimation {
			get => GameSettingWrapper.Wrap(videoStarAnimationField(GlobalVariables));
			set => videoStarAnimationField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0312\u0311\u0318\u0316\u030F\u031B\u031A\u0313\u031A\u030F\u0311")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoStarAnimationField;

		public GameSettingWrapper OffsetsAudio {
			get => GameSettingWrapper.Wrap(offsetsAudioField(GlobalVariables));
			set => offsetsAudioField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0312\u0314\u031B\u0318\u0318\u0312\u0319\u0311\u0315\u031C\u030E")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> offsetsAudioField;

		public GameSettingWrapper VideoMsaa {
			get => GameSettingWrapper.Wrap(videoMsaaField(GlobalVariables));
			set => videoMsaaField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0312\u0319\u0314\u0316\u0313\u0314\u030D\u0317\u031C\u030D\u0311")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoMsaaField;

		public GameSettingWrapper VideoDebugFPS {
			get => GameSettingWrapper.Wrap(videoDebugFPSField(GlobalVariables));
			set => videoDebugFPSField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0312\u0319\u0318\u031B\u031B\u0311\u031C\u0319\u031A\u0315\u0311")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoDebugFPSField;

		public GameSettingWrapper VolumeSounds {
			get => GameSettingWrapper.Wrap(volumeSoundsField(GlobalVariables));
			set => volumeSoundsField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0312\u031B\u0319\u0310\u0317\u0316\u030F\u031A\u0318\u031B\u031A")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> volumeSoundsField;

		public GameSettingWrapper SongSpeed {
			get => GameSettingWrapper.Wrap(songSpeedField(GlobalVariables));
			set => songSpeedField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0313\u030E\u0310\u031C\u0319\u0313\u0313\u031B\u0314\u0312\u0314")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> songSpeedField;

		public GameSettingWrapper OnlineHighwayPlacement {
			get => GameSettingWrapper.Wrap(onlineHighwayPlacementField(GlobalVariables));
			set => onlineHighwayPlacementField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0313\u030F\u030E\u0311\u0313\u031C\u0314\u0313\u0310\u0317\u031B")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> onlineHighwayPlacementField;

		public GameSettingWrapper GameMenuMusic {
			get => GameSettingWrapper.Wrap(gameMenuMusicField(GlobalVariables));
			set => gameMenuMusicField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0313\u0310\u0316\u0319\u031B\u031C\u030D\u030E\u030F\u0315\u0316")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameMenuMusicField;

		public GameSettingWrapper GameSortFilter {
			get => GameSettingWrapper.Wrap(gameSortFilterField(GlobalVariables));
			set => gameSortFilterField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0313\u0315\u0310\u0318\u031A\u0316\u0315\u0319\u0319\u0319\u030D")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameSortFilterField;

		public GameSettingWrapper VolumeMuteVolume {
			get => GameSettingWrapper.Wrap(volumeMuteVolumeField(GlobalVariables));
			set => volumeMuteVolumeField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0313\u0315\u0312\u030F\u031C\u0311\u0313\u0317\u0319\u0317\u0311")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> volumeMuteVolumeField;

		public GameSettingWrapper CustomBackgroundImage {
			get => GameSettingWrapper.Wrap(customBackgroundImageField(GlobalVariables));
			set => customBackgroundImageField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0313\u0318\u030E\u0315\u0319\u030E\u030E\u030E\u031C\u0319\u031A")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> customBackgroundImageField;

		public GameSettingWrapper GameShowHitWindow {
			get => GameSettingWrapper.Wrap(gameShowHitWindowField(GlobalVariables));
			set => gameShowHitWindowField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0314\u0312\u030F\u030E\u030F\u031C\u031A\u0316\u031C\u0317\u0317")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameShowHitWindowField;

		public GameSettingWrapper GamePauseOnFocusLost {
			get => GameSettingWrapper.Wrap(gamePauseOnFocusLostField(GlobalVariables));
			set => gamePauseOnFocusLostField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0314\u031B\u0313\u0310\u0318\u030F\u0311\u030E\u031A\u0316\u0319")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gamePauseOnFocusLostField;

		public GameSettingWrapper VideoHighwaySP {
			get => GameSettingWrapper.Wrap(videoHighwaySPField(GlobalVariables));
			set => videoHighwaySPField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0315\u0315\u0312\u0317\u0314\u031A\u031A\u030E\u0319\u0316\u031C")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoHighwaySPField;

		public GameSettingWrapper GameGemSize {
			get => GameSettingWrapper.Wrap(gameGemSizeField(GlobalVariables));
			set => gameGemSizeField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0316\u030D\u030D\u0310\u0313\u0317\u030F\u0314\u0310\u0319\u0314")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameGemSizeField;

		public GameSettingWrapper VideoFlames {
			get => GameSettingWrapper.Wrap(videoFlamesField(GlobalVariables));
			set => videoFlamesField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0316\u0310\u030D\u030F\u030D\u031A\u031B\u0312\u0315\u0314\u0315")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoFlamesField;

		public GameSettingWrapper VideoParticles {
			get => GameSettingWrapper.Wrap(videoParticlesField(GlobalVariables));
			set => videoParticlesField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0316\u0311\u030E\u0314\u0314\u0310\u0313\u0313\u0316\u0314\u0315")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoParticlesField;

		public GameSettingWrapper GamePollRate {
			get => GameSettingWrapper.Wrap(gamePollRateField(GlobalVariables));
			set => gamePollRateField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0316\u0318\u0312\u0314\u0317\u0318\u030D\u0312\u0311\u0314\u0318")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gamePollRateField;

		public GameSettingWrapper GameAllowDuplicateSongs {
			get => GameSettingWrapper.Wrap(gameAllowDuplicateSongsField(GlobalVariables));
			set => gameAllowDuplicateSongsField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0317\u0317\u0319\u0318\u030D\u0314\u0319\u0316\u031A\u0316\u0318")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameAllowDuplicateSongsField;

		public GameSettingWrapper VolumeMasterVolume {
			get => GameSettingWrapper.Wrap(volumeMasterVolumeField(GlobalVariables));
			set => volumeMasterVolumeField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0318\u030D\u0313\u031B\u0315\u030F\u030D\u0315\u030D\u0317\u030D")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> volumeMasterVolumeField;

		public GameSettingWrapper VideoNoteAnimation {
			get => GameSettingWrapper.Wrap(videoNoteAnimationField(GlobalVariables));
			set => videoNoteAnimationField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0318\u030F\u030E\u030D\u0310\u0319\u0315\u030D\u0315\u0314\u0315")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoNoteAnimationField;

		public GameSettingWrapper StreamerSongExport {
			get => GameSettingWrapper.Wrap(streamerSongExportField(GlobalVariables));
			set => streamerSongExportField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0319\u030D\u031A\u0314\u0315\u0316\u031B\u0312\u0316\u031A\u031A")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> streamerSongExportField;

		public GameSettingWrapper GameSongPreview {
			get => GameSettingWrapper.Wrap(gameSongPreviewField(GlobalVariables));
			set => gameSongPreviewField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0319\u0310\u030E\u030F\u030D\u0312\u0316\u0312\u031A\u0317\u0313")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameSongPreviewField;

		public GameSettingWrapper CustomSongVideos {
			get => GameSettingWrapper.Wrap(customSongVideosField(GlobalVariables));
			set => customSongVideosField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0319\u0318\u031B\u0313\u0318\u031B\u0313\u030F\u030E\u030E\u030E")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> customSongVideosField;

		public GameSettingWrapper VolumeMenuVolume {
			get => GameSettingWrapper.Wrap(volumeMenuVolumeField(GlobalVariables));
			set => volumeMenuVolumeField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u0319\u031B\u0319\u0317\u0310\u0316\u0319\u030F\u0317\u0316\u0319")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> volumeMenuVolumeField;

		public GameSettingWrapper CustomBackgroundVideo {
			get => GameSettingWrapper.Wrap(customBackgroundVideoField(GlobalVariables));
			set => customBackgroundVideoField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u031A\u030E\u0312\u0310\u0314\u0318\u0311\u030D\u030E\u030E\u030E")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> customBackgroundVideoField;

		public GameSettingWrapper GameNoFail {
			get => GameSettingWrapper.Wrap(gameNoFailField(GlobalVariables));
			set => gameNoFailField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u031A\u0312\u0312\u0313\u0316\u0315\u031B\u0318\u0316\u0318\u0311")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> gameNoFailField;

		public GameSettingWrapper OnlineSongsPerClient {
			get => GameSettingWrapper.Wrap(onlineSongsPerClientField(GlobalVariables));
			set => onlineSongsPerClientField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u031A\u0317\u031B\u0312\u030E\u0314\u030D\u031C\u0312\u030D\u031B")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> onlineSongsPerClientField;

		public GameSettingWrapper OnlineClientRemoveSongs {
			get => GameSettingWrapper.Wrap(onlineClientRemoveSongsField(GlobalVariables));
			set => onlineClientRemoveSongsField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u031A\u031B\u030F\u0312\u030F\u030E\u0317\u0319\u0311\u030E\u0316")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> onlineClientRemoveSongsField;

		public GameSettingWrapper VideoMenuBackground {
			get => GameSettingWrapper.Wrap(videoMenuBackgroundField(GlobalVariables));
			set => videoMenuBackgroundField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u031A\u031C\u0319\u0319\u031B\u0315\u030D\u0310\u0318\u0318\u0314")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoMenuBackgroundField;

		public GameSettingWrapper VideoSongDisplay {
			get => GameSettingWrapper.Wrap(videoSongDisplayField(GlobalVariables));
			set => videoSongDisplayField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u031B\u0316\u031B\u0312\u0318\u030E\u0316\u0318\u031B\u0310\u031B")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> videoSongDisplayField;

		public GameSettingWrapper OnlineMaxSongSpeed {
			get => GameSettingWrapper.Wrap(onlineMaxSongSpeedField(GlobalVariables));
			set => onlineMaxSongSpeedField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u031B\u0317\u0311\u031C\u0311\u0316\u0319\u030D\u0312\u0312\u0311")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> onlineMaxSongSpeedField;

		public GameSettingWrapper OnlineServerTickRate {
			get => GameSettingWrapper.Wrap(onlineServerTickRateField(GlobalVariables));
			set => onlineServerTickRateField(GlobalVariables) = value.GameSetting;
		}
		[WrapperField("\u031B\u0318\u0311\u0311\u031C\u031A\u030F\u0316\u0311\u0316\u0310")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> onlineServerTickRateField;

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
		public GameSettingWrapper[] VolumeStems {
			get => volumeStemsField(GlobalVariables).Select(o => GameSettingWrapper.Wrap(o)).ToArray();
			set => volumeStemsField(GlobalVariables) = value.Select(o => o.GameSetting).ToArray();
		}
		[WrapperField("\u031B\u0315\u031B\u0319\u030D\u0313\u0319\u0317\u031A\u0318\u0312")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object[]> volumeStemsField;

		public bool IsInPracticeMode {
			get => isInPracticeModeField(GlobalVariables);
			set => isInPracticeModeField(GlobalVariables) = value;
		}
		[WrapperField("\u0318\u030E\u0310\u0315\u0313\u0318\u031C\u0318\u030E\u0313\u031A")]
		private static readonly AccessTools.FieldRef<GlobalVariables, bool> isInPracticeModeField;

		public string CachePath {
			get => cachePathField(GlobalVariables);
			set => cachePathField(GlobalVariables) = value;
		}
		[WrapperField("\u0315\u0312\u0315\u0314\u0319\u0316\u0312\u030D\u030D\u031A\u030E")]
		private static readonly AccessTools.FieldRef<GlobalVariables, string> cachePathField;

		// Just duplicates SongEntry, so use that instead.
		public object SongEntryObject {
			get => songEntryObjectField(GlobalVariables);
			set => songEntryObjectField(GlobalVariables) = value;
		}
		[WrapperField("\u0317\u0313\u0311\u0317\u031B\u0314\u0314\u0313\u031B\u0315\u0317")]
		private static readonly AccessTools.FieldRef<GlobalVariables, object> songEntryObjectField;

		// Changes when selecting the instrument after deciding a song.
		public SongEntryWrapper SongEntry {
			get => SongEntryWrapper.Wrap(songEntryField(GlobalVariables));
			set => songEntryField(GlobalVariables) = value.SongEntry;
		}
		[WrapperField("\u0310\u030E\u0313\u0310\u0313\u0314\u030D\u031B\u0313\u031C\u0313")]
		private static readonly AccessTools.FieldRef<GlobalVariables, SongEntry> songEntryField;

		// Always true, but it seems to control background music on app focus.
		// There's an existing menu music setting though.
		//TODO: Is this an appropriate name?
		public bool IsBackgroundMusicEnabled {
			get => isBackgroundMusicEnabledField(GlobalVariables);
			set => isBackgroundMusicEnabledField(GlobalVariables) = value;
		}
		[WrapperField("\u030E\u0318\u0314\u0314\u0313\u031C\u0316\u0314\u031B\u0310\u0313")]
		private static readonly AccessTools.FieldRef<GlobalVariables, bool> isBackgroundMusicEnabledField;

		public int NumberOfPlayers {
			get => numberOfPlayersField(GlobalVariables);
			set => numberOfPlayersField(GlobalVariables) = value;
		}
		[WrapperField("\u0317\u030D\u0310\u030E\u030D\u0317\u0315\u031A\u0316\u0312\u030D")]
		private static readonly AccessTools.FieldRef<GlobalVariables, int> numberOfPlayersField;

		// Not too sure what this is but it's often 0 or 1, and is used as an index in GameManager.Awake(), which should
		// highlight its purpose.
		public int SomeInt {
			get => someIntField(GlobalVariables);
			set => someIntField(GlobalVariables) = value;
		}
		[WrapperField("\u0318\u0310\u0317\u0311\u031A\u030F\u0318\u0315\u0312\u0310\u0315")]
		private static readonly AccessTools.FieldRef<GlobalVariables, int> someIntField;

		#endregion

		#region Methods

		public void LoadSettings() => loadSettingsMethod(GlobalVariables);
		[WrapperMethod("\u0317\u0313\u0316\u0311\u031B\u0318\u0317\u0314\u031C\u031A\u0319")]
		private static readonly FastInvokeHandler loadSettingsMethod;

		public void WriteSettings() => writeSettingsMethod(GlobalVariables);
		[WrapperMethod("\u0318\u031C\u0313\u0312\u0317\u0315\u030D\u0312\u030D\u031A\u0317")]
		private static readonly FastInvokeHandler writeSettingsMethod;

		#endregion
	}
}
