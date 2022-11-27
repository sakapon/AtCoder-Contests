using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static EulerLib8.Common;

namespace EulerTest.Page002
{
	[TestClass]
	public class P061_070
	{
		#region Test Methods
		[TestMethod] public void Test061() => TestHelper.Execute();
		[TestMethod] public void Test062() => TestHelper.Execute();
		[TestMethod] public void Test063() => TestHelper.Execute();
		[TestMethod] public void Test064() => TestHelper.Execute();
		[TestMethod] public void Test065() => TestHelper.Execute();
		[TestMethod] public void Test066() => TestHelper.Execute();
		[TestMethod] public void Test067() => TestHelper.Execute();
		[TestMethod] public void Test068() => TestHelper.Execute();
		[TestMethod] public void Test069() => TestHelper.Execute();
		[TestMethod] public void Test070() => TestHelper.Execute();
		#endregion

		public static object P061()
		{
			return 0;
		}

		public static object P062()
		{
			return 0;
		}

		public static object P063()
		{
			return 0;
		}

		public static object P064()
		{
			return 0;
		}

		public static object P065()
		{
			return 0;
		}

		public static object P066()
		{
			return 0;
		}

		public static object P067()
		{
			var dp = GetText(Url067).Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(s => Array.ConvertAll(s.Split(), int.Parse))
				.ToArray();

			for (int i = dp.Length - 2; i >= 0; i--)
				for (int j = 0; j < dp[i].Length; j++)
					dp[i][j] += Math.Max(dp[i + 1][j], dp[i + 1][j + 1]);
			return dp[0][0];
		}

		public static object P068()
		{
			return 0;
		}

		public static object P069()
		{
			return 0;
		}

		public static object P070()
		{
			return 0;
		}

		const string Url067 = "https://projecteuler.net/project/resources/p067_triangle.txt";
	}
}
