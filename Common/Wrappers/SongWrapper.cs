using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	[Wrapper("\u031B\u0317\u0310\u0316\u0319\u0313\u0312\u0316\u0313\u031B\u0310")]
	internal class SongWrapper {
		public readonly object song;

		public MoonChartWrapper[] Charts => ((object[])chartsField.GetValue(song)).Select(c => new MoonChartWrapper(c)).ToArray();
		[WrapperField("\u030D\u0318\u0316\u0314\u0316\u0317\u0315\u0310\u0312\u0313\u0310")]
		private static readonly FieldInfo chartsField;

		/// <summary>
		/// Returns a chart given an instrument and a difficulty. This should have no side effects as the method
		/// implementation is just returning a specific element of an array.
		/// The difficulty and instrument fields seem to just be plain wrong, so maybe ignore getting this.
		/// </summary>
		/// <param name="instrument"></param>
		/// <param name="difficulty"></param>
		/// <returns></returns>
		public MoonChartWrapper GetChart(sbyte instrument, sbyte difficulty) => new MoonChartWrapper(getChartMethod.Invoke(song, new object[] { instrument, difficulty }));
		[WrapperMethod("\u030D\u0311\u0313\u0316\u0311\u0313\u0318\u030F\u0318\u0319\u0316")]
		private static readonly MethodInfo getChartMethod; //TODO: Make this use the enums

		public static string QUOTEVALIDATE => (string)quoteValidateField.GetValue(null); //! Regex
		[WrapperField("\u030E\u0311\u0317\u0310\u0310\u031A\u0311\u0319\u030F\u0318\u0310")]
		private static readonly FieldInfo quoteValidateField;

		public string Genre => (string)genreField.GetValue(song); //! Rock?
		[WrapperField("\u0311\u0313\u0310\u030D\u030F\u030E\u0317\u0313\u031A\u030F\u0310")]
		private static readonly FieldInfo genreField;

		public static string FLOATSEARCH => (string)floatSearchField.GetValue(null); //! Regex
		[WrapperField("\u0315\u030D\u0317\u0318\u0318\u030F\u031B\u0310\u0313\u0315\u030E")]
		private static readonly FieldInfo floatSearchField;

		public string UnknownString4 => (string)unknownString4Field.GetValue(song); //! String empty?
		[WrapperField("\u0316\u0316\u0317\u0310\u0315\u0312\u030F\u0313\u0316\u031A\u0319")]
		private static readonly FieldInfo unknownString4Field;

		public string UnknownString5 => (string)unknownString5Field.GetValue(song); //! String empty?
		[WrapperField("\u0317\u0310\u0312\u0315\u031A\u031C\u030E\u031B\u0318\u031A\u0310")]
		private static readonly FieldInfo unknownString5Field;

		public string UnknownString6 => (string)unknownString6Field.GetValue(song); //! String empty?
		[WrapperField("\u0318\u030F\u0314\u0314\u030D\u0316\u0319\u0312\u0316\u0313\u031C")]
		private static readonly FieldInfo unknownString6Field;

		public string MediaType => (string)mediaTypeField.GetValue(song); //! cd
		[WrapperField("\u031A\u0314\u030F\u0318\u0315\u0319\u0313\u0319\u0311\u0312\u030E")]
		private static readonly FieldInfo mediaTypeField;

		public string UnknownString8 => (string)unknownString8Field.GetValue(song); //! String empty?
		[WrapperField("\u031A\u031B\u0310\u0318\u031A\u031C\u030F\u0312\u0318\u031A\u031A")]
		private static readonly FieldInfo unknownString8Field;

		public string Player2 => (string)player2Field.GetValue(song); //! Bass
		[WrapperField("\u031B\u030E\u0310\u0315\u0312\u0311\u0310\u0312\u0319\u0313\u0311")]
		private static readonly FieldInfo player2Field;

		public static string QUOTESEARCH => (string)quoteSearchField.GetValue(null); //! Regex
		[WrapperField("\u031B\u0314\u031C\u0312\u031A\u031C\u0313\u0319\u0318\u031B\u031A")]
		private static readonly FieldInfo quoteSearchField;

		public SongWrapper(object song) {
			this.song = song;
		}
	}
}
