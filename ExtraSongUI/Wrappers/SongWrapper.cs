using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Wrappers {
	internal class SongWrapper : WrapperBase {
		//! \u031B\u0317\u0310\u0316\u0319\u0313\u0312\u0316\u0313\u031B\u0310

		public readonly object song;
		public static Type SongType => Assembly.Load("Assembly-CSharp.dll").GetType("̛̛̗̖̙̖̐̓̒̓̐");

		public MoonChartWrapper[] Charts => ((object[])chartsField.GetValue(song)).Select(c => new MoonChartWrapper(c)).ToArray();
		private static FieldInfo chartsField;
		private const string chartsFieldName = "̖̙̖̜̔̓̑̒̕̕̕";

		/// <summary>
		/// Returns a chart given an instrument and a difficulty. This should have no side effects as the method
		/// implementation is just returning a specific element of an array.
		/// </summary>
		/// <param name="instrument"></param>
		/// <param name="difficulty"></param>
		/// <returns></returns>
		public MoonChartWrapper GetChart(sbyte instrument, sbyte difficulty) => new MoonChartWrapper(getChartMethod.Invoke(song, new object[] { instrument, difficulty }));
		private static MethodInfo getChartMethod;
		private const string getChartMethodName = "̖̘̘̙̖̍̑̓̑̓̏";

		public static string QUOTEVALIDATE => (string)quoteValidateField.GetValue(null); //! Regex
		private static FieldInfo quoteValidateField;
		private const string quoteValidateFieldName = "̗̙̘̎̑̐̐̑̏̐̚";

		public string Genre => (string)genreField.GetValue(song); //! Rock?
		private static FieldInfo genreField;
		private const string genreFieldName = "̗̑̓̐̍̏̎̓̏̐̚";

		public static string FLOATSEARCH => (string)floatSearchField.GetValue(null); //! Regex
		private static FieldInfo floatSearchField;
		private const string floatSearchFieldName = "̛̗̘̘̍̏̐̓̎̕̕";

		public string UnknownString4 => (string)unknownString4Field.GetValue(song); //! String empty?
		private static FieldInfo unknownString4Field;
		private const string unknownString4Name = "̖̖̗̖̙̐̒̏̓̕̚";

		public string UnknownString5 => (string)unknownString5Field.GetValue(song); //! String empty?
		private static FieldInfo unknownString5Field;
		private const string unknownString5Name = "̛̗̜̘̐̒̎̐̕̚̚";

		public string UnknownString6 => (string)unknownString6Field.GetValue(song); //! String empty?
		private static FieldInfo unknownString6Field;
		private const string unknownString6Name = "̘̖̙̖̜̏̔̔̍̒̓";

		public string MediaType => (string)mediaTypeField.GetValue(song); //! cd
		private static FieldInfo mediaTypeField;
		private const string mediaTypeName = "̘̙̙̔̏̓̑̒̎̚̕";

		public string UnknownString8 => (string)unknownString8Field.GetValue(song); //! String empty?
		private static FieldInfo unknownString8Field;
		private const string unknownString8Name = "̛̘̜̘̐̏̒̚̚̚̚";

		public string Player2 => (string)player2Field.GetValue(song); //! Bass
		private static FieldInfo player2Field;
		private const string player2FieldName = "̛̙̎̐̒̑̐̒̓̑̕";

		public static string QUOTESEARCH => (string)quoteSearchField.GetValue(null); //! Regex
		private static FieldInfo quoteSearchField;
		private const string quoteSearchFieldName = "̛̛̜̜̙̘̔̒̓̚̚";

		public SongWrapper(object song) {
			InitializeSingletonFields();
			this.song = song;
		}

		private static void InitializeSingletonFields() {
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
