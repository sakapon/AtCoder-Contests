using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EulerLib8.Fractions;
using EulerLib8.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static EulerLib8.Common;

namespace EulerTest.Page002
{
	[TestClass]
	public class P061_070
	{
		#region Test Methods
		[TestMethod] public void T061() => TestHelper.Execute();
		[TestMethod] public void T062() => TestHelper.Execute();
		[TestMethod] public void T063() => TestHelper.Execute();
		[TestMethod] public void T064() => TestHelper.Execute();
		[TestMethod] public void T065() => TestHelper.Execute();
		[TestMethod] public void T066() => TestHelper.Execute();
		[TestMethod] public void T067() => TestHelper.Execute();
		[TestMethod] public void T068() => TestHelper.Execute();
		[TestMethod] public void T069() => TestHelper.Execute();
		[TestMethod] public void T070() => TestHelper.Execute();
		#endregion

		public static object P061()
		{
			return 0;
		}

		public static object P062()
		{
			const int count = 5;

			var dc = new Dictionary<string, int>();
			var dv = new Dictionary<string, long>();

			var r = (s: "", c: 0);

			for (long n = 1; r.c < count; n++)
			{
				var c = n * n * n;
				var cs = c.ToString().ToCharArray();
				Array.Sort(cs);
				var s = new string(cs);
				if (dc.ContainsKey(s))
				{
					dc[s]++;
				}
				else
				{
					dc[s] = 1;
					dv[s] = c;
				}
				ArgHelper.ChFirstMax(ref r, (s, dc[s]), p => p.Item2);
			}
			return dv[r.s];
		}

		public static object P063()
		{
			var r = 0L;
			for (int n = 1; ; n++)
			{
				var t = Enumerable.Range(1, 9).Count(i => BigInteger.Pow(i, n).ToString().Length == n);
				if (t == 0) break;
				r += t;
			}
			return r;
		}

		public static object P064()
		{
			const int nMax = 10000;

			var sqset = Enumerable.Range(1, 100).Select(i => (long)i * i).ToHashSet();
			return Enumerable.Range(1, nMax)
				.Count(n => !sqset.Contains(n) && ContinuedFractions.Expand(new QuadraticIrrational(0, n, 1)).Count() % 2 == 0);
		}

		public static object P065()
		{
			var e = ContinuedFractions.ExpandE().Take(100).ToArray();
			var c = ContinuedFractions.Convergent(e);
			return c.Numerator.ToString().Sum(c => c - '0');
		}

		public static object P066()
		{
			const int dMax = 1000;

			var sqset = Enumerable.Range(1, 100).Select(i => (long)i * i).ToHashSet();
			var r = (d: 0, x: (BigInteger)0);

			for (int d = 1; d <= dMax; d++)
			{
				if (sqset.Contains(d)) continue;

				var (x, _) = ContinuedFractions.Pell(d);
				ArgHelper.ChFirstMax(ref r, (d, x), p => p.x);
			}
			return r.d;
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
