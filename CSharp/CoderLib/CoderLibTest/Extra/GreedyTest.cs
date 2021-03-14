using System;
using CoderLib8.Extra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Extra
{
	[TestClass]
	public class GreedyTest
	{
		[TestMethod]
		public void KthTrueDay_1()
		{
			var period = 9L;
			var trueDays = new long[] { 0, 2, 6 };

			void Test(long expected, long k)
			{
				Assert.AreEqual(expected, GreedyHelper.KthTrueDay(k, period, trueDays));
			}

			var es = new long[] { 0, 2, 6, 9, 11, 15, 18 };
			for (int i = 0; i < es.Length; i++)
				Test(es[i], i);
		}

		[TestMethod]
		public void KthTrueDay_2()
		{
			var period = 9L;
			var trueDays = new long[] { 1, 5, 8 };

			void Test(long expected, long k)
			{
				Assert.AreEqual(expected, GreedyHelper.KthTrueDay(k, period, trueDays));
			}

			var es = new long[] { 1, 5, 8, 10, 14, 17, 19 };
			for (int i = 0; i < es.Length; i++)
				Test(es[i], i);
		}

		[TestMethod]
		public void KthFalseDay_1()
		{
			var period = 9L;
			var trueDays = new long[] { 0, 2, 6 };

			void Test(long expected, long k)
			{
				Assert.AreEqual(expected, GreedyHelper.KthFalseDay(k, period, trueDays));
			}

			var es = new long[] { 1, 3, 4, 5, 7, 8, 10, 12, 13, 14, 16, 17, 19, 21 };
			for (int i = 0; i < es.Length; i++)
				Test(es[i], i);
		}

		[TestMethod]
		public void KthFalseDay_2()
		{
			var period = 9L;
			var trueDays = new long[] { 1, 5, 8 };

			void Test(long expected, long k)
			{
				Assert.AreEqual(expected, GreedyHelper.KthFalseDay(k, period, trueDays));
			}

			var es = new long[] { 0, 2, 3, 4, 6, 7, 9, 11, 12, 13, 15, 16, 18, 20 };
			for (int i = 0; i < es.Length; i++)
				Test(es[i], i);
		}
	}
}
