using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;

namespace BiendeoCHLib.Wrappers
{
    [Wrapper("\u0314\u0318\u031A\u031A\u0310\u0318\u0313\u0317\u0313\u031B\u0310")]
    public struct ChartEventWrapper
    {
        public object ChartEvent { get; private set; }

        public static ChartEventWrapper Wrap(object chartEvent) => new ChartEventWrapper
        {
            ChartEvent = chartEvent
        };

        public override bool Equals(object obj) => ChartEvent.Equals(obj);

        public override int GetHashCode() => ChartEvent.GetHashCode();

        public bool IsNull() => ChartEvent == null;

        #region Casts

        public SongObjectWrapper CastToSongObject() => SongObjectWrapper.Wrap(ChartEvent);

        #endregion

        #region Fields

        public string EventName
        {
            get => eventNameField(ChartEvent);
            set => eventNameField(ChartEvent) = value;
        }
        [WrapperField("\u031A\u030E\u0310\u0312\u030F\u030D\u0318\u0318\u0315\u0312\u031B")]
        private static readonly AccessTools.FieldRef<object, string> eventNameField;

        #endregion
    }
}
