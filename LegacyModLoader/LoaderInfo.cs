using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LegacyModLoader {
	public class LoaderInfo {
		public Assembly Assembly;
		public Type LoaderType;
		public MethodInfo LoadTweakMethod;
		public MethodInfo UnloadTweakMethod;
		public object Instance;
	}
}
