using Common.Wrappers.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	[Wrapper(typeof(SongScan))]
	internal class SongScanWrapper {
		public readonly SongScan songScan;

		public SongScanWrapper(SongScan songScan) {
			this.songScan = songScan;
		}

		#region Fields

		public string folderLocalized => (string)folderLocalizedField.GetValue(songScan);
		[WrapperField("\u0317\u0311\u031A\u0319\u0313\u031A\u0316\u0313\u030D\u0311\u0313")]
		private static readonly FieldInfo folderLocalizedField;

		public string songsScannedLocalized => (string)songsScannedLocalizedField.GetValue(songScan);
		[WrapperField("\u0311\u030E\u0319\u0316\u0312\u0318\u0311\u030F\u0310\u031C\u031C")]
		private static readonly FieldInfo songsScannedLocalizedField;

		public string errorsLocalized => (string)errorsLocalizedField.GetValue(songScan);
		[WrapperField("\u030E\u0310\u0313\u0314\u031C\u031C\u031A\u0315\u031C\u031B\u030E")]
		private static readonly FieldInfo errorsLocalizedField;

		public string badSongsLocalized => (string)badSongsLocalizedField.GetValue(songScan);
		[WrapperField("\u0311\u0319\u031A\u030F\u030D\u0316\u0318\u0317\u0318\u030E\u031A")]
		private static readonly FieldInfo badSongsLocalizedField;

		public Thread scanThread => (Thread)scanThreadField.GetValue(songScan);
		[WrapperField("\u030D\u0319\u030F\u0314\u030D\u0310\u030F\u0316\u0317\u030E\u0314")]
		private static readonly FieldInfo scanThreadField;

		public bool waitForFinish => (bool)waitForFinishField.GetValue(songScan);
		[WrapperField("\u031A\u031C\u0310\u0316\u0313\u0315\u0313\u0318\u031A\u0312\u031C")]
		private static readonly FieldInfo waitForFinishField;

		#endregion

		#region Methods

		// There's three other identical versions of this method:
		// \u030E\u030E\u0315\u0316\u030F\u0316\u0315\u0312\u031C\u0314\u030D
		// \u031A\u0318\u030F\u0318\u0318\u031A\u031A\u030E\u0318\u0318\u030E
		// \u031B\u031C\u031B\u030D\u0315\u031B\u031B\u0313\u0318\u0319\u0314
		public IEnumerator ScanForSongsThread(bool fullScan) => (IEnumerator)scanForSongsThreadMethod.Invoke(songScan, new object[] { fullScan });
		[WrapperMethod("\u0316\u0310\u0317\u030D\u030F\u030D\u030F\u0316\u0318\u031A\u0313")]
		private static readonly MethodInfo scanForSongsThreadMethod;

		public void DisplayStatus(CacheWrapper songCache) => displayStatusMethod.Invoke(songScan, new object[] { songCache.cache });
		[WrapperMethod("\u0317\u0312\u031B\u0314\u0310\u0314\u0315\u0314\u0311\u0311\u031C")]
		private static readonly MethodInfo displayStatusMethod;

		#endregion
	}
}
