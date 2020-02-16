using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	[Wrapper("\u0311\u0318\u030F\u0314\u0315\u031B\u030F\u0316\u0313\u0316\u0316")]
	internal class MoonChartWrapper {
		public readonly object moonChart;

		// Old reference had base score and note count in there, maybe those shouldn't be relied on
		//? Seems to be the note count but for the wrong difficulty?
		public int UnknownInt1 => (int)unknownInt1Field.GetValue(moonChart);
		[WrapperField("\u0314\u0313\u030D\u0311\u0318\u0314\u031C\u0311\u0314\u0310\u0317")]
		private static readonly FieldInfo unknownInt1Field;

		public int UnknownInt2 => (int)unknownInt2Field.GetValue(moonChart);
		[WrapperField("\u0315\u030F\u0313\u0318\u0318\u0316\u0318\u031B\u030D\u0319\u0313")]
		private static readonly FieldInfo unknownInt2Field;

		public SongWrapper Song => new SongWrapper(songProperty.GetValue(moonChart));
		[WrapperField("\u030F\u0315\u031A\u0316\u0318\u031A\u030E\u0319\u0316\u0311\u031A")]
		private static readonly PropertyInfo songProperty;

		// A property for UnknownInt1.
		public int UnknownInt1Property => (int)unknownInt1Property.GetValue(moonChart);
		[WrapperProperty("\u0314\u0316\u030F\u0319\u0315\u031B\u0318\u0318\u031B\u0315\u031B")]
		private static readonly PropertyInfo unknownInt1Property;

		public StarPowerWrapper[] StarPower => ((object[])starPowerProperty.GetValue(moonChart)).Select(o => new StarPowerWrapper(o)).ToArray();
		[WrapperProperty("\u0314\u0312\u0318\u031C\u031A\u0313\u0314\u0317\u030E\u0315\u031C")]
		private static readonly PropertyInfo starPowerProperty;

		public MoonChartWrapper(object moonChart) {
			this.moonChart = moonChart;
		}
	}
}
