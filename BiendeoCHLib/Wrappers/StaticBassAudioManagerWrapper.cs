using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers
{

    // There are 2 BassAudioManagers. One is a static class and the other is a regular class.
    // The static class seems to just be a way to play sounds using their own functions, rather than
    // passing an enum each time.
    [Wrapper("\u031C\u0314\u030D\u0313\u0311\u0312\u0311\u0319\u0314\u031A\u030F")]
    public struct StaticBassAudioManagerWrapper
    {

        public object StaticBassAudioManager { get; private set; }

        public static StaticBassAudioManagerWrapper Wrap(object staticBassAudioManager) => new StaticBassAudioManagerWrapper
        {
            StaticBassAudioManager = staticBassAudioManager
        };

        public override bool Equals(object obj) => StaticBassAudioManager.Equals(obj);

        public override int GetHashCode() => StaticBassAudioManager.GetHashCode();

        public bool IsNull() => StaticBassAudioManager == null;

        #region Methods

        public static void PlayStarPowerAward() => starPowerAwardMethod(null, null);
        [WrapperMethod("\u030F\u0317\u030E\u031A\u0317\u031A\u0316\u031C\u0319\u031C\u0312")]
        private static FastInvokeHandler starPowerAwardMethod;

        #endregion

    }
}
