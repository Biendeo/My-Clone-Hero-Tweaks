using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;

namespace BiendeoCHLib.Wrappers
{
    [Wrapper("\u0310\u031B\u0311\u0311\u0317\u030D\u0313\u030D\u0316\u0317\u031B")]
    public struct TimeSyncWrapper
    {

		public object TimeSync { get; private set; }

		public static TimeSyncWrapper Wrap(object timeSync) => new TimeSyncWrapper
		{
			TimeSync = timeSync
		};

		public override bool Equals(object obj) => TimeSync.Equals(obj);

		public override int GetHashCode() => TimeSync.GetHashCode();

		public bool IsNull() => TimeSync == null;

		#region Fields

		public double SongOffsetField
		{
			get => songOffsetField(TimeSync);
			set => songOffsetField(TimeSync) = value;
		}
		[WrapperField("\u0310\u030F\u0317\u0315\u0312\u0316\u0317\u030E\u0317\u0312\u0314")]
		private static readonly AccessTools.FieldRef<object, double> songOffsetField;

		public double SongTimeField
		{
			get => songTimeField(TimeSync);
			set => songTimeField(TimeSync) = value;
		}
		[WrapperField("\u031C\u030D\u030D\u0317\u0317\u0312\u031B\u031C\u0318\u0312\u030E")]
		private static readonly AccessTools.FieldRef<object, double> songTimeField;

		public float PracticeSpeedField
		{
			get => practiceSpeedField(TimeSync);
			set => practiceSpeedField(TimeSync) = value;
		}
		[WrapperField("\u0312\u030E\u0312\u0319\u0317\u0314\u030D\u030E\u0310\u0315\u030F")]
		private static readonly AccessTools.FieldRef<object, float> practiceSpeedField;

		#endregion

		#region Properties

		public double SongOffsetProperty => (double)songOffsetProperty.GetValue(TimeSync);
		[WrapperProperty("\u0317\u0314\u0318\u030F\u030D\u0318\u030D\u0313\u030E\u0313\u0310")]
		private static readonly PropertyInfo songOffsetProperty;

		public double SongTimeProperty => (double)songTimeProperty.GetValue(TimeSync);
		[WrapperProperty("\u030D\u0318\u0314\u0311\u0312\u0314\u0315\u031C\u0311\u0313\u0316")]
		private static readonly PropertyInfo songTimeProperty;

		public float PracticeSpeedProperty => (float)practiceSpeedProperty.GetValue(TimeSync);
		[WrapperProperty("\u0319\u031C\u031A\u0317\u0317\u031B\u030F\u031B\u030D\u031C\u030F")]
		private static readonly PropertyInfo practiceSpeedProperty;

		public bool StartSong => (bool)startSongProperty.GetValue(TimeSync);
		[WrapperProperty("\u0310\u030D\u030D\u0312\u0315\u030D\u0312\u030D\u030E\u0312\u0318")]
		private static readonly PropertyInfo startSongProperty;

		#endregion

	}
}
