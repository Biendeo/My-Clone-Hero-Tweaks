using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Wrappers {
	[Wrapper("\u030E\u0317\u0316\u031A\u0318\u0314\u0315\u030F\u0315\u0313\u0311")]
	internal class INIParserWrapper {
		public readonly object iniParser;

		public INIParserWrapper(object iniParser) {
			this.iniParser = iniParser;
		}

		#region Methods

		public void Open(string path) => openMethod.Invoke(iniParser, new object[] { path });
		[WrapperMethod("\u0318\u0318\u030F\u0316\u0314\u0317\u031A\u0314\u0310\u031A\u031B", new Type[] { typeof(string) })]
		private static readonly MethodInfo openMethod;

		public void Close() => closeMethod.Invoke(iniParser, Array.Empty<object>());
		[WrapperMethod("\u031A\u031C\u0318\u030E\u030F\u0317\u030E\u030D\u030D\u030D\u0315")]
		private static readonly MethodInfo closeMethod;

		public string ReadValue(string SectionName, string Key, string DefaultValue) => (string)readValue1Method.Invoke(iniParser, new object[] { SectionName, Key, DefaultValue });
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(string) })]
		private static readonly MethodInfo readValue1Method;

		public bool ReadValue(string SectionName, string Key, bool DefaultValue) => (bool)readValue2Method.Invoke(iniParser, new object[] { SectionName, Key, DefaultValue });
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(bool) })]
		private static readonly MethodInfo readValue2Method;

		public int ReadValue(string SectionName, string Key, int DefaultValue) => (int)readValue3Method.Invoke(iniParser, new object[] { SectionName, Key, DefaultValue });
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(int) })]
		private static readonly MethodInfo readValue3Method;

		public long ReadValue(string SectionName, string Key, long DefaultValue) => (long)readValue4Method.Invoke(iniParser, new object[] { SectionName, Key, DefaultValue });
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(long) })]
		private static readonly MethodInfo readValue4Method;

		public double ReadValue(string SectionName, string Key, double DefaultValue) => (double)readValue5Method.Invoke(iniParser, new object[] { SectionName, Key, DefaultValue });
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(double) })]
		private static readonly MethodInfo readValue5Method;

		public byte[] ReadValue(string SectionName, string Key, byte[] DefaultValue) => (byte[])readValue6Method.Invoke(iniParser, new object[] { SectionName, Key, DefaultValue });
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(byte[]) })]
		private static readonly MethodInfo readValue6Method;

		public DateTime ReadValue(string SectionName, string Key, DateTime DefaultValue) => (DateTime)readValue7Method.Invoke(iniParser, new object[] { SectionName, Key, DefaultValue });
		[WrapperMethod("\u0312\u031A\u030F\u0319\u0312\u0316\u0318\u031C\u030E\u0316\u030F", new Type[] { typeof(string), typeof(string), typeof(DateTime) })]
		private static readonly MethodInfo readValue7Method;

		public bool IsKeyExists(string SectionName, string Key) => (bool)isKeyExistsMethod.Invoke(iniParser, new object[] { SectionName, Key });
		[WrapperMethod("\u031C\u0310\u030D\u031C\u031C\u031C\u030D\u030D\u030D\u0318\u0317")]
		private static readonly MethodInfo isKeyExistsMethod;

		#endregion
	}
}
