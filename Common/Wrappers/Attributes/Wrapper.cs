using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Common.Wrappers.Attributes {
	public sealed class Wrapper : Attribute {
		private Type type;

		public Wrapper(Type type) {
			this.type = type;
		}

		public Wrapper(string typeName) {
			type = Assembly.Load("Assembly-CSharp.dll").GetType(typeName);
		}

		public void InitializeSingletons(Type wrapperType) {
			//Debug.LogError($"Looking at {wrapperType.Name} for {type.Name}");
			//Debug.LogError($"Fields for {wrapperType.Name}: {string.Join(", ", wrapperType.GetFields(BindingFlags.Static | BindingFlags.NonPublic).Select(t => t.Name))}");
			foreach (var field in wrapperType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) {
				if (field.FieldType == typeof(FieldInfo) && field.GetValue(null) == null) {
					var wrapperMember = field.GetCustomAttribute<WrapperField>();
					if (wrapperMember != null) {
						field.SetValue(null, type.GetField(wrapperMember.ObfuscatedName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
					}
					//Debug.LogError($"Field {field.Name} of type {field.FieldType.Name}");
				} else if (field.FieldType == typeof(PropertyInfo) && field.GetValue(null) == null) {
					var wrapperProperty = field.GetCustomAttribute<WrapperProperty>();
					if (wrapperProperty != null) {
						field.SetValue(null, type.GetProperty(wrapperProperty.ObfuscatedName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
					}
					//Debug.LogError($"Property {field.Name} of type {field.FieldType.Name}");
				} else if (field.FieldType == typeof(MethodInfo) && field.GetValue(null) == null) {
					var wrapperMethod = field.GetCustomAttribute<WrapperMethod>();
					if (wrapperMethod != null) {
						field.SetValue(null, type.GetMethod(wrapperMethod.ObfuscatedName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
					}
					//Debug.LogError($"Method {field.Name} of type {field.FieldType.Name}");
				} else {
					//Debug.LogError($"Unknown thing {field.Name} of type {field.FieldType.Name}");
				}
				if ((field.FieldType == typeof(FieldInfo) || field.FieldType == typeof(PropertyInfo) || field.FieldType == typeof(MethodInfo)) && field.GetValue(null) == null) {
					Debug.LogError($"Uh oh! {wrapperType.Name}.{field.Name} was null!");
				}
			}
		}
	}
}
