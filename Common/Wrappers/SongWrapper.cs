using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Wrappers {
	internal class SongWrapper : WrapperBase {
		public readonly object song;
		public static Type SongType;

		public MoonChartWrapper[] Charts => ((object[])chartsField.GetValue(song)).Select(c => new MoonChartWrapper(c)).ToArray();
		private static FieldInfo chartsField;
		private const string chartsFieldName = "\u030D\u0318\u0316\u0314\u0316\u0317\u0315\u0310\u0312\u0313\u0310";

		/// <summary>
		/// Returns a chart given an instrument and a difficulty. This should have no side effects as the method
		/// implementation is just returning a specific element of an array.
		/// </summary>
		/// <param name="instrument"></param>
		/// <param name="difficulty"></param>
		/// <returns></returns>
		public MoonChartWrapper GetChart(sbyte instrument, sbyte difficulty) => new MoonChartWrapper(getChartMethod.Invoke(song, new object[] { instrument, difficulty }));
		private static MethodInfo getChartMethod;
		private const string getChartMethodName = "\u030D\u0311\u0313\u0316\u0311\u0313\u0318\u030F\u0318\u0319\u0316";

		public static string QUOTEVALIDATE => (string)quoteValidateField.GetValue(null); //! Regex
		private static FieldInfo quoteValidateField;
		private const string quoteValidateFieldName = "\u030E\u0311\u0317\u0310\u0310\u031A\u0311\u0319\u030F\u0318\u0310";
		//private const string quoteValidateFieldName = "̗̙̘̎̑̐̐̑̏̐̚";

		public string Genre => (string)genreField.GetValue(song); //! Rock?
		private static FieldInfo genreField;
		private const string genreFieldName = "\u0311\u0313\u0310\u030D\u030F\u030E\u0317\u0313\u031A\u030F\u0310";

		public static string FLOATSEARCH => (string)floatSearchField.GetValue(null); //! Regex
		private static FieldInfo floatSearchField;
		private const string floatSearchFieldName = "\u0315\u030D\u0317\u0318\u0318\u030F\u031B\u0310\u0313\u0315\u030E";

		public string UnknownString4 => (string)unknownString4Field.GetValue(song); //! String empty?
		private static FieldInfo unknownString4Field;
		private const string unknownString4Name = "\u0316\u0316\u0317\u0310\u0315\u0312\u030F\u0313\u0316\u031A\u0319";

		public string UnknownString5 => (string)unknownString5Field.GetValue(song); //! String empty?
		private static FieldInfo unknownString5Field;
		private const string unknownString5Name = "\u0317\u0310\u0312\u0315\u031A\u031C\u030E\u031B\u0318\u031A\u0310";

		public string UnknownString6 => (string)unknownString6Field.GetValue(song); //! String empty?
		private static FieldInfo unknownString6Field;
		private const string unknownString6Name = "\u0318\u030F\u0314\u0314\u030D\u0316\u0319\u0312\u0316\u0313\u031C";

		public string MediaType => (string)mediaTypeField.GetValue(song); //! cd
		private static FieldInfo mediaTypeField;
		private const string mediaTypeName = "\u031A\u0314\u030F\u0318\u0315\u0319\u0313\u0319\u0311\u0312\u030E";

		public string UnknownString8 => (string)unknownString8Field.GetValue(song); //! String empty?
		private static FieldInfo unknownString8Field;
		private const string unknownString8Name = "\u031A\u031B\u0310\u0318\u031A\u031C\u030F\u0312\u0318\u031A\u031A";

		public string Player2 => (string)player2Field.GetValue(song); //! Bass
		private static FieldInfo player2Field;
		private const string player2FieldName = "\u031B\u030E\u0310\u0315\u0312\u0311\u0310\u0312\u0319\u0313\u0311";

		public static string QUOTESEARCH => (string)quoteSearchField.GetValue(null); //! Regex
		private static FieldInfo quoteSearchField;
		private const string quoteSearchFieldName = "\u031B\u0314\u031C\u0312\u031A\u031C\u0313\u0319\u0318\u031B\u031A";

		public SongWrapper(object song) {
			this.song = song;
		}

		public static void InitializeSingletonFields() {
			SongType = Assembly.Load("Assembly-CSharp.dll").GetType("\u031B\u0317\u0310\u0316\u0319\u0313\u0312\u0316\u0313\u031B\u0310");
			RegisterField(ref chartsField, SongType, chartsFieldName);
			RegisterMethod(ref getChartMethod, SongType, getChartMethodName);
			RegisterStaticField(ref quoteValidateField, SongType, quoteValidateFieldName);
			RegisterField(ref genreField, SongType, genreFieldName);
			RegisterStaticField(ref floatSearchField, SongType, floatSearchFieldName);
			RegisterField(ref unknownString4Field, SongType, unknownString4Name);
			RegisterField(ref unknownString5Field, SongType, unknownString5Name);
			RegisterField(ref unknownString6Field, SongType, unknownString6Name);
			RegisterField(ref mediaTypeField, SongType, mediaTypeName);
			RegisterField(ref unknownString8Field, SongType, unknownString8Name);
			RegisterField(ref player2Field, SongType, player2FieldName);
			RegisterStaticField(ref quoteSearchField, SongType, quoteSearchFieldName);
		}
	}
}
