using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerTest
{
	[TestClass]
	public class TestHelper
	{
		static Dictionary<string, Type> types;

		public static void Execute([CallerFilePath] string typeName = "", [CallerMemberName] string methodName = "")
		{
			typeName = Path.GetFileNameWithoutExtension(typeName);
			methodName = methodName.Replace("Test", "P");

			if (types == null)
			{
				var assembly = Assembly.GetCallingAssembly();
				types = assembly.ExportedTypes.ToDictionary(t => t.Name);
			}

			var method = types[typeName].GetMethod(methodName);
			Console.WriteLine(method.Invoke(null, null));
		}

		[TestMethod]
		public void TestAll()
		{
		}
	}
}
