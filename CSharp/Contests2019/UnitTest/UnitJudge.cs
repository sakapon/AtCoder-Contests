using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainMethodMap = System.Collections.Generic.Dictionary<string, System.Reflection.MethodInfo>;
using TestCaseMap = System.Collections.Generic.Dictionary<string, UnitTest.TestCase>;

namespace UnitTest
{
	[DebuggerNonUserCode]
	public static class UnitJudge
	{
		static readonly Dictionary<string, MainMethodMap> TargetDlls = new Dictionary<string, MainMethodMap>();
		static readonly Dictionary<string, TestCaseMap> TestCases = new Dictionary<string, TestCaseMap>();

		static void LoadDll(string contestName)
		{
			var thisDll = Assembly.GetExecutingAssembly();
			var path = thisDll.Location.Replace(thisDll.GetName().Name, contestName);
			if (!File.Exists(path)) throw new InvalidOperationException("The target DLL is not found.");

			var targetDll = Assembly.LoadFile(path);
			TargetDlls[contestName] = targetDll.DefinedTypes
				.Where(t => t.Name.Length == 1 && 'A' <= t.Name[0] && t.Name[0] <= 'F')
				.ToDictionary(t => t.Name, t => t.GetMethod("Main", BindingFlags.NonPublic | BindingFlags.Static));
		}

		static void LoadTestCases(string contestName)
		{
			var path = $@"..\..\..\..\{contestName}\TestCases.txt";
			if (!File.Exists(path)) throw new InvalidOperationException("TestCases.txt is not found.");

			TestCases[contestName] = File.ReadAllText(path)
				.Split("---").Select(s => s.Split('/'))
				.Select(a => new TestCase { Name = a[0].Trim(), Input = a[1].Trim(), Output = a[2].Trim() })
				.ToDictionary(t => t.Name);
		}

		static void TestOne(string contestName, string testName)
		{
			var mainMethod = TargetDlls[contestName][testName.Split('_')[0]];
			var testCase = TestCases[contestName][testName];

			using (var reader = new StringReader(testCase.Input))
			using (var writer = new StringWriter())
			{
				Console.SetIn(reader);
				Console.SetOut(writer);

				mainMethod.Invoke(null, null);

				Assert.AreEqual(testCase.Output, writer.ToString().Trim());
			}
		}

		public static void LoadTestClass(TestContext context)
		{
			var contestName = Type.GetType(context.FullyQualifiedTestClassName).Name;
			LoadDll(contestName);
			LoadTestCases(contestName);
		}

		public static void TestAll(TestContext context)
		{
			var contestName = Type.GetType(context.FullyQualifiedTestClassName).Name;
			foreach (var test in TestCases[contestName].Values)
				TestOne(contestName, test.Name);
		}

		public static void TestOne(TestContext context)
		{
			var contestName = Type.GetType(context.FullyQualifiedTestClassName).Name;
			TestOne(contestName, context.TestName);
		}
	}

	struct TestCase
	{
		public string Name, Input, Output;
	}
}
