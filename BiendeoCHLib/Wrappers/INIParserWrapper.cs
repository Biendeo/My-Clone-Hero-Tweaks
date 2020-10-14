using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("\u030E\u0317\u0316\u031A\u0318\u0314\u0315\u030F\u0315\u0313\u0311")]
	public struct INIParserWrapper {
		public object INIParser { get; private set; }

		public static INIParserWrapper Wrap(object iniParser) => new INIParserWrapper {
			INIParser = iniParser
		};

		public override bool Equals(object obj) => INIParser.Equals(obj);

		public override int GetHashCode() => INIParser.GetHashCode();

		public bool IsNull() => INIParser == null;

		#region Methods

		public void Open(string path) => openMethod(INIParser, path);
		[WrapperMethod("\u0318\u0318\u030F\u0316\u0314\u0317\u031A\u0314\u0310\u031A\u031B", new Type[] { typeof(string) })]
		private static readonly FastInvokeHandler openMethod;

		public void Close() => closeMethod(INIParser);
		[WrapperMethod("\u031A\u031C\u0318\u030E\u030F\u0317\u030E\u030D\u030D\u030D\u0315")]
		private static readonly FastInvokeHandler closeMethod;

		public string ReadValue(string SectionName, string Key, string DefaultValue) => (string)readValue1Method(INIParser, SectionName, Key, DefaultValue);
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(string) })]
		private static readonly FastInvokeHandler readValue1Method;

		public bool ReadValue(string SectionName, string Key, bool DefaultValue) => (bool)readValue2Method(INIParser, SectionName, Key, DefaultValue);
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(bool) })]
		private static readonly FastInvokeHandler readValue2Method;

		public int ReadValue(string SectionName, string Key, int DefaultValue) => (int)readValue3Method(INIParser, SectionName, Key, DefaultValue);
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(int) })]
		private static readonly FastInvokeHandler readValue3Method;

		public long ReadValue(string SectionName, string Key, long DefaultValue) => (long)readValue4Method(INIParser, SectionName, Key, DefaultValue);
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(long) })]
		private static readonly FastInvokeHandler readValue4Method;

		public double ReadValue(string SectionName, string Key, double DefaultValue) => (double)readValue5Method(INIParser, SectionName, Key, DefaultValue);
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(double) })]
		private static readonly FastInvokeHandler readValue5Method;

		public byte[] ReadValue(string SectionName, string Key, byte[] DefaultValue) => (byte[])readValue6Method(INIParser, SectionName, Key, DefaultValue);
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(byte[]) })]
		private static readonly FastInvokeHandler readValue6Method;

		public DateTime ReadValue(string SectionName, string Key, DateTime DefaultValue) => (DateTime)readValue7Method(INIParser, SectionName, Key, DefaultValue);
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(DateTime) })]
		private static readonly FastInvokeHandler readValue7Method;

		public bool IsKeyExists(string SectionName, string Key) => (bool)isKeyExistsMethod(INIParser, SectionName, Key);
		[WrapperMethod("\u031C\u0310\u030D\u031C\u031C\u031C\u030D\u030D\u030D\u0318\u0317")]
		private static readonly FastInvokeHandler isKeyExistsMethod;

		#endregion
	}
}
