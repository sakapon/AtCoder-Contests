using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class ABC137
	{
		[TestMethod] public void All() => TestAll();
		[TestMethod] public void E_1() => TestOne();
		[TestMethod] public void E_2() => TestOne();
		[TestMethod] public void E_3() => TestOne();

		#region Common Part

		[ClassInitialize] public static void ClassInitialize(TestContext context) => TestJudge.LoadTestClass(context);

		public TestContext TestContext { get; set; }

		void TestAll() => TestJudge.TestAll(TestContext);
		void TestOne() => TestJudge.TestOne(TestContext);

		#endregion
	}
}
