using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class ABC137
	{
		[TestMethod] public void All() => TestAll();
		[TestMethod] public void E_1() => Test();
		[TestMethod] public void E_2() => Test();
		[TestMethod] public void E_3() => Test();

		struct TestCase
		{
			public string Name, Input, Output;
		}

		static readonly Assembly TargetAssembly = LoadAssembly();
		static Assembly LoadAssembly([CallerFilePath]string path = "")
		{
			var thisDll = Assembly.GetExecutingAssembly();
			var dllName = Path.GetFileNameWithoutExtension(path);
			return Assembly.LoadFile(thisDll.Location.Replace("UnitTest", dllName));
		}

		static readonly Dictionary<string, TestCase> TestCases = GetTestCases();
		static Dictionary<string, TestCase> GetTestCases([CallerFilePath]string path = "") =>
			File.ReadAllText($@"..\..\..\..\{Path.GetFileNameWithoutExtension(path)}\TestCases.txt")
				.Split("---").Select(s => s.Split('/'))
				.Select(a => new TestCase { Name = a[0].Trim(), Input = a[1].Trim(), Output = a[2].Trim() })
				.ToDictionary(t => t.Name);

		static void TestAll()
		{
		}

		static void Test([CallerFilePath]string path = "", [CallerMemberName]string name = "")
		{
			using (var reader = new StringReader(TestCases[name].Input))
			using (var writer = new StringWriter())
			{
				Console.SetIn(reader);
				Console.SetOut(writer);

				var type = TargetAssembly.DefinedTypes.First(t => t.Name == name.Split('_')[0]);
				type.GetMethod("Main", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);

				Assert.AreEqual(TestCases[name].Output, writer.ToString().Trim());
			}
		}
	}
}
