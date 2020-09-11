using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;

namespace BiendeoCHLib.Wrappers
{
    [Wrapper("\u030D\u031C\u030F\u030D\u0315\u0312\u0311\u0318\u030F\u0312\u030F")]
    public struct SongObjectWrapper
    {
        public object SongObject { get; private set; }

        public static SongObjectWrapper Wrap(object songObject) => new SongObjectWrapper
        {
            SongObject = songObject
        };

        public override bool Equals(object obj) => SongObject.Equals(obj);

        public override int GetHashCode() => SongObject.GetHashCode();

        public bool IsNull() => SongObject == null;

        #region Fields

        public uint Tick
        {
            get => tickField(SongObject);
            set => tickField(SongObject) = value;
        }
        [WrapperField("\u030E\u031B\u0312\u030D\u030E\u0311\u030F\u031A\u030D\u0310\u030E")]
        private static readonly AccessTools.FieldRef<object, uint> tickField;

        #endregion

    }
}
