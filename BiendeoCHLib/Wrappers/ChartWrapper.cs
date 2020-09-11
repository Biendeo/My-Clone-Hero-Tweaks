using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;

namespace BiendeoCHLib.Wrappers
{
    [Wrapper(typeof(Chart))]
    public struct ChartWrapper
    {
        public Chart Chart { get; private set; }

        public static ChartWrapper Wrap(Chart chart) => new ChartWrapper
        {
            Chart = chart
        };

        public override bool Equals(object obj) => Chart.Equals(obj);

        public override int GetHashCode() => Chart.GetHashCode();

        public bool IsNull() => Chart == null;

        #region Methods

        public static List<NoteWrapper> GetNotesFromStandardChart(SongWrapper song, MoonChartWrapper moonChart, PlayerProfileWrapper playerProfile)
        {
            var notes = (ICollection)getNotesFromStandardChartMethod(song.Song, moonChart.MoonChart, playerProfile.PlayerProfile);
            return notes.Cast<object>().Select(o => NoteWrapper.Wrap(o)).ToList();
        }
        [WrapperMethod("\u0311\u0313\u0311\u0316\u0311\u0318\u0313\u031C\u0312\u030D\u0315")]
        private static readonly FastInvokeHandler getNotesFromStandardChartMethod;

        #endregion
    }
}
