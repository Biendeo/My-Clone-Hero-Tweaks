using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Wrappers {
	internal class MoonChartWrapper : WrapperBase {
		//! \u0311\u0318\u030F\u0314\u0315\u031B\u030F\u0316\u0313\u0316\u0316

		public readonly object moonChart;
		public static Type MoonChartType => Assembly.Load("Assembly-CSharp.dll").GetType("̛̘̖̖̖̑̏̔̏̓̕");

		// Old reference had base score and note count in there, maybe those shouldn't be relied on
		//? Seems to be the note count but for the wrong difficulty?
		public int UnknownInt1 => (int)unknownInt1Field.GetValue(moonChart);
		private static FieldInfo unknownInt1Field;
		private const string unknownInt1FieldName = "̘̜̗̔̓̍̑̔̑̔̐";

		public int UnknownInt2 => (int)unknownInt2Field.GetValue(moonChart);
		private static FieldInfo unknownInt2Field;
		private const string unknownInt2FieldName = "̛̘̘̖̘̙̏̓̍̓̕";

		public SongWrapper Song => new SongWrapper(songProperty.GetValue(moonChart));
		private static PropertyInfo songProperty;
		private const string songPropertyName = "̛̛̛̗̙̖̑̎̒̏̚";

		// A property for UnknownInt1.
		public int UnknownInt3 => (int)unknownInt3Property.GetValue(moonChart);
		private static PropertyInfo unknownInt3Property;
		private const string unknownInt3PropertyName = "̛̛̛̖̙̘̘̔̏̕̕";

		public MoonChartWrapper(object moonChart) {
			InitializeSingletonFields();
			this.moonChart = moonChart;
		}

		private static void InitializeSingletonFields() {
			RegisterField(ref unknownInt1Field, MoonChartType, unknownInt1FieldName);
			RegisterField(ref unknownInt2Field, MoonChartType, unknownInt2FieldName);
			RegisterProperty(ref songProperty, MoonChartType, songPropertyName);
			RegisterProperty(ref unknownInt3Property, MoonChartType, unknownInt3PropertyName);
		}

	}
}
