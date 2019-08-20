using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainMethodMap = System.Collections.Generic.Dictionary<string, System.Reflection.MethodInfo>;
using TestCaseMap = System.Collections.Generic.Dictionary<string, UnitTest.TestCase>;

namespace UnitTest
{
	public static class TestJudge
	{
		static readonly Dictionary<string, MainMethodMap> TargetDlls = new Dictionary<string, MainMethodMap>();
		static readonly Dictionary<string, TestCaseMap> TestCases = new Dictionary<string, TestCaseMap>();

		public static void LoadTestClass(TestContext context)
		{
			var testClassName = Type.GetType(context.FullyQualifiedTestClassName).Name;

			var thisDll = Assembly.GetExecutingAssembly();
			var targetDll = Assembly.LoadFile(thisDll.Location.Replace(thisDll.GetName().Name, testClassName));
			TargetDlls[testClassName] = targetDll.DefinedTypes
				.Where(t => t.Name.Length == 1 && 'A' <= t.Name[0] && t.Name[0] <= 'F')
				.ToDictionary(t => t.Name, t => t.GetMethod("Main", BindingFlags.NonPublic | BindingFlags.Static));

			TestCases[testClassName] = File.ReadAllText($@"..\..\..\..\{testClassName}\TestCases.txt")
				.Split("---").Select(s => s.Split('/'))
				.Select(a => new TestCase { Name = a[0].Trim(), Input = a[1].Trim(), Output = a[2].Trim() })
				.ToDictionary(t => t.Name);
		}

		public static void TestAll(TestContext context)
		{
		}

		public static void TestOne(TestContext context)
		{
			var testClassName = Type.GetType(context.FullyQualifiedTestClassName).Name;
			var mainMethod = TargetDlls[testClassName][context.TestName.Split('_')[0]];
			var testCase = TestCases[testClassName][context.TestName];

			using (var reader = new StringReader(testCase.Input))
			using (var writer = new StringWriter())
			{
				Console.SetIn(reader);
				Console.SetOut(writer);

				mainMethod.Invoke(null, null);

				Assert.AreEqual(testCase.Output, writer.ToString().Trim());
			}
		}
	}

	struct TestCase
	{
		public string Name, Input, Output;
	}
}
