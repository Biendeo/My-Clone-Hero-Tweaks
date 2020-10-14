using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(SongScan))]
	public struct SongScanWrapper {
		public SongScan SongScan { get; private set; }

		public static SongScanWrapper Wrap(SongScan songScan) => new SongScanWrapper {
			SongScan = songScan
		};

		public override bool Equals(object obj) => SongScan.Equals(obj);

		public override int GetHashCode() => SongScan.GetHashCode();

		public bool IsNull() => SongScan == null;

		#region Constructors

		// The constructor probably expects a prefab to instantiate some fields so don't expect this to work.
		public static SongScanWrapper Construct() => new SongScanWrapper {
			SongScan = (SongScan)defaultConstructor.Invoke(Array.Empty<object>())
		};
		[WrapperConstructor]
		private static readonly ConstructorInfo defaultConstructor;

		#endregion

		#region Fields

		// Seems to match WaitForFinish
		public bool IsScanning {
			get => isScanningField(SongScan);
			set => isScanningField(SongScan) = value;
		}
		[WrapperField("\u0318\u0316\u0315\u0313\u031B\u0310\u031A\u0311\u0314\u0317\u030F")]
		private static readonly AccessTools.FieldRef<SongScan, bool> isScanningField;

		public Coroutine Coroutine {
			get => coroutineField(SongScan);
			set => coroutineField(SongScan) = value;
		}
		[WrapperField("\u0319\u031A\u031C\u0315\u0317\u0319\u0314\u0317\u031A\u0316\u0314")]
		private static readonly AccessTools.FieldRef<SongScan, Coroutine> coroutineField;

		public TextMeshProUGUI CountText {
			get => countTextField(SongScan);
			set => countTextField(SongScan) = value;
		}
		[WrapperField("countText")]
		private static readonly AccessTools.FieldRef<SongScan, TextMeshProUGUI> countTextField;

		public TextMeshProUGUI MainText {
			get => mainTextField(SongScan);
			set => mainTextField(SongScan) = value;
		}
		[WrapperField("mainText")]
		private static readonly AccessTools.FieldRef<SongScan, TextMeshProUGUI> mainTextField;

		public TextMeshProUGUI FolderText {
			get => folderTextField(SongScan);
			set => folderTextField(SongScan) = value;
		}
		[WrapperField("folderText")]
		private static readonly AccessTools.FieldRef<SongScan, TextMeshProUGUI> folderTextField;

		public TextMeshProUGUI ErrorText {
			get => errorTextField(SongScan);
			set => errorTextField(SongScan) = value;
		}
		[WrapperField("errorText")]
		private static readonly AccessTools.FieldRef<SongScan, TextMeshProUGUI> errorTextField;

		public TextMeshProUGUI BadSongsText {
			get => badSongsTextField(SongScan);
			set => badSongsTextField(SongScan) = value;
		}
		[WrapperField("badSongsText")]
		private static readonly AccessTools.FieldRef<SongScan, TextMeshProUGUI> badSongsTextField;

		public SongSelectWrapper SongSelect {
			get => SongSelectWrapper.Wrap(songSelectField(SongScan));
			set => songSelectField(SongScan) = value.SongSelect;
		}
		[WrapperField("songSelect")]
		private static readonly AccessTools.FieldRef<SongScan, SongSelect> songSelectField;

		public MainMenuWrapper MainMenu {
			get => MainMenuWrapper.Wrap(mainMenuField(SongScan));
			set => mainMenuField(SongScan) = value.MainMenu;
		}
		[WrapperField("mainMenu")]
		private static readonly AccessTools.FieldRef<SongScan, MainMenu> mainMenuField;

		public GameObject Container {
			get => containerField(SongScan);
			set => containerField(SongScan) = value;
		}
		[WrapperField("container")]
		private static readonly AccessTools.FieldRef<SongScan, GameObject> containerField;

		public bool WaitForFinish {
			get => waitForFinishField(SongScan);
			set => waitForFinishField(SongScan) = value;
		}
		[WrapperField("\u031A\u031C\u0310\u0316\u0313\u0315\u0313\u0318\u031A\u0312\u031C")]
		private static readonly AccessTools.FieldRef<SongScan, bool> waitForFinishField;

		public Thread ScanThread {
			get => scanThreadField(SongScan);
			set => scanThreadField(SongScan) = value;
		}
		[WrapperField("\u030D\u0319\u030F\u0314\u030D\u0310\u030F\u0316\u0317\u030E\u0314")]
		private static readonly AccessTools.FieldRef<SongScan, Thread> scanThreadField;

		public string FolderLocalized {
			get => folderLocalizedField(SongScan);
			set => folderLocalizedField(SongScan) = value;
		}
		[WrapperField("\u0317\u0311\u031A\u0319\u0313\u031A\u0316\u0313\u030D\u0311\u0313")]
		private static readonly AccessTools.FieldRef<SongScan, string> folderLocalizedField;

		public string SongsScannedLocalized {
			get => songsScannedLocalizedField(SongScan);
			set => songsScannedLocalizedField(SongScan) = value;
		}
		[WrapperField("\u0311\u030E\u0319\u0316\u0312\u0318\u0311\u030F\u0310\u031C\u031C")]
		private static readonly AccessTools.FieldRef<SongScan, string> songsScannedLocalizedField;

		public string ErrorsLocalized {
			get => errorsLocalizedField(SongScan);
			set => errorsLocalizedField(SongScan) = value;
		}
		[WrapperField("\u030E\u0310\u0313\u0314\u031C\u031C\u031A\u0315\u031C\u031B\u030E")]
		private static readonly AccessTools.FieldRef<SongScan, string> errorsLocalizedField;

		public string BadSongsLocalized {
			get => badSongsLocalizedField(SongScan);
			set => badSongsLocalizedField(SongScan) = value;
		}
		[WrapperField("\u0311\u0319\u031A\u030F\u030D\u0316\u0318\u0317\u0318\u030E\u031A")]
		private static readonly AccessTools.FieldRef<SongScan, string> badSongsLocalizedField;

		#endregion

		#region Methods

		public IEnumerator ScanForSongsThread(bool fullScan) => (IEnumerator)scanForSongsThreadMethod(SongScan, fullScan);
		[WrapperMethod("\u031B\u031C\u031B\u030D\u0315\u031B\u031B\u0313\u0318\u0319\u0314")]
		private static readonly FastInvokeHandler scanForSongsThreadMethod;

		public void DisplayStatus(CacheWrapper songCache) => displayStatusMethod(SongScan, songCache.Cache);
		[WrapperMethod("\u0317\u0312\u031B\u0314\u0310\u0314\u0315\u0314\u0311\u0311\u031C")]
		private static readonly FastInvokeHandler displayStatusMethod;

		public void OnApplicationQuit() => onApplicationQuitMethod(SongScan);
		[WrapperMethod("OnApplicationQuit")]
		private static readonly FastInvokeHandler onApplicationQuitMethod;

		public void AbortScan() => abortScanMethod(SongScan);
		[WrapperMethod("\u0312\u0318\u0311\u0316\u0316\u0313\u0314\u030E\u031B\u0310\u0317")]
		private static readonly FastInvokeHandler abortScanMethod;

		public void InitializeScanSettings() => initializeScanSettingsMethod(SongScan);
		[WrapperMethod("\u0319\u0317\u0316\u0314\u030F\u0319\u0318\u0319\u0311\u0310\u0314")]
		private static readonly FastInvokeHandler initializeScanSettingsMethod;

		public Coroutine StartScan(bool fullScan) => (Coroutine)startScanMethod(SongScan, fullScan);
		[WrapperMethod("\u031C\u0311\u0310\u030D\u0315\u0315\u031B\u030D\u0313\u0310\u0312")]
		private static readonly FastInvokeHandler startScanMethod;

		#endregion

		#region Duplicate Methods
#pragma warning disable IDE0051, CS0169 // Remove unused private members

		// These three methods are all identical to the main one, including which functions they do call.
		[WrapperMethod("\u030E\u030E\u0315\u0316\u030F\u0316\u0315\u0312\u031C\u0314\u030D")]
		private static readonly FastInvokeHandler scanForSongsThreadMethodDuplicate1;

		[WrapperMethod("\u031A\u0318\u030F\u0318\u0318\u031A\u031A\u030E\u0318\u0318\u030E")]
		private static readonly FastInvokeHandler scanForSongsThreadMethodDuplicate2;

		[WrapperMethod("\u0316\u0310\u0317\u030D\u030F\u030D\u030F\u0316\u0318\u031A\u0313")]
		private static readonly FastInvokeHandler scanForSongsThreadMethodDuplicate3;

		[WrapperMethod("\u030E\u030D\u0319\u030F\u031C\u030F\u030E\u0311\u0318\u0318\u030E")]
		private static readonly FastInvokeHandler onApplicationQuitMethodDuplicate1;

		[WrapperMethod("\u0312\u0318\u0311\u0316\u0316\u0313\u0314\u030E\u031B\u0310\u0317")]
		private static readonly FastInvokeHandler onApplicationQuitMethodDuplicate2;

		[WrapperMethod("\u031A\u0314\u030E\u0312\u0311\u0315\u0318\u030E\u031A\u031C\u0318")]
		private static readonly FastInvokeHandler onApplicationQuitMethodDuplicate3;

		[WrapperMethod("\u031C\u031A\u031A\u030E\u030D\u0314\u031B\u0310\u0315\u030D\u031C")]
		private static readonly FastInvokeHandler displayStatusMethodDuplicate1;

		[WrapperMethod("\u0310\u031C\u0315\u0314\u0315\u030D\u031C\u031C\u0313\u031C\u0318")]
		private static readonly FastInvokeHandler displayStatusMethodDuplicate2;

		[WrapperMethod("\u030E\u0310\u0314\u031C\u0311\u031B\u0315\u0317\u030E\u030F\u030F")]
		private static readonly FastInvokeHandler startScanMethodDuplicate1;

		[WrapperMethod("\u0312\u031A\u0314\u030D\u0315\u0314\u0313\u0310\u0315\u0316\u0314")]
		private static readonly FastInvokeHandler initializeScanSettingsMethodDuplicate1;

		[WrapperMethod("\u0316\u0312\u0315\u0316\u0318\u0313\u031C\u0313\u031B\u030E\u030E")]
		private static readonly FastInvokeHandler initializeScanSettingsMethodDuplicate2;

#pragma warning restore IDE0051, CS0169 // Remove unused private members
		#endregion
	}
}
