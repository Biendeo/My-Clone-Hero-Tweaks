using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Wrappers {
	internal class MoonChartWrapper : WrapperBase {
		public readonly object moonChart;
		public static Type MoonChartType;

		// Old reference had base score and note count in there, maybe those shouldn't be relied on
		//? Seems to be the note count but for the wrong difficulty?
		public int UnknownInt1 => (int)unknownInt1Field.GetValue(moonChart);
		private static FieldInfo unknownInt1Field;
		private const string unknownInt1FieldName = "\u0314\u0313\u030D\u0311\u0318\u0314\u031C\u0311\u0314\u0310\u0317";

		public int UnknownInt2 => (int)unknownInt2Field.GetValue(moonChart);
		private static FieldInfo unknownInt2Field;
		private const string unknownInt2FieldName = "\u0315\u030F\u0313\u0318\u0318\u0316\u0318\u031B\u030D\u0319\u0313";

		public SongWrapper Song => new SongWrapper(songProperty.GetValue(moonChart));
		private static PropertyInfo songProperty;
		private const string songPropertyName = "\u030F\u0315\u031A\u0316\u0318\u031A\u030E\u0319\u0316\u0311\u031A";

		// A property for UnknownInt1.
		public int UnknownInt3 => (int)unknownInt3Property.GetValue(moonChart);
		private static PropertyInfo unknownInt3Property;
		private const string unknownInt3PropertyName = "\u0314\u0316\u030F\u0319\u0315\u031B\u0318\u0318\u031B\u0315\u031B";

		public StarPowerWrapper[] StarPower => ((object[])starPowerProperty.GetValue(moonChart)).Select(o => new StarPowerWrapper(o)).ToArray();
		private static PropertyInfo starPowerProperty;
		private const string starPowerPropertyName = "\u0314\u0312\u0318\u031C\u031A\u0313\u0314\u0317\u030E\u0315\u031C";

		public MoonChartWrapper(object moonChart) {
			this.moonChart = moonChart;
		}

		public static void InitializeSingletonFields() {
			MoonChartType = Assembly.Load("Assembly-CSharp.dll").GetType("\u0311\u0318\u030F\u0314\u0315\u031B\u030F\u0316\u0313\u0316\u0316");
			RegisterField(ref unknownInt1Field, MoonChartType, unknownInt1FieldName);
			RegisterField(ref unknownInt2Field, MoonChartType, unknownInt2FieldName);
			RegisterProperty(ref songProperty, MoonChartType, songPropertyName);
			RegisterProperty(ref unknownInt3Property, MoonChartType, unknownInt3PropertyName);
			RegisterProperty(ref starPowerProperty, MoonChartType, starPowerPropertyName);
		}

	}
}
