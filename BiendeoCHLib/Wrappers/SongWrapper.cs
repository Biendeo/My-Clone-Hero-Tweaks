using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("\u031B\u0317\u0310\u0316\u0319\u0313\u0312\u0316\u0313\u031B\u0310")]
	public struct SongWrapper {
		public object Song { get; private set; }

		public static SongWrapper Wrap(object song) => new SongWrapper {
			Song = song
		};

		public override bool Equals(object obj) => Song.Equals(obj);

		public override int GetHashCode() => Song.GetHashCode();

		public bool IsNull() => Song == null;

		#region Fields

		public MoonChartWrapper[] Charts {
			get => chartsField(Song).Select(o => MoonChartWrapper.Wrap(o)).ToArray();
			set => chartsField(Song) = value.Select(o => o.MoonChart).ToArray();
		}
		[WrapperField("\u030D\u0318\u0316\u0314\u0316\u0317\u0315\u0310\u0312\u0313\u0310")]
		private static readonly AccessTools.FieldRef<object, object[]> chartsField;

		public static string QUOTEVALIDATE => (string)quoteValidateField.GetValue(null); //! Regex
		[WrapperField("\u030E\u0311\u0317\u0310\u0310\u031A\u0311\u0319\u030F\u0318\u0310")]
		private static readonly FieldInfo quoteValidateField;

		public string Genre {
			get => genreField(Song);
			set => genreField(Song) = value;
		} //! Rock?
		[WrapperField("\u0311\u0313\u0310\u030D\u030F\u030E\u0317\u0313\u031A\u030F\u0310")]
		private static readonly AccessTools.FieldRef<object, string> genreField;

		public static string FLOATSEARCH => (string)floatSearchField.GetValue(null); //! Regex
		[WrapperField("\u0315\u030D\u0317\u0318\u0318\u030F\u031B\u0310\u0313\u0315\u030E")]
		private static readonly FieldInfo floatSearchField;

		public string UnknownString4 {
			get => unknownString4Field(Song);
			set => unknownString4Field(Song) = value;
		} //! String empty?
		[WrapperField("\u0316\u0316\u0317\u0310\u0315\u0312\u030F\u0313\u0316\u031A\u0319")]
		private static readonly AccessTools.FieldRef<object, string> unknownString4Field;

		public string UnknownString5 {
			get => unknownString5Field(Song);
			set => unknownString5Field(Song) = value;
		} //! String empty?
		[WrapperField("\u0317\u0310\u0312\u0315\u031A\u031C\u030E\u031B\u0318\u031A\u0310")]
		private static readonly AccessTools.FieldRef<object, string> unknownString5Field;

		public string UnknownString6 {
			get => unknownString6Field(Song);
			set => unknownString6Field(Song) = value;
		} //! String empty?
		[WrapperField("\u0318\u030F\u0314\u0314\u030D\u0316\u0319\u0312\u0316\u0313\u031C")]
		private static readonly AccessTools.FieldRef<object, string> unknownString6Field;

		public string MediaType {
			get => mediaTypeField(Song);
			set => mediaTypeField(Song) = value;
		} //! cd
		[WrapperField("\u031A\u0314\u030F\u0318\u0315\u0319\u0313\u0319\u0311\u0312\u030E")]
		private static readonly AccessTools.FieldRef<object, string> mediaTypeField;

		public string UnknownString8 {
			get => unknownString8Field(Song);
			set => unknownString8Field(Song) = value;
		} //! String empty?
		[WrapperField("\u031A\u031B\u0310\u0318\u031A\u031C\u030F\u0312\u0318\u031A\u031A")]
		private static readonly AccessTools.FieldRef<object, string> unknownString8Field;

		public string Player2 {
			get => player2Field(Song);
			set => player2Field(Song) = value;
		} //! Bass
		[WrapperField("\u031B\u030E\u0310\u0315\u0312\u0311\u0310\u0312\u0319\u0313\u0311")]
		private static readonly AccessTools.FieldRef<object, string> player2Field;

		public static string QUOTESEARCH => (string)quoteSearchField.GetValue(null); //! Regex
		[WrapperField("\u031B\u0314\u031C\u0312\u031A\u031C\u0313\u0319\u0318\u031B\u031A")]
		private static readonly FieldInfo quoteSearchField;

		#endregion

		#region Methods

		/// <summary>
		/// Returns a chart given an instrument and a difficulty. This should have no side effects as the method
		/// implementation is just returning a specific element of an array.
		/// The difficulty and instrument fields seem to just be plain wrong, so maybe ignore getting this.
		/// </summary>
		/// <param name="instrument"></param>
		/// <param name="difficulty"></param>
		/// <returns></returns>
		public MoonChartWrapper GetChart(sbyte instrument, sbyte difficulty) => MoonChartWrapper.Wrap(getChartMethod(Song, instrument, difficulty));
		[WrapperMethod("\u030D\u0311\u0313\u0316\u0311\u0313\u0318\u030F\u0318\u0319\u0316")]
		private static readonly FastInvokeHandler getChartMethod; //TODO: Make this use the enums

		#endregion

		#region Enumerations

		// \u030F\u0312\u031A\u0318\u0311\u030F\u031B\u0313\u0311\u030E\u0310
		public enum Difficulty {
			Expert,
			Hard,
			Medium,
			Easy
		}

		// \u0315\u031C\u0313\u0319\u0310\u0313\u030F\u0311\u030F\u0319\u030F
		public enum Instrument {
			Guitar,
			GuitarCoop,
			Bass,
			Keys,
			Drums,
			GHLGuitar,
			GHLBass,
			Vocals,
			Crowd,
			None,
			Rhythm
		}

		#endregion
	}
}
