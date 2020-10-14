using BepInEx.Logging;
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

			var staticFieldRefAccessMethod = AccessTools.FirstMethod(typeof(AccessTools), f => f.Name == nameof(AccessTools.StaticFieldRefAccess) && f.GetParameters().Length == 1 && f.GetParameters().Single().ParameterType == typeof(FieldInfo) && f.ContainsGenericParameters && f.GetGenericArguments().Length == 1);
			var fieldRefAccessMethod = AccessTools.Method(typeof(AccessTools), nameof(AccessTools.FieldRefAccess), new Type[] { typeof(FieldInfo) });

			foreach (var field in wrapperType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) {
				if (field.FieldType.IsGenericType && (typeof(AccessTools.FieldRef<object, object>).GetGenericTypeDefinition() == field.FieldType.GetGenericTypeDefinition() || typeof(AccessTools.FieldRef<object>).GetGenericTypeDefinition() == field.FieldType.GetGenericTypeDefinition()) && field.GetValue(null) == null) {
					var wrapperMember = field.GetCustomAttribute<WrapperField>();
					if (wrapperMember != null) {
						var fieldInfo = WrappedType.GetField(wrapperMember.ObfuscatedName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
						if (fieldInfo != null) {
							if (field.FieldType.GenericTypeArguments.Length == 1) {
								//TODO: Figure out how to do statics.
								field.SetValue(null, staticFieldRefAccessMethod.MakeGenericMethod(field.FieldType.GenericTypeArguments).Invoke(null, new object[] { fieldInfo }));
							} else if (field.FieldType.GenericTypeArguments.Length == 2) {
								field.SetValue(null, fieldRefAccessMethod.MakeGenericMethod(field.FieldType.GenericTypeArguments).Invoke(null, new object[] { fieldInfo }));
							} else {
								logger.LogError($"Field {wrapperType.Name}.{field.Name} has an incorrect number of generic arguments! Panic!");
								Environment.Exit(1);
							}
							fieldsSeen.Add(fieldInfo);
							logger.LogDebug($"Loaded field {wrapperType.Name}.{field.Name}");
						}
					}
				} else if (field.FieldType == typeof(FieldInfo) && field.GetValue(null) == null) {
					var wrapperMember = field.GetCustomAttribute<WrapperField>();
					if (wrapperMember != null) {
						var fieldInfo = WrappedType.GetField(wrapperMember.ObfuscatedName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
						if (fieldInfo != null) {
							field.SetValue(null, fieldInfo);
							fieldsSeen.Add(fieldInfo);
							logger.LogDebug($"Loaded field {wrapperType.Name}.{field.Name}");
#if DEBUG
							if (!fieldInfo.IsStatic) {
								logger.LogWarning($"This is a FieldInfo field, please replace it with a FieldRefAccess version!");
							}
#endif
						}
					}
				} else if (field.FieldType == typeof(PropertyInfo) && field.GetValue(null) == null) {
					var wrapperProperty = field.GetCustomAttribute<WrapperProperty>();
					if (wrapperProperty != null) {
						var propertyInfo = WrappedType.GetProperty(wrapperProperty.ObfuscatedName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
						if (propertyInfo != null) {
							field.SetValue(null, propertyInfo);
							propertiesSeen.Add(propertyInfo);
							logger.LogDebug($"Loaded property {wrapperType.Name}.{field.Name}");
						}
					}
				} else if (field.FieldType == typeof(FastInvokeHandler) && field.GetValue(null) == null) {
					var wrapperMethod = field.GetCustomAttribute<WrapperMethod>();
					if (wrapperMethod != null) {
						var methodInfo = wrapperMethod.GetMethodInfo(WrappedType);
						if (methodInfo != null) {
							field.SetValue(null, MethodInvoker.GetHandler(methodInfo));
							methodsSeen.Add(methodInfo);
							logger.LogDebug($"Loaded method {wrapperType.Name}.{field.Name}");
						}
					}
				} else if (field.FieldType == typeof(ConstructorInfo) && field.GetValue(null) == null) {
					var wrapperConstructor = field.GetCustomAttribute<WrapperConstructor>();
					if (wrapperConstructor != null) {
						var constructorInfo = WrappedType.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, wrapperConstructor.Types, null);
						if (constructorInfo != null) {
							field.SetValue(null, constructorInfo);
							constructorsSeen.Add(constructorInfo);
							logger.LogDebug($"Loaded constructor {wrapperType.Name}.{field.Name}");
						}
					}
				}
				if ((field.FieldType == typeof(FieldInfo) || (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(AccessTools.FieldRef<,>)) || field.FieldType == typeof(PropertyInfo) || field.FieldType == typeof(MethodInfo) || field.FieldType == typeof(FastInvokeHandler) || field.FieldType == typeof(ConstructorInfo)) && field.GetValue(null) == null) {
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
				logger.LogDebug($"Wrapper {wrapperType.Name} is fully defined.");
			}
#endif
		}
	}
}
