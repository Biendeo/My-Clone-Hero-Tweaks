using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;

namespace BiendeoCHLib.Wrappers
{
    [Wrapper("\u030D\u031C\u0319\u030D\u030D\u0318\u0319\u030F\u030D\u0318\u0319")]
    public struct ScoreIOWrapper
    {

        public object ScoreIO { get; private set; }

        public static ScoreIOWrapper Wrap(object scoreIo) => new ScoreIOWrapper
        {
            ScoreIO = scoreIo
        };

        public override bool Equals(object obj) => ScoreIO.Equals(obj);

        public override int GetHashCode() => ScoreIO.GetHashCode();

        public bool IsNull() => ScoreIO == null;

        #region Fields

        public static string ScoresPath
        {
            get => scoresPathField(null);
            set => scoresPathField(null) = value;
        }
        [WrapperField("\u031A\u0311\u0315\u0312\u0314\u031B\u030E\u0312\u030D\u031C\u0316")]
        private static AccessTools.FieldRef<object, string> scoresPathField;

        public static string BackupScoresPath
        {
            get => backupScoresPathField(null);
            set => backupScoresPathField(null) = value;
        }
        [WrapperField("\u0313\u0316\u031A\u0311\u0314\u031B\u030F\u0314\u0314\u0314\u0317")]
        private static AccessTools.FieldRef<object, string> backupScoresPathField;

        #endregion

        #region Methods

        public void SetPaths() => setPathsMethod(null);
        [WrapperMethod("\u0313\u0314\u0319\u0310\u0313\u0319\u0312\u0317\u030F\u0312\u0317")]
        private static FastInvokeHandler setPathsMethod;

        #endregion

    }
}
