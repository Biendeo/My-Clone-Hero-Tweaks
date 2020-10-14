using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("\u030F\u031A\u0311\u031B\u0314\u0319\u031B\u0312\u030F\u030E\u0319")]
	public struct CacheWrapper {
		public object Cache { get; private set; }

		public static CacheWrapper Wrap(object cache) => new CacheWrapper {
			Cache = cache
		};

		public override bool Equals(object obj) => Cache.Equals(obj);

		public override int GetHashCode() => Cache.GetHashCode();

		public bool IsNull() => Cache == null;

		#region Constructors

		public static CacheWrapper Construct() => new CacheWrapper {
			Cache = defaultConstructor.Invoke(Array.Empty<object>())
		};
		[WrapperConstructor]
		private static readonly ConstructorInfo defaultConstructor;

		#endregion

		#region Fields

		//TODO: Statics are a little iffy with FieldRef, migrate this when it works.
		public static int CacheVersionConstant => (int)cacheVersionConstantField.GetValue(null); // const 20191015
		[WrapperField("\u0314\u030D\u0311\u0314\u0317\u030F\u031A\u0317\u031B\u0317\u0316")]
		private static readonly FieldInfo cacheVersionConstantField;

		public List<SongEntryWrapper> SongEntryList1 {
			get => songEntryList1Field(Cache)?.Cast<SongEntry>().Select(o => SongEntryWrapper.Wrap(o)).ToList();
			set => songEntryList1Field(Cache) = value.Select(o => o.SongEntry).ToList();
		}
		[WrapperField("\u030F\u030D\u031A\u031A\u0315\u0319\u0312\u0313\u030F\u0311\u0312")]
		private static readonly AccessTools.FieldRef<object, List<SongEntry>> songEntryList1Field;

		public List<SongEntryWrapper> SongEntryList2 {
			get => songEntryList2Field(Cache)?.Cast<SongEntry>().Select(o => SongEntryWrapper.Wrap(o)).ToList();
			set => songEntryList2Field(Cache) = value.Select(o => o.SongEntry).ToList();
		}
		[WrapperField("\u0314\u030F\u031C\u0314\u0317\u031C\u0311\u0313\u030E\u0314\u0311")]
		private static readonly AccessTools.FieldRef<object, List<SongEntry>> songEntryList2Field;

		public List<Exception> Exceptions {
			get => exceptionsField(Cache);
			set => exceptionsField(Cache) = value;
		}
		[WrapperField("\u031B\u0319\u0318\u031B\u0311\u0311\u030F\u030D\u0317\u0317\u031A")]
		private static readonly AccessTools.FieldRef<object, List<Exception>> exceptionsField;

		public List<string> StringList1 {
			get => stringList1Field(Cache);
			set => stringList1Field(Cache) = value;
		}
		[WrapperField("\u0316\u031A\u0317\u0317\u030D\u0311\u031A\u030D\u0311\u0310\u030F")]
		private static readonly AccessTools.FieldRef<object, List<string>> stringList1Field;

		public List<string> StringList2 {
			get => stringList2Field(Cache);
			set => stringList2Field(Cache) = value;
		}
		[WrapperField("\u0316\u0315\u0313\u030E\u031C\u031B\u0316\u0313\u031B\u031C\u0316")]
		private static readonly AccessTools.FieldRef<object, List<string>> stringList2Field;

		public List<string> StringList3 {
			get => stringList3Field(Cache);
			set => stringList3Field(Cache) = value;
		}
		[WrapperField("\u030E\u031B\u030F\u0313\u031B\u0319\u030D\u0316\u0315\u031C\u0311")]
		private static readonly AccessTools.FieldRef<object, List<string>> stringList3Field;

		public List<string> StringList4 {
			get => stringList4Field(Cache);
			set => stringList4Field(Cache) = value;
		}
		[WrapperField("\u0317\u0317\u0313\u0314\u030E\u0310\u030F\u030F\u0310\u0311\u0316")]
		private static readonly AccessTools.FieldRef<object, List<string>> stringList4Field;

		public List<string> StringList5 {
			get => stringList5Field(Cache);
			set => stringList5Field(Cache) = value;
		}
		[WrapperField("\u0313\u030E\u0314\u0312\u0315\u0316\u031A\u030D\u0312\u0319\u0311")]
		private static readonly AccessTools.FieldRef<object, List<string>> stringList5Field;

		public List<string> StringList6 {
			get => stringList6Field(Cache);
			set => stringList6Field(Cache) = value;
		}
		[WrapperField("\u0314\u030F\u0317\u0313\u0312\u030F\u0316\u030F\u0310\u031A\u0319")]
		private static readonly AccessTools.FieldRef<object, List<string>> stringList6Field;

		public List<string> StringList7 {
			get => stringList7Field(Cache);
			set => stringList7Field(Cache) = value;
		}
		[WrapperField("\u030D\u0317\u0319\u0312\u0314\u0312\u0310\u030F\u030F\u030F\u031C")]
		private static readonly AccessTools.FieldRef<object, List<string>> stringList7Field;

		public List<string> StringList8 {
			get => stringList8Field(Cache);
			set => stringList8Field(Cache) = value;
		}
		[WrapperField("\u0315\u031A\u0312\u030F\u031B\u0310\u0312\u031C\u0315\u030E\u031B")]
		private static readonly AccessTools.FieldRef<object, List<string>> stringList8Field;

		public List<string> StringList9 {
			get => stringList9Field(Cache);
			set => stringList9Field(Cache) = value;
		}
		[WrapperField("\u0319\u0310\u030E\u030E\u0310\u0312\u031A\u0312\u030E\u0310\u0310")]
		private static readonly AccessTools.FieldRef<object, List<string>> stringList9Field;

		public Stopwatch Stopwatch {
			get => stopwatchField(Cache);
			set => stopwatchField(Cache) = value;
		}
		[WrapperField("\u0310\u0313\u030D\u031B\u0317\u031C\u0315\u0313\u0314\u030D\u0317")]
		private static readonly AccessTools.FieldRef<object, Stopwatch> stopwatchField;

		public string CachePath {
			get => cachePathField(Cache);
			set => cachePathField(Cache) = value;
		}
		[WrapperField("\u0319\u0315\u031C\u030E\u0318\u0318\u031C\u0316\u0318\u0312\u0312")]
		private static readonly AccessTools.FieldRef<object, string> cachePathField;

		public bool SomeBool {
			get => someBoolField(Cache);
			set => someBoolField(Cache) = value;
		}
		[WrapperField("\u031B\u0315\u0314\u031A\u0311\u0311\u0319\u0311\u0312\u031A\u0312")]
		private static readonly AccessTools.FieldRef<object, bool> someBoolField;

		public HashSet<string> StringHashSet {
			get => stringHashSetField(Cache);
			set => stringHashSetField(Cache) = value;
		}
		[WrapperField("\u030D\u0314\u0316\u0312\u030F\u0317\u0317\u0317\u0312\u030F\u030E")]
		private static readonly AccessTools.FieldRef<object, HashSet<string>> stringHashSetField;

		public int Int1 {
			get => int1Field(Cache);
			set => int1Field(Cache) = value;
		}
		[WrapperField("\u0313\u0315\u0314\u0317\u0310\u0319\u0310\u030E\u031B\u0317\u0314")]
		private static readonly AccessTools.FieldRef<object, int> int1Field;

		public CacheState State {
			get => (CacheState)stateField(Cache);
			set => stateField(Cache) = value;
		}
		[WrapperField("\u0314\u0314\u0313\u0311\u0311\u0314\u0318\u0314\u0311\u031C\u0312")]
		private static readonly AccessTools.FieldRef<object, object> stateField;

		public string BadSongsPath {
			get => badSongsPathField(Cache);
			set => badSongsPathField(Cache) = value;
		}
		[WrapperField("\u0314\u0319\u030F\u0316\u0310\u031A\u0312\u0319\u031A\u0315\u030E")]
		private static readonly AccessTools.FieldRef<object, string> badSongsPathField;

		public Exception SomeException {
			get => someExceptionField(Cache);
			set => someExceptionField(Cache) = value;
		}
		[WrapperField("\u0314\u030E\u030E\u030F\u0317\u030F\u031A\u031C\u031A\u030E\u031A")]
		private static readonly AccessTools.FieldRef<object, Exception> someExceptionField;

		#endregion

		#region Properties

		public int Int3 => (int)int3Property.GetValue(Cache);
		[WrapperProperty("\u030F\u0318\u0316\u0311\u031C\u031A\u0318\u031A\u031C\u0319\u0317")]
		private static readonly PropertyInfo int3Property;

		public int Int4 => (int)int4Property.GetValue(Cache);
		[WrapperProperty("\u031A\u0315\u030F\u031C\u0310\u030E\u030E\u0318\u0319\u0311\u0311")]
		private static readonly PropertyInfo int4Property;

		#endregion

		#region Methods

		public void ScanSongsFull() => scanSongsFullMethod(Cache);
		[WrapperMethod("\u030E\u0316\u0316\u031A\u0313\u030E\u031C\u031B\u031B\u0312\u030D")]
		private static readonly FastInvokeHandler scanSongsFullMethod;

		public void ScanSongsFast() => scanSongsFastMethod(Cache);
		[WrapperMethod("\u031C\u031C\u030D\u0318\u030D\u0314\u030E\u030F\u0316\u031C\u0318")]
		private static readonly FastInvokeHandler scanSongsFastMethod;

		public void ScanSongsInternal(bool fullScan) => scanSongsInternalMethod(Cache, fullScan);
		[WrapperMethod("\u0310\u031A\u030E\u0318\u0318\u0313\u0316\u0310\u0314\u0318\u0312")]
		private static readonly FastInvokeHandler scanSongsInternalMethod;

		#endregion

		#region Enumerations

		// \u0315\u0310\u0319\u0315\u0312\u030D\u0312\u0313\u0312\u0314\u0311
		public enum CacheState {
			ReadingCache,
			GettingPaths,
			ScanningFolders,
			UpdatingMetadata,
			UpdatingCharts,
			WritingCache,
			WritingBadSongs,
			SortingMetadata,
			OnlineHash
		}

		#endregion

		#region Duplicate Methods
#pragma warning disable IDE0051, CS0169 // Remove unused private members

		[WrapperMethod("\u0310\u0318\u0318\u030E\u031C\u030D\u030E\u030F\u030E\u030D\u0319")]
		private static readonly FastInvokeHandler scanSongsInternalMethodDuplicate1;

		[WrapperMethod("\u0315\u0310\u0313\u0318\u030D\u0317\u031A\u030E\u030E\u0314\u0317")]
		private static readonly FastInvokeHandler scanSongsInternalMethodDuplicate2;

		[WrapperMethod("\u0318\u030D\u031A\u031C\u0314\u0311\u031A\u0313\u0315\u0314\u031B")]
		private static readonly FastInvokeHandler scanSongsInternalMethodDuplicate3;

		[WrapperMethod("\u0318\u0315\u0315\u0312\u0317\u0315\u031C\u0317\u0318\u0315\u0310")]
		private static readonly FastInvokeHandler scanSongsInternalMethodDuplicate4;

#pragma warning restore IDE0051, CS0169 // Remove unused private members
		#endregion
	}
}
