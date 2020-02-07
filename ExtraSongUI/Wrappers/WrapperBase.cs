using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtraSongUI.Wrappers {
	abstract class WrapperBase {
		/// <summary>
		/// A helper method that initialises a FieldInfo if it is null. Useful for singleton knowledge of reflected fields.
		/// </summary>
		/// <param name="field"></param>
		/// <param name="owningType"></param>
		/// <param name="fieldName"></param>
		protected static void RegisterField(ref FieldInfo field, Type owningType, string fieldName) {
			if (field == null) {
				field = owningType.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>
		/// A helper method that initialises a static FieldInfo if it is null. Useful for singleton knowledge of reflected fields.
		/// </summary>
		/// <param name="field"></param>
		/// <param name="owningType"></param>
		/// <param name="fieldName"></param>
		protected static void RegisterStaticField(ref FieldInfo field, Type owningType, string fieldName) {
			if (field == null) {
				field = owningType.GetField(fieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>
		/// A helper method that initialises a PropertyInfo if it is null. Useful for singleton knowledge of reflected fields.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="owningType"></param>
		/// <param name="propertyName"></param>
		protected static void RegisterProperty(ref PropertyInfo property, Type owningType, string propertyName) {
			if (property == null) {
				property = owningType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>
		/// A helper method that initialises a MethodInfo if it is null. Useful for singleton knowledge of reflected methods.
		/// </summary>
		/// <param name="method"></param>
		/// <param name="owningType"></param>
		/// <param name="methodName"></param>
		/// <param name="types"></param>
		protected static void RegisterMethod(ref MethodInfo method, Type owningType, string methodName, Type[] types) {
			if (method == null) {
				method = owningType.GetMethod(methodName, types);
			}
		}

		/// <summary>
		/// A helper method that initialises a MethodInfo if it is null. Useful for singleton knowledge of reflected methods.
		/// </summary>
		/// <param name="method"></param>
		/// <param name="owningType"></param>
		/// <param name="methodName"></param>
		protected static void RegisterMethod(ref MethodInfo method, Type owningType, string methodName) {
			if (method == null) {
				method = owningType.GetMethod(methodName);
			}
		}
	}

	internal static class StringExtensions {
		public static string DecodeUnicode(this string s) {
			if (s.Any(c => c >= '\u0300')) {
				return string.Join("", s.Select(c => $"\\u{((int)c).ToString("X4")}"));
			} else {
				return s;
			}
		}
	}
}
