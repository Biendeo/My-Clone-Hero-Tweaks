using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrappers {
	[Wrapper("\u030F\u031A\u0311\u031B\u0314\u0319\u031B\u0312\u030F\u030E\u0319")]
	internal class CacheWrapper {
		public readonly object cache;

		public CacheWrapper(object cache) {
			this.cache = cache;
		}

		#region Constructors

		public CacheWrapper() {
			cache = defaultConstructor.Invoke(Array.Empty<object>());
		}
		[WrapperConstructor()]
		private static readonly ConstructorInfo defaultConstructor;

		#endregion

		#region Fields

		public string cachePath => (string)cachePathField.GetValue(cache);
		[WrapperField("\u0319\u0315\u031C\u030E\u0318\u0318\u031C\u0316\u0318\u0312\u0312")]
		private static readonly FieldInfo cachePathField;

		public string badSongsPath => (string)badSongsPathField.GetValue(cache);
		[WrapperField("\u0314\u0319\u030F\u0316\u0310\u031A\u0312\u0319\u031A\u0315\u030E")]
		private static readonly FieldInfo badSongsPathField;

		public CacheState cacheState => (CacheState)cacheStateField.GetValue(cache);
		[WrapperField("\u0314\u0314\u0313\u0311\u0311\u0314\u0318\u0314\u0311\u031C\u0312")]
		private static readonly FieldInfo cacheStateField;

		public int Int1 => (int)int1Field.GetValue(cache);
		[WrapperField("\u0313\u0315\u0314\u0317\u0310\u0319\u0310\u030E\u031B\u0317\u0314")]
		private static readonly FieldInfo int1Field;

		public static int Int2 => (int)int2Field.GetValue(null); // 20191015
		[WrapperField("\u0314\u030D\u0311\u0314\u0317\u030F\u031A\u0317\u031B\u0317\u0316")]
		private static readonly FieldInfo int2Field;

		public List<Exception> exceptions => (List<Exception>)exceptionsField.GetValue(cache);
		[WrapperField("\u031B\u0319\u0318\u031B\u0311\u0311\u030F\u030D\u0317\u0317\u031A")]
		private static readonly FieldInfo exceptionsField;

		#endregion

		#region Properties

		public int Int3 => (int)int3Property.GetValue(cache);
		[WrapperProperty("\u030F\u0318\u0316\u0311\u031C\u031A\u0318\u031A\u031C\u0319\u0317")]
		private static readonly PropertyInfo int3Property;

		public int Int4 => (int)int4Property.GetValue(cache);
		[WrapperProperty("\u031A\u0315\u030F\u031C\u0310\u030E\u030E\u0318\u0319\u0311\u0311")]
		private static readonly PropertyInfo int4Property;

		#endregion

		#region Methods

		public void ScanSongsFull() => scanSongsFullMethod.Invoke(cache, Array.Empty<object>());
		[WrapperMethod("\u030E\u0316\u0316\u031A\u0313\u030E\u031C\u031B\u031B\u0312\u030D")]
		private static readonly MethodInfo scanSongsFullMethod;

		public void ScanSongsFast() => scanSongsFastMethod.Invoke(cache, Array.Empty<object>());
		[WrapperMethod("\u031C\u031C\u030D\u0318\u030D\u0314\u030E\u030F\u0316\u031C\u0318")]
		private static readonly MethodInfo scanSongsFastMethod;

		#endregion

		#region Enumerations

		//\u0315\u0310\u0319\u0315\u0312\u030D\u0312\u0313\u0312\u0314\u0311
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
	}
}
