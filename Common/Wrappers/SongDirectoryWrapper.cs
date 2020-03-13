using Common.Wrappers.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	[Wrapper("\u0310\u0313\u031B\u030F\u0310\u031C\u030D\u0317\u0315\u031A\u0315")]
	internal static class SongDirectoryWrapper {
		#region Fields

		public static List<SongEntryWrapper> setlistSongEntries => ((List<SongEntry>)setlistSongEntriesField.GetValue(null)).Select(o => new SongEntryWrapper(o)).ToList();
		[WrapperField("\u030F\u0315\u0311\u0315\u0313\u0313\u031B\u0318\u0316\u031A\u0317")]
		private static readonly FieldInfo setlistSongEntriesField;

		public static List<SongEntryWrapper> setlistSongEntries2 => ((List<SongEntry>)setlistSongEntriesField2.GetValue(null)).Select(o => new SongEntryWrapper(o)).ToList();
		[WrapperField("\u0312\u030E\u031B\u030E\u0313\u0316\u0312\u0311\u0317\u0312\u0313")]
		private static readonly FieldInfo setlistSongEntriesField2;

		public static List<SongEntryWrapper> setlistSongEntries3 => ((List<SongEntry>)setlistSongEntriesField3.GetValue(null)).Select(o => new SongEntryWrapper(o)).ToList();
		[WrapperField("\u031A\u0315\u0314\u031B\u0316\u0318\u0317\u0315\u0315\u030E\u0318")]
		private static readonly FieldInfo setlistSongEntriesField3;

		public static int sortCounter {
			get => (int)songCounterField.GetValue(null);
			set => songCounterField.SetValue(null, value);
		}
		[WrapperField("\u031A\u0312\u031C\u0319\u0310\u0317\u0319\u0315\u0317\u031A\u031A")]
		private static readonly FieldInfo songCounterField;

		#endregion

		#region Methods

		public static void Sort(string forceSort, bool something) => sortMethod.Invoke(null, new object[] { forceSort, something });
		[WrapperMethod("\u0313\u0311\u030F\u0311\u0317\u0312\u0315\u031B\u0313\u031A\u031B")]
		private static readonly MethodInfo sortMethod;

		#endregion

	}
}
