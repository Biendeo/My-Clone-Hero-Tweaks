using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	abstract class WrapperBase {
		public static void InitializeLoaders() {
			foreach (var type in Assembly.GetExecutingAssembly().GetTypes()) {
				var wrapper = type.GetCustomAttribute<Wrapper>();
				if (wrapper != null) {
					wrapper.InitializeSingletons(type);
				}
			}
		}
	}

	public static class StringExtensions {
		public static string DecodeUnicode(this string s) {
			if (s.Any(c => c >= '\u0300')) {
				return string.Join("", s.Select(c => $"\\u{((int)c).ToString("X4")}"));
			} else {
				return s;
			}
		}
	}
}
