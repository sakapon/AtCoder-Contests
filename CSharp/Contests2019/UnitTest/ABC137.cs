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

		// 静的フィールドは共通で、テストケースごとに初期化されません。複数のテストを実行すると不正確になる場合があります。
		[ClassInitialize] public static void ClassInitialize(TestContext context) => UnitJudge.LoadTestClass(context);

		public TestContext TestContext { get; set; }

		void TestAll() => UnitJudge.TestAll(TestContext);
		void TestOne() => UnitJudge.TestOne(TestContext);

		#endregion
	}
}
