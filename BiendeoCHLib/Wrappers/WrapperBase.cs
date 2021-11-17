using BepInEx.Logging;
using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	public abstract class WrapperBase {
		private static bool initializedWrappers = false;

		public static void InitializeWrappers(ManualLogSource logger) {
			if (!initializedWrappers) {
				foreach (var type in Assembly.GetExecutingAssembly().GetTypes()) {
					var wrapper = type.GetCustomAttribute<Wrapper>();
					if (wrapper != null) {
						logger.LogDebug($"Initialising wrapper {type.Name}");
						wrapper.InitializeSingletons(type, logger);
					}
				}
				initializedWrappers = true;
			}
		}
	}

	public static class StringExtensions {
		public static string DecodeUnicode(this string s) => string.Join(string.Empty, s.Select(c => (c >= '\u0200' ? $"\\u{(int)c:X4}" : $"{c}")));
		public static bool HasObfuscation(this string s) => s.DecodeUnicode() != s;
	}
}
