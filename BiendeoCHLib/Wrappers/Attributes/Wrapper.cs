﻿using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Schema;
using UnityEngine;

namespace BiendeoCHLib.Wrappers.Attributes {
	public sealed class Wrapper : Attribute {
		public readonly Type WrappedType;

		public Wrapper(Type type) {
			WrappedType = type;
		}

		public Wrapper(string typeName) {
			WrappedType = Assembly.Load("Assembly-CSharp.dll").GetType(typeName);
		}

		public void InitializeSingletons(Type wrapperType, ManualLogSource logger) {
			var fieldsSeen = new HashSet<FieldInfo>();
			var propertiesSeen = new HashSet<PropertyInfo>();
			var methodsSeen = new HashSet<MethodInfo>();
			var constructorsSeen = new HashSet<ConstructorInfo>();

			foreach (var field in wrapperType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) {
				if (field.FieldType == typeof(FieldInfo) && field.GetValue(null) == null) {
					var wrapperMember = field.GetCustomAttribute<WrapperField>();
					if (wrapperMember != null) {
						var fieldInfo = WrappedType.GetField(wrapperMember.ObfuscatedName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
						if (fieldInfo != null) {
							field.SetValue(null, fieldInfo);
							fieldsSeen.Add(fieldInfo);
							logger.LogInfo($"Loaded field {wrapperType.Name}.{field.Name}");
						}
					}
				} else if (field.FieldType == typeof(PropertyInfo) && field.GetValue(null) == null) {
					var wrapperProperty = field.GetCustomAttribute<WrapperProperty>();
					if (wrapperProperty != null) {
						var propertyInfo = WrappedType.GetProperty(wrapperProperty.ObfuscatedName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
						if (propertyInfo != null) {
							field.SetValue(null, propertyInfo);
							propertiesSeen.Add(propertyInfo);
							logger.LogInfo($"Loaded property {wrapperType.Name}.{field.Name}");
						}
					}
				} else if (field.FieldType == typeof(FastInvokeHandler) && field.GetValue(null) == null) {
					var wrapperMethod = field.GetCustomAttribute<WrapperMethod>();
					if (wrapperMethod != null) {
						var methodInfo = wrapperMethod.GetMethodInfo(WrappedType);
						if (methodInfo != null) {
							field.SetValue(null, MethodInvoker.GetHandler(methodInfo));
							methodsSeen.Add(methodInfo);
							logger.LogInfo($"Loaded method {wrapperType.Name}.{field.Name}");
						}
					}
				} else if (field.FieldType == typeof(ConstructorInfo) && field.GetValue(null) == null) {
					var wrapperConstructor = field.GetCustomAttribute<WrapperConstructor>();
					if (wrapperConstructor != null) {
						var constructorInfo = WrappedType.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, wrapperConstructor.Types, null);
						if (constructorInfo != null) {
							field.SetValue(null, constructorInfo);
							constructorsSeen.Add(constructorInfo);
							logger.LogInfo($"Loaded constructor {wrapperType.Name}.{field.Name}");
						}
					}
				}
				if ((field.FieldType == typeof(FieldInfo) || field.FieldType == typeof(PropertyInfo) || field.FieldType == typeof(MethodInfo) || field.FieldType == typeof(ConstructorInfo)) && field.GetValue(null) == null) {
					logger.LogError($"Uh oh! {wrapperType.Name}.{field.Name} was null!");
#if DEBUG
					logger.LogError($"Program terminating until this wrapper is fixed!");
					Environment.Exit(1);
#endif
				}
			}
#if DEBUG
			var remainingFields = WrappedType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).Except(fieldsSeen).ToList();
			var remainingProperties = WrappedType.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).Except(propertiesSeen).ToList();
			var remainingMethods = WrappedType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).Except(methodsSeen).ToList();
			var remainingConstructors = WrappedType.GetConstructors(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).Except(constructorsSeen).ToList();

			if (remainingFields.Count > 0) {
				logger.LogWarning($"Wrapper {wrapperType.Name} is missing {remainingFields.Count} fields, first is {remainingFields.First().Name.DecodeUnicode()}.");
			}
			if (remainingProperties.Count > 0) {
				logger.LogWarning($"Wrapper {wrapperType.Name} is missing {remainingProperties.Count} properties, first is {remainingProperties.First().Name.DecodeUnicode()}.");
			}
			if (remainingMethods.Count > 0) {
				logger.LogWarning($"Wrapper {wrapperType.Name} is missing {remainingMethods.Count} methods (unless there's method duplication), first is {remainingMethods.First().Name.DecodeUnicode()}.");
			}
			if (remainingConstructors.Count > 0) {
				logger.LogWarning($"Wrapper {wrapperType.Name} is missing {remainingConstructors.Count} constructors, first is {remainingConstructors.First().Name.DecodeUnicode()}.");
			}
			if (remainingFields.Count == 0 && remainingProperties.Count == 0 && remainingMethods.Count == 0 && remainingConstructors.Count == 0) {
				logger.LogInfo($"Wrapper {wrapperType.Name} is fully defined.");
			}
#endif
		}
	}
}
