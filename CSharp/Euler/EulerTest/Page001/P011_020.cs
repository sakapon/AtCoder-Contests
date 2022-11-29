using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EulerLib8.Linq;
using EulerLib8.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerTest.Page001
{
	[TestClass]
	public class P011_020
	{
		#region Test Methods
		[TestMethod] public void T011() => TestHelper.Execute();
		[TestMethod] public void T012() => TestHelper.Execute();
		[TestMethod] public void T013() => TestHelper.Execute();
		[TestMethod] public void T014() => TestHelper.Execute();
		[TestMethod] public void T015() => TestHelper.Execute();
		[TestMethod] public void T016() => TestHelper.Execute();
		[TestMethod] public void T017() => TestHelper.Execute();
		[TestMethod] public void T018() => TestHelper.Execute();
		[TestMethod] public void T019() => TestHelper.Execute();
		[TestMethod] public void T020() => TestHelper.Execute();
		#endregion

		public static object P011()
		{
			return 0;
		}

		public static object P012()
		{
			const int n_max = 100000;

			var pf = new PrimeFactorization(n_max);
			for (int i = 1; i < n_max; i++)
			{
				var f0 = pf.GetFactors(i);
				var f1 = pf.GetFactors(i + 1);
				var c = f0.Concat(f1)
					.GroupBy(x => x)
					.Aggregate(1, (v, g) => v * (g.Count() + (g.Key == 2 ? 0 : 1)));
				if (c > 500) return i * (i + 1) / 2;
			}
			return 0;
		}

		public static object P013()
		{
			return 0;
		}

		public static object P014()
		{
			const int n = 1000000;

			var dp = new Dictionary<long, int>();
			dp[1] = 0;
			var p = Enumerable.Range(1, n - 1).Select(i => (i, c: GetCost(i))).FirstMax(p => p.c);
			return p.i;

			int GetCost(long v)
			{
				if (!dp.TryGetValue(v, out var c))
				{
					var nv = Next(v);
					dp[v] = c = GetCost(nv) + 1;
				}
				return c;
			}

			static long Next(long v) => (v & 1) == 0 ? v / 2 : 3 * v + 1;
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
			var r = Enumerable.Range(1, 100).Aggregate((BigInteger)1, (x, y) => x * y);
			return r.ToString().Sum(c => c - '0');
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
