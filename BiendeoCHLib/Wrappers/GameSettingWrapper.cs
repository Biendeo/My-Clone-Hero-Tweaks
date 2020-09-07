using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("\u031A\u030E\u0315\u0319\u0313\u030D\u0310\u0311\u0315\u0311\u0313")]
	public struct GameSettingWrapper {
		public object GameSetting { get; private set; }

		public static GameSettingWrapper Wrap(object gameSetting) => new GameSettingWrapper {
			GameSetting = gameSetting
		};

		public override bool Equals(object obj) => GameSetting.Equals(obj);

		public override int GetHashCode() => GameSetting.GetHashCode();

		public bool IsNull() => GameSetting == null;

		#region Fields

		public int LowestValue {
			get => lowestValueField(GameSetting);
			set => lowestValueField(GameSetting) = value;
		}
		[WrapperField("\u030F\u031C\u031C\u0312\u030F\u0311\u0317\u0311\u030D\u0318\u0316")]
		private static readonly AccessTools.FieldRef<object, int> lowestValueField;

		public int MaxValue {
			get => maxValueField(GameSetting);
			set => maxValueField(GameSetting) = value;
		}
		[WrapperField("\u0312\u0313\u0313\u0311\u0311\u0310\u0317\u030D\u0315\u030E\u030E")]
		private static readonly AccessTools.FieldRef<object, int> maxValueField;

		public int DefaultValue {
			get => defaultValueField(GameSetting);
			set => defaultValueField(GameSetting) = value;
		}
		[WrapperField("\u0312\u0314\u031B\u030F\u0314\u0319\u030D\u031A\u0317\u0311\u0315")]
		private static readonly AccessTools.FieldRef<object, int> defaultValueField;

		public int IncrementAmount {
			get => incrementAmountField(GameSetting);
			set => incrementAmountField(GameSetting) = value;
		}
		[WrapperField("\u0316\u0310\u030F\u0314\u0310\u031C\u030F\u0310\u031B\u0314\u030E")]
		private static readonly AccessTools.FieldRef<object, int> incrementAmountField;

		public int CurrentInternalValue {
			get => currentInternalValueField(GameSetting);
			set => currentInternalValueField(GameSetting) = value;
		}
		[WrapperField("\u0316\u0310\u031C\u0311\u0314\u0310\u030D\u0311\u031B\u0317\u030E")]
		private static readonly AccessTools.FieldRef<object, int> currentInternalValueField;

		#endregion

		#region Constructors

		public GameSettingWrapper(bool initialValue, bool defaultValue) {
			GameSetting = twoBoolConstructor.Invoke(null, new object[] { initialValue, defaultValue });
		}
		[WrapperConstructor(new Type[] { typeof(bool), typeof(bool) })]
		private static readonly ConstructorInfo twoBoolConstructor;

		public GameSettingWrapper(int initialValue, int defaultValue, int lowestValue, int maxValue, int incrementAmount) {
			GameSetting = fiveIntConstructor.Invoke(null, new object[] { initialValue, defaultValue, lowestValue, maxValue, incrementAmount });
		}
		[WrapperConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
		private static readonly ConstructorInfo fiveIntConstructor;

		#endregion

		#region Properties

		public bool IsDefaultValue => (bool)isDefaultValueProperty.GetValue(GameSetting);
		[WrapperProperty("\u0316\u0310\u0312\u0317\u030E\u0313\u0318\u0313\u030D\u0318\u030F")]
		private static readonly PropertyInfo isDefaultValueProperty;

		public int SetMaxValue {
			set => setMaxValueProperty.SetValue(GameSetting, value);
		}
		[WrapperProperty("\u0317\u0314\u0315\u0314\u031C\u031B\u0314\u031B\u0310\u0316\u0318")]
		private static readonly PropertyInfo setMaxValueProperty;

		public int SetLowestValue {
			set => setLowestValueProperty.SetValue(GameSetting, value);
		}
		[WrapperProperty("\u0315\u0310\u0310\u0316\u0317\u031B\u031B\u0310\u030D\u0312\u030F")]
		private static readonly PropertyInfo setLowestValueProperty;

		public bool GetBoolValue => (bool)getBoolValueProperty.GetValue(GameSetting);
		[WrapperProperty("\u030E\u0311\u0319\u0310\u0317\u0311\u0316\u0314\u031B\u031C\u0315")]
		private static readonly PropertyInfo getBoolValueProperty;

		public float GetFloatPercent => (float)getFloatPercentProperty.GetValue(GameSetting);
		[WrapperProperty("\u030F\u0318\u0310\u031B\u0319\u0313\u0319\u0311\u0316\u031A\u030D")]
		private static readonly PropertyInfo getFloatPercentProperty;

		public float GetFloatSecond => (float)getFloatSecondProperty.GetValue(GameSetting);
		[WrapperProperty("\u0317\u0318\u030E\u031A\u0310\u030F\u0316\u0312\u0314\u0312\u030F")]
		private static readonly PropertyInfo getFloatSecondProperty;

		public int CurrentValue {
			get => (int)currentValueProperty.GetValue(GameSetting);
			set => currentValueProperty.SetValue(GameSetting, value);
		}
		[WrapperProperty("\u0316\u0314\u031C\u0319\u030E\u0310\u031A\u0312\u0317\u0314\u030F")]
		private static readonly PropertyInfo currentValueProperty;

		public string GetBoolString => (string)getBoolStringProperty.GetValue(GameSetting);
		[WrapperProperty("\u031A\u031B\u031C\u0319\u0315\u031A\u0313\u031B\u031B\u0314\u031B")]
		private static readonly PropertyInfo getBoolStringProperty;

		public PlayerProfileWrapper.ControllerType GetControllerType => (PlayerProfileWrapper.ControllerType)getControllerTypeProperty.GetValue(GameSetting);
		[WrapperProperty("\u030E\u0318\u030F\u030F\u030D\u0314\u0316\u031C\u0313\u0318\u0318")]
		private static readonly PropertyInfo getControllerTypeProperty;

		public string GetPercentString => (string)getPercentStringProperty.GetValue(GameSetting);
		[WrapperProperty("\u0311\u031C\u0314\u0315\u030D\u031B\u0317\u0318\u0312\u030E\u030E")]
		private static readonly PropertyInfo getPercentStringProperty;

		public string GetIntString => (string)getIntStringProperty.GetValue(GameSetting);
		[WrapperProperty("\u0311\u0314\u030F\u0319\u0319\u0314\u0316\u031B\u030E\u0310\u0319")]
		private static readonly PropertyInfo getIntStringProperty;

		public string GetIntMSString => (string)getIntMSStringProperty.GetValue(GameSetting);
		[WrapperProperty("\u031B\u031C\u0313\u0312\u0316\u0310\u0318\u030F\u0313\u030D\u0319")]
		private static readonly PropertyInfo getIntMSStringProperty;

		//? Implemented as (float)Math.Pow((double)this.currentValue / 100.0, 1.53)
		public float UnknownFloat1 => (float)unknownFloat1Property.GetValue(GameSetting);
		[WrapperProperty("\u0318\u0319\u0318\u0314\u030D\u0311\u0315\u0319\u0313\u0319\u031C")]
		private static readonly PropertyInfo unknownFloat1Property;

		//? Implemented as (int)(this.UnknownFloat1 * 100f);
		//? Used by VolumeMenu a lot, perhaps it's a decibels thing.
		public int UnknownInt1 => (int)unknownInt1Property.GetValue(GameSetting);
		[WrapperProperty("\u0315\u030E\u0313\u031C\u0311\u030F\u0316\u0311\u030D\u0311\u0316")]
		private static readonly PropertyInfo unknownInt1Property;

		#endregion

		#region Methods

		public void ClampValue() => clampValueMethod(GameSetting);
		[WrapperMethod("\u0317\u0319\u0315\u031B\u0319\u0313\u031C\u0313\u030E\u031A\u030F")]
		private static readonly FastInvokeHandler clampValueMethod;

		public void ClampValue2() => clampValueMethod2(GameSetting);
		[WrapperMethod("\u030E\u0319\u0316\u031B\u031B\u0311\u0310\u030D\u031B\u0311\u031C")]
		private static readonly FastInvokeHandler clampValueMethod2;

		public void ClampValue3() => clampValueMethod3(GameSetting);
		[WrapperMethod("\u030F\u0311\u0317\u0312\u0315\u0315\u030D\u030F\u0311\u030F\u031B")]
		private static readonly FastInvokeHandler clampValueMethod3;

		public void ClampValue4() => clampValueMethod4(GameSetting);
		[WrapperMethod("\u030D\u0314\u0310\u0319\u0317\u0319\u031C\u0311\u0317\u030F\u0311")]
		private static readonly FastInvokeHandler clampValueMethod4;

		public void ClampValue5() => clampValueMethod5(GameSetting);
		[WrapperMethod("\u0314\u0319\u0314\u0318\u0317\u031A\u0319\u0311\u0312\u0318\u0318")]
		private static readonly FastInvokeHandler clampValueMethod5;

		public void Increment() => incrementMethod(GameSetting);
		[WrapperMethod("\u030E\u0318\u0313\u0319\u0313\u0313\u0314\u0311\u0316\u0312\u0312")]
		private static readonly FastInvokeHandler incrementMethod;

		public void Increment2() => incrementMethod2(GameSetting);
		[WrapperMethod("\u0314\u0313\u0314\u0310\u0314\u0319\u0310\u0317\u0313\u030E\u030D")]
		private static readonly FastInvokeHandler incrementMethod2;

		public void Increment3() => incrementMethod3(GameSetting);
		[WrapperMethod("\u031C\u030E\u0317\u0313\u031A\u031B\u031B\u0313\u031B\u0316\u0315")]
		private static readonly FastInvokeHandler incrementMethod3;

		public void Increment4() => incrementMethod4(GameSetting);
		[WrapperMethod("\u0318\u0312\u031B\u030D\u0316\u031A\u0316\u0319\u0317\u030E\u0311")]
		private static readonly FastInvokeHandler incrementMethod4;

		public void Increment5() => incrementMethod5(GameSetting);
		[WrapperMethod("\u0317\u031A\u0310\u0319\u0311\u031C\u030F\u0313\u031C\u0311\u0311")]
		private static readonly FastInvokeHandler incrementMethod5;

		public void Increment6() => incrementMethod6(GameSetting);
		[WrapperMethod("\u030F\u031B\u0315\u0312\u0317\u0316\u031A\u031B\u030E\u0318\u031C")]
		private static readonly FastInvokeHandler incrementMethod6;

		public void Decrement() => decrementMethod(GameSetting);
		[WrapperMethod("\u0313\u031B\u031B\u0313\u0314\u031C\u030F\u031A\u0319\u030E\u0312")]
		private static readonly FastInvokeHandler decrementMethod;

		public void Decrement2() => decrementMethod2(GameSetting);
		[WrapperMethod("\u0310\u0310\u0311\u030E\u031B\u0313\u030E\u030F\u0311\u030F\u0313")]
		private static readonly FastInvokeHandler decrementMethod2;

		public void Decrement3() => decrementMethod3(GameSetting);
		[WrapperMethod("\u0316\u0313\u0310\u031B\u0316\u030F\u031C\u030E\u0313\u0317\u0315")]
		private static readonly FastInvokeHandler decrementMethod3;

		public void Reset() => resetMethod(GameSetting);
		[WrapperMethod("\u0316\u0311\u0317\u0316\u031B\u0315\u0313\u031B\u0318\u0315\u0312")]
		private static readonly FastInvokeHandler resetMethod;

		public void Reset2() => resetMethod2(GameSetting);
		[WrapperMethod("\u0310\u0311\u030D\u0315\u031C\u0313\u0318\u0317\u030F\u030E\u030F")]
		private static readonly FastInvokeHandler resetMethod2;

		public void Reset3() => resetMethod3(GameSetting);
		[WrapperMethod("\u0312\u0317\u0311\u0314\u030F\u0315\u0314\u0317\u030F\u030F\u0314")]
		private static readonly FastInvokeHandler resetMethod3;

		public void Reset4() => resetMethod4(GameSetting);
		[WrapperMethod("\u0316\u0311\u0319\u031B\u0317\u0318\u030D\u0318\u0316\u0311\u0314")]
		private static readonly FastInvokeHandler resetMethod4;

		public void Reset5() => resetMethod5(GameSetting);
		[WrapperMethod("\u0314\u0313\u0312\u0312\u030E\u0319\u031A\u031A\u031C\u0314\u031A")]
		private static readonly FastInvokeHandler resetMethod5;

		public PlayerProfileWrapper.ControllerType GetControllerType2() => (PlayerProfileWrapper.ControllerType)getControllerTypeMethod2(GameSetting);
		[WrapperMethod("\u030E\u0319\u0316\u0317\u0316\u0311\u0317\u0310\u0316\u030E\u030E")]
		private static readonly FastInvokeHandler getControllerTypeMethod2;

		public PlayerProfileWrapper.ControllerType GetControllerType3() => (PlayerProfileWrapper.ControllerType)getControllerTypeMethod3(GameSetting);
		[WrapperMethod("\u0317\u031A\u031A\u0312\u031C\u0319\u031B\u030D\u030D\u0318\u0318")]
		private static readonly FastInvokeHandler getControllerTypeMethod3;

		public bool GetBoolValue2() => (bool)getBoolValueMethod2(GameSetting);
		[WrapperMethod("\u030E\u030E\u0312\u0319\u0317\u0311\u0319\u031A\u031B\u0317\u0317")]
		private static readonly FastInvokeHandler getBoolValueMethod2;

		public static bool operator true(GameSettingWrapper gameSetting) => (bool)operatorTrueMethod(gameSetting, gameSetting.GameSetting);
		[WrapperMethod("\u030F\u030F\u0314\u0319\u0319\u0312\u0313\u030D\u030F\u0311\u0311")]
		private static readonly FastInvokeHandler operatorTrueMethod;

		public static bool operator false(GameSettingWrapper gameSetting) => (bool)operatorFalseMethod(gameSetting, gameSetting.GameSetting);
		[WrapperMethod("\u030E\u030D\u0319\u030E\u0318\u031B\u031A\u0310\u031B\u030F\u0314")]
		private static readonly FastInvokeHandler operatorFalseMethod;

		public static bool operator !(GameSettingWrapper gameSetting) => (bool)operatorLogicalNotMethod(gameSetting, gameSetting.GameSetting);
		[WrapperMethod("\u0314\u0315\u0313\u0314\u0316\u031B\u0318\u030F\u0313\u0316\u0315")]
		private static readonly FastInvokeHandler operatorLogicalNotMethod;

		#endregion
	}
}
