using BepInEx.Logging;
using BiendeoCHLib.Patches.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Patches {
	public abstract class PatchBase {
		public static void InitializePatches(Harmony harmony, Assembly assembly, ManualLogSource logger) {
			foreach (var type in assembly.GetTypes()) {
				var patch = type.GetCustomAttribute<HarmonyCHPatch>();
				if (patch != null) {
					logger.LogDebug($"Initialising patches with class {type.Name}");
					patch.InitializePatch(harmony, type, logger);
				}
			}
		}
	}
}
