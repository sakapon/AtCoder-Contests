﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EulerLib8.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerTest.Page001
{
	[TestClass]
	public class P011_020
	{
		#region Test Methods
		[TestMethod] public void Test011() => TestHelper.Execute();
		[TestMethod] public void Test012() => TestHelper.Execute();
		[TestMethod] public void Test013() => TestHelper.Execute();
		[TestMethod] public void Test014() => TestHelper.Execute();
		[TestMethod] public void Test015() => TestHelper.Execute();
		[TestMethod] public void Test016() => TestHelper.Execute();
		[TestMethod] public void Test017() => TestHelper.Execute();
		[TestMethod] public void Test018() => TestHelper.Execute();
		[TestMethod] public void Test019() => TestHelper.Execute();
		[TestMethod] public void Test020() => TestHelper.Execute();
		#endregion

		public static object P011()
		{
			return 0;
		}

		public static object P012()
		{
			return 0;
		}

		public static object P013()
		{
			return 0;
		}

		public static object P014()
		{
			return 0;
		}

		public static object P015()
		{
			var ncr = Combination.GetNcrs();
			return ncr[40, 20];
		}

		public static object P016()
		{
			var r = BigInteger.Pow(2, 1000);
			return r.ToString().Sum(c => c - '0');
		}

		public static object P017()
		{
			return 0;
		}

		public static object P018()
		{
			var dp = Input018.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(s => Array.ConvertAll(s.Split(), int.Parse))
				.ToArray();

			for (int i = dp.Length - 2; i >= 0; i--)
				for (int j = 0; j < dp[i].Length; j++)
					dp[i][j] += Math.Max(dp[i + 1][j], dp[i + 1][j + 1]);
			return dp[0][0];
		}

		public static object P019()
		{
			var r12 = Enumerable.Range(1, 12).ToArray();
			return Enumerable.Range(1901, 100).Sum(y => r12.Count(m => new DateTime(y, m, 1).DayOfWeek == DayOfWeek.Sunday));
		}

		public static object P020()
		{
			return 0;
		}

		const string Input018 = @"
75
95 64
17 47 82
18 35 87 10
20 04 82 47 65
19 01 23 75 03 34
88 02 77 73 07 63 67
99 65 04 28 06 16 70 92
41 41 26 56 83 40 80 70 33
41 48 72 33 47 32 37 16 94 29
53 71 44 65 25 43 91 52 97 51 14
70 11 33 28 77 73 17 78 39 68 17 57
91 71 52 38 17 14 91 43 58 50 27 29 48
63 66 04 68 89 53 67 30 73 16 69 87 40 31
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23
";
	}
}