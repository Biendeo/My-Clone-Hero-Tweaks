using BiendeoCHLib.Wrappers.Attributes;
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

		#region Constructors

		public static CacheWrapper Construct() => new CacheWrapper {
			Cache = defaultConstructor.Invoke(Array.Empty<object>())
		};
		[WrapperConstructor]
		private static readonly ConstructorInfo defaultConstructor;

		#endregion

		#region Fields

		public static int CacheVersionConstant => (int)cacheVersionConstant.GetValue(null); // const 20191015
		[WrapperField("\u0314\u030D\u0311\u0314\u0317\u030F\u031A\u0317\u031B\u0317\u0316")]
		private static readonly FieldInfo cacheVersionConstant;

		public List<SongEntryWrapper> SongEntryList1 {
			get => ((ICollection)songEntryList1Field.GetValue(Cache))?.Cast<SongEntry>().Select(o => SongEntryWrapper.Wrap(o)).ToList();
			set => songEntryList1Field.SetValue(Cache, value);
		}
		[WrapperField("\u030F\u030D\u031A\u031A\u0315\u0319\u0312\u0313\u030F\u0311\u0312")]
		private static readonly FieldInfo songEntryList1Field;

		public List<SongEntryWrapper> SongEntryList2 {
			get => ((ICollection)songEntryList2Field.GetValue(Cache))?.Cast<SongEntry>().Select(o => SongEntryWrapper.Wrap(o)).ToList();
			set => songEntryList2Field.SetValue(Cache, value);
		}
		[WrapperField("\u0314\u030F\u031C\u0314\u0317\u031C\u0311\u0313\u030E\u0314\u0311")]
		private static readonly FieldInfo songEntryList2Field;

		public List<Exception> Exceptions {
			get => (List<Exception>)exceptionsField.GetValue(Cache);
			set => exceptionsField.SetValue(Cache, value);
		}
		[WrapperField("\u031B\u0319\u0318\u031B\u0311\u0311\u030F\u030D\u0317\u0317\u031A")]
		private static readonly FieldInfo exceptionsField;

		public List<string> StringList1 {
			get => (List<string>)stringList1Field.GetValue(Cache);
			set => stringList1Field.SetValue(Cache, value);
		}
		[WrapperField("\u0316\u031A\u0317\u0317\u030D\u0311\u031A\u030D\u0311\u0310\u030F")]
		private static readonly FieldInfo stringList1Field;

		public List<string> StringList2 {
			get => (List<string>)stringList2Field.GetValue(Cache);
			set => stringList2Field.SetValue(Cache, value);
		}
		[WrapperField("\u0316\u0315\u0313\u030E\u031C\u031B\u0316\u0313\u031B\u031C\u0316")]
		private static readonly FieldInfo stringList2Field;

		public List<string> StringList3 {
			get => (List<string>)stringList3Field.GetValue(Cache);
			set => stringList3Field.SetValue(Cache, value);
		}
		[WrapperField("\u030E\u031B\u030F\u0313\u031B\u0319\u030D\u0316\u0315\u031C\u0311")]
		private static readonly FieldInfo stringList3Field;

		public List<string> StringList4 {
			get => (List<string>)stringList4Field.GetValue(Cache);
			set => stringList4Field.SetValue(Cache, value);
		}
		[WrapperField("\u0317\u0317\u0313\u0314\u030E\u0310\u030F\u030F\u0310\u0311\u0316")]
		private static readonly FieldInfo stringList4Field;

		public List<string> StringList5 {
			get => (List<string>)stringList5Field.GetValue(Cache);
			set => stringList5Field.SetValue(Cache, value);
		}
		[WrapperField("\u0313\u030E\u0314\u0312\u0315\u0316\u031A\u030D\u0312\u0319\u0311")]
		private static readonly FieldInfo stringList5Field;

		public List<string> StringList6 {
			get => (List<string>)stringList6Field.GetValue(Cache);
			set => stringList6Field.SetValue(Cache, value);
		}
		[WrapperField("\u0314\u030F\u0317\u0313\u0312\u030F\u0316\u030F\u0310\u031A\u0319")]
		private static readonly FieldInfo stringList6Field;

		public List<string> StringList7 {
			get => (List<string>)stringList7Field.GetValue(Cache);
			set => stringList7Field.SetValue(Cache, value);
		}
		[WrapperField("\u030D\u0317\u0319\u0312\u0314\u0312\u0310\u030F\u030F\u030F\u031C")]
		private static readonly FieldInfo stringList7Field;

		public List<string> StringList8 {
			get => (List<string>)stringList8Field.GetValue(Cache);
			set => stringList8Field.SetValue(Cache, value);
		}
		[WrapperField("\u0315\u031A\u0312\u030F\u031B\u0310\u0312\u031C\u0315\u030E\u031B")]
		private static readonly FieldInfo stringList8Field;

		public List<string> StringList9 {
			get => (List<string>)stringList9Field.GetValue(Cache);
			set => stringList9Field.SetValue(Cache, value);
		}
		[WrapperField("\u0319\u0310\u030E\u030E\u0310\u0312\u031A\u0312\u030E\u0310\u0310")]
		private static readonly FieldInfo stringList9Field;

		public Stopwatch Stopwatch {
			get => (Stopwatch)stopwatchField.GetValue(Cache);
			set => stopwatchField.SetValue(Cache, value);
		}
		[WrapperField("\u0310\u0313\u030D\u031B\u0317\u031C\u0315\u0313\u0314\u030D\u0317")]
		private static readonly FieldInfo stopwatchField;

		public string CachePath {
			get => (string)cachePathField.GetValue(Cache);
			set => cachePathField.SetValue(Cache, value);
		}
		[WrapperField("\u0319\u0315\u031C\u030E\u0318\u0318\u031C\u0316\u0318\u0312\u0312")]
		private static readonly FieldInfo cachePathField;

		public bool SomeBool {
			get => (bool)someBoolField.GetValue(Cache);
			set => someBoolField.SetValue(Cache, value);
		}
		[WrapperField("\u031B\u0315\u0314\u031A\u0311\u0311\u0319\u0311\u0312\u031A\u0312")]
		private static readonly FieldInfo someBoolField;

		public HashSet<string> StringHashSet {
			get => (HashSet<string>)stringHashSetField.GetValue(Cache);
			set => stringHashSetField.SetValue(Cache, value);
		}
		[WrapperField("\u030D\u0314\u0316\u0312\u030F\u0317\u0317\u0317\u0312\u030F\u030E")]
		private static readonly FieldInfo stringHashSetField;

		public int Int1 {
			get => (int)int1Field.GetValue(Cache);
			set => int1Field.SetValue(Cache, value);
		}
		[WrapperField("\u0313\u0315\u0314\u0317\u0310\u0319\u0310\u030E\u031B\u0317\u0314")]
		private static readonly FieldInfo int1Field;

		public CacheState State {
			get => (CacheState)stateField.GetValue(Cache);
			set => stateField.SetValue(Cache, value);
		}
		[WrapperField("\u0314\u0314\u0313\u0311\u0311\u0314\u0318\u0314\u0311\u031C\u0312")]
		private static readonly FieldInfo stateField;

		public string BadSongsPath {
			get => (string)badSongsPathField.GetValue(Cache);
			set => badSongsPathField.SetValue(Cache, value);
		}
		[WrapperField("\u0314\u0319\u030F\u0316\u0310\u031A\u0312\u0319\u031A\u0315\u030E")]
		private static readonly FieldInfo badSongsPathField;

		public Exception SomeException {
			get => (Exception)someExceptionField.GetValue(Cache);
			set => someExceptionField.SetValue(Cache, value);
		}
		[WrapperField("\u0314\u030E\u030E\u030F\u0317\u030F\u031A\u031C\u031A\u030E\u031A")]
		private static readonly FieldInfo someExceptionField;

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

		public void ScanSongsFull() => scanSongsFullMethod.Invoke(Cache, Array.Empty<object>());
		[WrapperMethod("\u030E\u0316\u0316\u031A\u0313\u030E\u031C\u031B\u031B\u0312\u030D")]
		private static readonly MethodInfo scanSongsFullMethod;

		public void ScanSongsFast() => scanSongsFastMethod.Invoke(Cache, Array.Empty<object>());
		[WrapperMethod("\u031C\u031C\u030D\u0318\u030D\u0314\u030E\u030F\u0316\u031C\u0318")]
		private static readonly MethodInfo scanSongsFastMethod;

		public void ScanSongsInternal(bool fullScan) => scanSongsInternalMethod.Invoke(Cache, new object[] { fullScan });
		[WrapperMethod("\u0310\u031A\u030E\u0318\u0318\u0313\u0316\u0310\u0314\u0318\u0312")]
		private static readonly MethodInfo scanSongsInternalMethod;

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
		private static readonly MethodInfo scanSongsInternalMethodDuplicate1;

		[WrapperMethod("\u0315\u0310\u0313\u0318\u030D\u0317\u031A\u030E\u030E\u0314\u0317")]
		private static readonly MethodInfo scanSongsInternalMethodDuplicate2;

		[WrapperMethod("\u0318\u030D\u031A\u031C\u0314\u0311\u031A\u0313\u0315\u0314\u031B")]
		private static readonly MethodInfo scanSongsInternalMethodDuplicate3;

		[WrapperMethod("\u0318\u0315\u0315\u0312\u0317\u0315\u031C\u0317\u0318\u0315\u0310")]
		private static readonly MethodInfo scanSongsInternalMethodDuplicate4;

#pragma warning restore IDE0051, CS0169 // Remove unused private members
		#endregion
	}
}
