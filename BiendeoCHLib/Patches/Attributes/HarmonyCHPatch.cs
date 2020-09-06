using BepInEx.Logging;
using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Patches.Attributes {
	public class HarmonyCHPatch : Attribute {
		private readonly Type wrapperType;
		private readonly string wrapperMethodName;

		public HarmonyCHPatch(Type wrapperType, string wrapperMethodName) {
			this.wrapperType = wrapperType;
			this.wrapperMethodName = wrapperMethodName;
		}

		public void InitializePatch(Harmony harmony, Type patchType, ManualLogSource logger) {
			// Assert that the wrapper type indeed has the methodInfo expected.
			MethodInfo targetMethod = null;
			var wrapperField = wrapperType.GetField($"{wrapperMethodName.ToLower()[0]}{wrapperMethodName.Substring(1)}Method", BindingFlags.Static | BindingFlags.NonPublic);
			if (wrapperField != null) {
				targetMethod = wrapperField.GetCustomAttribute<WrapperMethod>().GetMethodInfo(wrapperType.GetCustomAttribute<Wrapper>().WrappedType);
				logger.LogInfo($"Found matching method for patch {wrapperType.Name}.{wrapperMethodName}");
			} else {
				logger.LogError($"Could not find matching method for patch {wrapperType.Name}.{wrapperMethodName}");
#if DEBUG
				logger.LogError($"Program terminating until this patch is fixed!");
				Environment.Exit(1);
#endif
				return;
			}

			// Now determine the prefix and postfix methods in the patch type.
			MethodInfo prefixMethod = null;
			MethodInfo postfixMethod = null;
			foreach (var method in patchType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) {
				if (method.Name == "Prefix" || method.GetCustomAttribute<HarmonyCHPrefix>() != null) {
					logger.LogInfo($"Found prefix method {method.Name}");
#if DEBUG
					if (prefixMethod != null) {
						logger.LogError($"This replaces a previously defined prefix method, terminating...");
						Environment.Exit(1);
					}
#endif
					prefixMethod = method;
				}
				if (method.Name == "Postfix" || method.GetCustomAttribute<HarmonyCHPostfix>() != null) {
					logger.LogInfo($"Found postfix method {method.Name}");
#if DEBUG
					if (postfixMethod != null) {
						logger.LogError($"This replaces a previously defined postfix method, terminating...");
						Environment.Exit(1);
					}
#endif
					postfixMethod = method;
				}
			}
			if (prefixMethod != null || postfixMethod != null) {
				harmony.Patch(targetMethod, prefix: prefixMethod == null ? null : new HarmonyMethod(prefixMethod), postfix: postfixMethod == null ? null : new HarmonyMethod(postfixMethod));
			} else {
				logger.LogWarning("Found no prefix/postfix methods for patch, so no action will be done.");
			}
		}
	}
}
