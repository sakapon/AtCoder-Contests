using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class ABC138
	{
		[TestMethod] public void All() => TestAll();
		[TestMethod] public void F_1() => TestOne();
		[TestMethod] public void F_2() => TestOne();
		[TestMethod] public void F_3() => TestOne();

		#region Common Part

		// 静的フィールドは共通で、テストケースごとに初期化されません。複数のテストを実行すると不正確になる場合があります。
		[ClassInitialize] public static void ClassInitialize(TestContext context) => UnitJudge.LoadTestClass(context);

		public TestContext TestContext { get; set; }

		void TestAll() => UnitJudge.TestAll(TestContext);
		void TestOne() => UnitJudge.TestOne(TestContext);

		#endregion
	}
}
