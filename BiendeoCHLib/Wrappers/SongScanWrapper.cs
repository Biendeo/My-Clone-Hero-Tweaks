using BiendeoCHLib.Wrappers.Attributes;
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
			get => (bool)isScanningField.GetValue(SongScan);
			set => isScanningField.SetValue(SongScan, value);
		}
		[WrapperField("\u0318\u0316\u0315\u0313\u031B\u0310\u031A\u0311\u0314\u0317\u030F")]
		private static readonly FieldInfo isScanningField;

		public Coroutine Coroutine {
			get => (Coroutine)coroutineField.GetValue(SongScan);
			set => coroutineField.SetValue(SongScan, value);
		}
		[WrapperField("\u0319\u031A\u031C\u0315\u0317\u0319\u0314\u0317\u031A\u0316\u0314")]
		private static readonly FieldInfo coroutineField;

		public TextMeshProUGUI CountText {
			get => (TextMeshProUGUI)countTextField.GetValue(SongScan);
			set => countTextField.SetValue(SongScan, value);
		}
		[WrapperField("countText")]
		private static readonly FieldInfo countTextField;

		public TextMeshProUGUI MainText {
			get => (TextMeshProUGUI)mainTextField.GetValue(SongScan);
			set => mainTextField.SetValue(SongScan, value);
		}
		[WrapperField("mainText")]
		private static readonly FieldInfo mainTextField;

		public TextMeshProUGUI FolderText {
			get => (TextMeshProUGUI)folderTextField.GetValue(SongScan);
			set => folderTextField.SetValue(SongScan, value);
		}
		[WrapperField("folderText")]
		private static readonly FieldInfo folderTextField;

		public TextMeshProUGUI ErrorText {
			get => (TextMeshProUGUI)errorTextField.GetValue(SongScan);
			set => errorTextField.SetValue(SongScan, value);
		}
		[WrapperField("errorText")]
		private static readonly FieldInfo errorTextField;

		public TextMeshProUGUI BadSongsText {
			get => (TextMeshProUGUI)badSongsTextField.GetValue(SongScan);
			set => badSongsTextField.SetValue(SongScan, value);
		}
		[WrapperField("badSongsText")]
		private static readonly FieldInfo badSongsTextField;

		public SongSelectWrapper SongSelect {
			get => SongSelectWrapper.Wrap((SongSelect)songSelectField.GetValue(SongScan));
			set => songSelectField.SetValue(SongScan, value.SongSelect);
		}
		[WrapperField("songSelect")]
		private static readonly FieldInfo songSelectField;

		public MainMenuWrapper MainMenu {
			get => MainMenuWrapper.Wrap((MainMenu)mainMenuField.GetValue(SongScan));
			set => mainMenuField.SetValue(SongScan, value.MainMenu);
		}
		[WrapperField("mainMenu")]
		private static readonly FieldInfo mainMenuField;

		public GameObject Container {
			get => (GameObject)containerField.GetValue(SongScan);
			set => containerField.SetValue(SongScan, value);
		}
		[WrapperField("container")]
		private static readonly FieldInfo containerField;

		public bool WaitForFinish {
			get => (bool)waitForFinishField.GetValue(SongScan);
			set => waitForFinishField.SetValue(SongScan, value);
		}
		[WrapperField("\u031A\u031C\u0310\u0316\u0313\u0315\u0313\u0318\u031A\u0312\u031C")]
		private static readonly FieldInfo waitForFinishField;

		public Thread ScanThread {
			get => (Thread)scanThreadField.GetValue(SongScan);
			set => scanThreadField.SetValue(SongScan, value);
		}
		[WrapperField("\u030D\u0319\u030F\u0314\u030D\u0310\u030F\u0316\u0317\u030E\u0314")]
		private static readonly FieldInfo scanThreadField;

		public string FolderLocalized {
			get => (string)folderLocalizedField.GetValue(SongScan);
			set => folderLocalizedField.SetValue(SongScan, value);
		}
		[WrapperField("\u0317\u0311\u031A\u0319\u0313\u031A\u0316\u0313\u030D\u0311\u0313")]
		private static readonly FieldInfo folderLocalizedField;

		public string SongsScannedLocalized {
			get => (string)songsScannedLocalizedField.GetValue(SongScan);
			set => songsScannedLocalizedField.SetValue(SongScan, value);
		}
		[WrapperField("\u0311\u030E\u0319\u0316\u0312\u0318\u0311\u030F\u0310\u031C\u031C")]
		private static readonly FieldInfo songsScannedLocalizedField;

		public string ErrorsLocalized {
			get => (string)errorsLocalizedField.GetValue(SongScan);
			set => errorsLocalizedField.SetValue(SongScan, value);
		}
		[WrapperField("\u030E\u0310\u0313\u0314\u031C\u031C\u031A\u0315\u031C\u031B\u030E")]
		private static readonly FieldInfo errorsLocalizedField;

		public string BadSongsLocalized {
			get => (string)badSongsLocalizedField.GetValue(SongScan);
			set => badSongsLocalizedField.SetValue(SongScan, value);
		}
		[WrapperField("\u0311\u0319\u031A\u030F\u030D\u0316\u0318\u0317\u0318\u030E\u031A")]
		private static readonly FieldInfo badSongsLocalizedField;

		#endregion

		#region Methods

		public IEnumerator ScanForSongsThread(bool fullScan) => (IEnumerator)scanForSongsThreadMethod.Invoke(SongScan, new object[] { fullScan });
		[WrapperMethod("\u031B\u031C\u031B\u030D\u0315\u031B\u031B\u0313\u0318\u0319\u0314")]
		private static readonly MethodInfo scanForSongsThreadMethod;

		public void DisplayStatus(CacheWrapper songCache) => displayStatusMethod.Invoke(SongScan, new object[] { songCache.Cache });
		[WrapperMethod("\u0317\u0312\u031B\u0314\u0310\u0314\u0315\u0314\u0311\u0311\u031C")]
		private static readonly MethodInfo displayStatusMethod;

		public void OnApplicationQuit() => onApplicationQuitMethod.Invoke(SongScan, Array.Empty<object>());
		[WrapperMethod("OnApplicationQuit")]
		private static readonly MethodInfo onApplicationQuitMethod;

		public void AbortScan() => abortScanMethod.Invoke(SongScan, Array.Empty<object>());
		[WrapperMethod("\u0312\u0318\u0311\u0316\u0316\u0313\u0314\u030E\u031B\u0310\u0317")]
		private static readonly MethodInfo abortScanMethod;

		public void InitializeScanSettings() => initializeScanSettingsMethod.Invoke(SongScan, Array.Empty<object>());
		[WrapperMethod("\u0319\u0317\u0316\u0314\u030F\u0319\u0318\u0319\u0311\u0310\u0314")]
		private static readonly MethodInfo initializeScanSettingsMethod;

		public Coroutine StartScan(bool fullScan) => (Coroutine)startScanMethod.Invoke(SongScan, new object[] { fullScan });
		[WrapperMethod("\u031C\u0311\u0310\u030D\u0315\u0315\u031B\u030D\u0313\u0310\u0312")]
		private static readonly MethodInfo startScanMethod;

		#endregion

		#region Duplicate Methods
#pragma warning disable IDE0051, CS0169 // Remove unused private members

		// These three methods are all identical to the main one, including which functions they do call.
		[WrapperMethod("\u030E\u030E\u0315\u0316\u030F\u0316\u0315\u0312\u031C\u0314\u030D")]
		private static readonly MethodInfo scanForSongsThreadMethodDuplicate1;

		[WrapperMethod("\u031A\u0318\u030F\u0318\u0318\u031A\u031A\u030E\u0318\u0318\u030E")]
		private static readonly MethodInfo scanForSongsThreadMethodDuplicate2;

		[WrapperMethod("\u0316\u0310\u0317\u030D\u030F\u030D\u030F\u0316\u0318\u031A\u0313")]
		private static readonly MethodInfo scanForSongsThreadMethodDuplicate3;

		[WrapperMethod("\u030E\u030D\u0319\u030F\u031C\u030F\u030E\u0311\u0318\u0318\u030E")]
		private static readonly MethodInfo onApplicationQuitMethodDuplicate1;

		[WrapperMethod("\u0312\u0318\u0311\u0316\u0316\u0313\u0314\u030E\u031B\u0310\u0317")]
		private static readonly MethodInfo onApplicationQuitMethodDuplicate2;

		[WrapperMethod("\u031A\u0314\u030E\u0312\u0311\u0315\u0318\u030E\u031A\u031C\u0318")]
		private static readonly MethodInfo onApplicationQuitMethodDuplicate3;

		[WrapperMethod("\u031C\u031A\u031A\u030E\u030D\u0314\u031B\u0310\u0315\u030D\u031C")]
		private static readonly MethodInfo displayStatusMethodDuplicate1;

		[WrapperMethod("\u0310\u031C\u0315\u0314\u0315\u030D\u031C\u031C\u0313\u031C\u0318")]
		private static readonly MethodInfo displayStatusMethodDuplicate2;

		[WrapperMethod("\u030E\u0310\u0314\u031C\u0311\u031B\u0315\u0317\u030E\u030F\u030F")]
		private static readonly MethodInfo startScanMethodDuplicate1;

		[WrapperMethod("\u0312\u031A\u0314\u030D\u0315\u0314\u0313\u0310\u0315\u0316\u0314")]
		private static readonly MethodInfo initializeScanSettingsMethodDuplicate1;

		[WrapperMethod("\u0316\u0312\u0315\u0316\u0318\u0313\u031C\u0313\u031B\u030E\u030E")]
		private static readonly MethodInfo initializeScanSettingsMethodDuplicate2;

#pragma warning restore IDE0051, CS0169 // Remove unused private members
		#endregion
	}
}
