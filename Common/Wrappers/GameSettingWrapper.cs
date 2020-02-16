using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Wrappers {
	[Wrapper("\u031A\u030E\u0315\u0319\u0313\u030D\u0310\u0311\u0315\u0311\u0313")]
	internal class GameSettingWrapper {
		public readonly object gameSetting;

		public GameSettingWrapper(object gameSetting) {
			this.gameSetting = gameSetting;
		}

		#region Fields

		public int lowestValue => (int)lowestValueField.GetValue(gameSetting);
		[WrapperField("\u030F\u031C\u031C\u0312\u030F\u0311\u0317\u0311\u030D\u0318\u0316")]
		private static readonly FieldInfo lowestValueField;

		public int maxValue => (int)maxValueField.GetValue(gameSetting);
		[WrapperField("\u0312\u0313\u0313\u0311\u0311\u0310\u0317\u030D\u0315\u030E\u030E")]
		private static readonly FieldInfo maxValueField;

		public int defaultValue => (int)defaultValueField.GetValue(gameSetting);
		[WrapperField("\u0312\u0314\u031B\u030F\u0314\u0319\u030D\u031A\u0317\u0311\u0315")]
		private static readonly FieldInfo defaultValueField;

		public int incrementAmount => (int)incrementAmountField.GetValue(gameSetting);
		[WrapperField("\u0316\u0310\u030F\u0314\u0310\u031C\u030F\u0310\u031B\u0314\u030E")]
		private static readonly FieldInfo incrementAmountField;

		public int currentValue => (int)currentValueField.GetValue(gameSetting);
		[WrapperField("\u0316\u0310\u031C\u0311\u0314\u0310\u030D\u0311\u031B\u0317\u030E")]
		private static readonly FieldInfo currentValueField;

		#endregion

		#region Constructors

		public GameSettingWrapper(bool initialValue, bool defaultValue) {
			gameSetting = twoBoolConstructor.Invoke(null, new object[] { initialValue, defaultValue });
		}
		[WrapperConstructor(new Type[] { typeof(bool), typeof(bool) })]
		private static readonly ConstructorInfo twoBoolConstructor;

		public GameSettingWrapper(int initialValue, int defaultValue, int lowestValue, int maxValue, int incrementAmount) {
			gameSetting = fiveIntConstructor.Invoke(null, new object[] { initialValue, defaultValue, lowestValue, maxValue, incrementAmount });
		}
		[WrapperConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
		private static readonly ConstructorInfo fiveIntConstructor;

		#endregion

		#region Properties

		public bool IsDefaultValue => (bool)isDefaultValueProperty.GetValue(gameSetting);
		[WrapperProperty("\u0316\u0310\u0312\u0317\u030E\u0313\u0318\u0313\u030D\u0318\u030F")]
		private static readonly PropertyInfo isDefaultValueProperty;

		public int SetMaxValue {
			set => setMaxValueProperty.SetValue(gameSetting, value);
		}
		[WrapperProperty("\u0317\u0314\u0315\u0314\u031C\u031B\u0314\u031B\u0310\u0316\u0318")]
		private static readonly PropertyInfo setMaxValueProperty;

		public int SetLowestValue {
			set => setLowestValueProperty.SetValue(gameSetting, value);
		}
		[WrapperProperty("\u0315\u0310\u0310\u0316\u0317\u031B\u031B\u0310\u030D\u0312\u030F")]
		private static readonly PropertyInfo setLowestValueProperty;

		public bool GetBoolValue => (bool)getBoolValueProperty.GetValue(gameSetting);
		[WrapperProperty("\u030E\u0311\u0319\u0310\u0317\u0311\u0316\u0314\u031B\u031C\u0315")]
		private static readonly PropertyInfo getBoolValueProperty;

		public float GetFloatPercent => (float)getFloatPercentProperty.GetValue(gameSetting);
		[WrapperProperty("\u030F\u0318\u0310\u031B\u0319\u0313\u0319\u0311\u0316\u031A\u030D")]
		private static readonly PropertyInfo getFloatPercentProperty;

		public float GetFloatSecond => (float)getFloatSecondProperty.GetValue(gameSetting);
		[WrapperProperty("\u0317\u0318\u030E\u031A\u0310\u030F\u0316\u0312\u0314\u0312\u030F")]
		private static readonly PropertyInfo getFloatSecondProperty;

		public int CurrentValue {
			get => (int)currentValueProperty.GetValue(gameSetting);
			set => currentValueProperty.SetValue(gameSetting, value);
		}
		[WrapperProperty("\u0316\u0314\u031C\u0319\u030E\u0310\u031A\u0312\u0317\u0314\u030F")]
		private static readonly PropertyInfo currentValueProperty;

		public string GetBoolString => (string)getBoolStringProperty.GetValue(gameSetting);
		[WrapperProperty("\u031A\u031B\u031C\u0319\u0315\u031A\u0313\u031B\u031B\u0314\u031B")]
		private static readonly PropertyInfo getBoolStringProperty;

		public PlayerProfileWrapper.ControllerType GetControllerType => (PlayerProfileWrapper.ControllerType)getControllerTypeProperty.GetValue(gameSetting);
		[WrapperProperty("\u030E\u0318\u030F\u030F\u030D\u0314\u0316\u031C\u0313\u0318\u0318")]
		private static readonly PropertyInfo getControllerTypeProperty;

		public string GetPercentString => (string)getPercentStringProperty.GetValue(gameSetting);
		[WrapperProperty("\u0311\u031C\u0314\u0315\u030D\u031B\u0317\u0318\u0312\u030E\u030E")]
		private static readonly PropertyInfo getPercentStringProperty;

		public string GetIntString => (string)getIntStringProperty.GetValue(gameSetting);
		[WrapperProperty("\u0311\u0314\u030F\u0319\u0319\u0314\u0316\u031B\u030E\u0310\u0319")]
		private static readonly PropertyInfo getIntStringProperty;

		public string GetIntMSString => (string)getIntMSStringProperty.GetValue(gameSetting);
		[WrapperProperty("\u031B\u031C\u0313\u0312\u0316\u0310\u0318\u030F\u0313\u030D\u0319")]
		private static readonly PropertyInfo getIntMSStringProperty;

		//? Implemented as (float)Math.Pow((double)this.currentValue / 100.0, 1.53)
		public float UnknownFloat1 => (float)unknownFloat1Property.GetValue(gameSetting);
		[WrapperProperty("\u0318\u0319\u0318\u0314\u030D\u0311\u0315\u0319\u0313\u0319\u031C")]
		private static readonly PropertyInfo unknownFloat1Property;

		//? Implemented as (int)(this.UnknownFloat1 * 100f);
		//? Used by VolumeMenu a lot, perhaps it's a decibels thing.
		public int UnknownInt1 => (int)unknownInt1Property.GetValue(gameSetting);
		[WrapperProperty("\u0315\u030E\u0313\u031C\u0311\u030F\u0316\u0311\u030D\u0311\u0316")]
		private static readonly PropertyInfo unknownInt1Property;

		#endregion

		#region Methods

		public void ClampValue() => clampValueMethod.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0317\u0319\u0315\u031B\u0319\u0313\u031C\u0313\u030E\u031A\u030F")]
		private static readonly MethodInfo clampValueMethod;

		public void ClampValue2() => clampValueMethod2.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u030E\u0319\u0316\u031B\u031B\u0311\u0310\u030D\u031B\u0311\u031C")]
		private static readonly MethodInfo clampValueMethod2;

		public void ClampValue3() => clampValueMethod3.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u030F\u0311\u0317\u0312\u0315\u0315\u030D\u030F\u0311\u030F\u031B")]
		private static readonly MethodInfo clampValueMethod3;

		public void ClampValue4() => clampValueMethod4.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u030D\u0314\u0310\u0319\u0317\u0319\u031C\u0311\u0317\u030F\u0311")]
		private static readonly MethodInfo clampValueMethod4;

		public void ClampValue5() => clampValueMethod5.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0314\u0319\u0314\u0318\u0317\u031A\u0319\u0311\u0312\u0318\u0318")]
		private static readonly MethodInfo clampValueMethod5;

		public void Increment() => incrementMethod.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u030E\u0318\u0313\u0319\u0313\u0313\u0314\u0311\u0316\u0312\u0312")]
		private static readonly MethodInfo incrementMethod;

		public void Increment2() => incrementMethod2.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0314\u0313\u0314\u0310\u0314\u0319\u0310\u0317\u0313\u030E\u030D")]
		private static readonly MethodInfo incrementMethod2;

		public void Increment3() => incrementMethod3.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u031C\u030E\u0317\u0313\u031A\u031B\u031B\u0313\u031B\u0316\u0315")]
		private static readonly MethodInfo incrementMethod3;

		public void Increment4() => incrementMethod4.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0318\u0312\u031B\u030D\u0316\u031A\u0316\u0319\u0317\u030E\u0311")]
		private static readonly MethodInfo incrementMethod4;

		public void Increment5() => incrementMethod5.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0317\u031A\u0310\u0319\u0311\u031C\u030F\u0313\u031C\u0311\u0311")]
		private static readonly MethodInfo incrementMethod5;

		public void Increment6() => incrementMethod6.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u030F\u031B\u0315\u0312\u0317\u0316\u031A\u031B\u030E\u0318\u031C")]
		private static readonly MethodInfo incrementMethod6;

		public void Decrement() => decrementMethod.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0313\u031B\u031B\u0313\u0314\u031C\u030F\u031A\u0319\u030E\u0312")]
		private static readonly MethodInfo decrementMethod;

		public void Decrement2() => decrementMethod2.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0310\u0310\u0311\u030E\u031B\u0313\u030E\u030F\u0311\u030F\u0313")]
		private static readonly MethodInfo decrementMethod2;

		public void Decrement3() => decrementMethod3.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0316\u0313\u0310\u031B\u0316\u030F\u031C\u030E\u0313\u0317\u0315")]
		private static readonly MethodInfo decrementMethod3;

		public void Reset() => resetMethod.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0316\u0311\u0317\u0316\u031B\u0315\u0313\u031B\u0318\u0315\u0312")]
		private static readonly MethodInfo resetMethod;

		public void Reset2() => resetMethod2.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0310\u0311\u030D\u0315\u031C\u0313\u0318\u0317\u030F\u030E\u030F")]
		private static readonly MethodInfo resetMethod2;

		public void Reset3() => resetMethod3.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0312\u0317\u0311\u0314\u030F\u0315\u0314\u0317\u030F\u030F\u0314")]
		private static readonly MethodInfo resetMethod3;

		public void Reset4() => resetMethod4.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0316\u0311\u0319\u031B\u0317\u0318\u030D\u0318\u0316\u0311\u0314")]
		private static readonly MethodInfo resetMethod4;

		public void Reset5() => resetMethod5.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0314\u0313\u0312\u0312\u030E\u0319\u031A\u031A\u031C\u0314\u031A")]
		private static readonly MethodInfo resetMethod5;

		public PlayerProfileWrapper.ControllerType GetControllerType2() => (PlayerProfileWrapper.ControllerType)getControllerTypeMethod2.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u030E\u0319\u0316\u0317\u0316\u0311\u0317\u0310\u0316\u030E\u030E")]
		private static readonly MethodInfo getControllerTypeMethod2;

		public PlayerProfileWrapper.ControllerType GetControllerType3() => (PlayerProfileWrapper.ControllerType)getControllerTypeMethod3.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u0317\u031A\u031A\u0312\u031C\u0319\u031B\u030D\u030D\u0318\u0318")]
		private static readonly MethodInfo getControllerTypeMethod3;

		public bool GetBoolValue2() => (bool)getBoolValueMethod2.Invoke(gameSetting, Array.Empty<object>());
		[WrapperMethod("\u030E\u030E\u0312\u0319\u0317\u0311\u0319\u031A\u031B\u0317\u0317")]
		private static readonly MethodInfo getBoolValueMethod2;

		public static bool operator true(GameSettingWrapper gameSetting) => (bool)operatorTrueMethod.Invoke(gameSetting, new object[] { gameSetting.gameSetting });
		[WrapperMethod("\u030F\u030F\u0314\u0319\u0319\u0312\u0313\u030D\u030F\u0311\u0311")]
		private static readonly MethodInfo operatorTrueMethod;

		public static bool operator false(GameSettingWrapper gameSetting) => (bool)operatorFalseMethod.Invoke(gameSetting, new object[] { gameSetting.gameSetting });
		[WrapperMethod("\u030E\u030D\u0319\u030E\u0318\u031B\u031A\u0310\u031B\u030F\u0314")]
		private static readonly MethodInfo operatorFalseMethod;

		public static bool operator !(GameSettingWrapper gameSetting) => (bool)operatorLogicalNotMethod.Invoke(gameSetting, new object[] { gameSetting.gameSetting });
		[WrapperMethod("\u0314\u0315\u0313\u0314\u0316\u031B\u0318\u030F\u0313\u0316\u0315")]
		private static readonly MethodInfo operatorLogicalNotMethod;

		#endregion
	}
}
