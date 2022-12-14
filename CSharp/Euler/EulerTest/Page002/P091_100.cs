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
	public class P091_100
	{
		#region Test Methods
		[TestMethod] public void T091() => TestHelper.Execute();
		[TestMethod] public void T092() => TestHelper.Execute();
		[TestMethod] public void T093() => TestHelper.Execute();
		[TestMethod] public void T094() => TestHelper.Execute();
		[TestMethod] public void T095() => TestHelper.Execute();
		[TestMethod] public void T096() => TestHelper.Execute();
		[TestMethod] public void T097() => TestHelper.Execute();
		[TestMethod] public void T098() => TestHelper.Execute();
		[TestMethod] public void T099() => TestHelper.Execute();
		[TestMethod] public void T100() => TestHelper.Execute();
		#endregion

		public static object P091()
		{
			const int n = 50;

			var r = 0;
			for (int x = 0; x <= n; x++)
			{
				for (int y = 0; y <= n; y++)
				{
					if ((x, y) == (0, 0)) continue;

					var g = Gcd(x, y);
					var (dx, dy) = (y / g, -x / g);

					for (var (u, v) = (x + dx, y + dy); u <= n && v >= 0; u += dx, v += dy)
					{
						r++;
					}
				}
			}
			r <<= 1;
			r += n * n;
			return r;

			// モノイドとする場合 (単位元は 0)
			static int Gcd(int a, int b) { if (b == 0) return a; for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
		}

		public static object P092()
		{
			const int n = 10000000;

			var d = Enumerable.Range(0, n).Select(DigitsSum).ToArray();
			d[89] = 89;
			for (int x = 1; x < n; x++) UpdateValue(x);
			return d.Count(x => x == 89);

			void UpdateValue(int x)
			{
				if (d[x] == 1 || d[x] == 89) return;
				UpdateValue(d[x]);
				d[x] = d[d[x]];
			}

			static int DigitsSum(int x)
			{
				var r = 0;
				foreach (var c in x.ToString()) r += (c - '0') * (c - '0');
				return r;
			}
		}

		public static object P093()
		{
			return 0;
		}

		public static object P094()
		{
			// mod 3 で考えることで、m = √(3 n^2 + 1), 2n + √(3 n^2 + 1)

			const long pMax = 1000000000;

			var sq = Enumerable.Range(1, 100000).ToDictionary(x => (long)x * x, x => (long)x);
			var r = 0L;

			for (long n = 1; n < 100000; n++)
			{
				var m2 = 3 * n * n + 1;
				if (sq.ContainsKey(m2))
				{
					var m = sq[m2];
					Check(m, n);
					m += 2 * n;
					Check(m, n);
				}
			}
			return r;

			void Check(long m, long n)
			{
				var m2 = m * m;
				var n2 = n * n;

				var a = 2 * m * n;
				var b = m2 - n2;
				var c = m2 + n2;

				var p = (ArgHelper.FirstMin(a, b) + c) * 2;
				if (p <= pMax) r += p;
			}
		}

		public static object P095()
		{
			return 0;
		}

		public static object P096()
		{
			return 0;
		}

		public static object P097()
		{
			const long M = 10000000000;
			return (28433 * BigInteger.ModPow(2, 7830457, M) + 1) % M;
		}

		public static object P098()
		{
			return 0;
		}

		public static object P099()
		{
			var t = GetText(Url099).Split('\n')
				.Select(l => Array.ConvertAll(l.Split(','), int.Parse))
				.Select((a, i) => (i, v: a[1] * Math.Log(a[0])))
				.FirstMax(p => p.v);
			return t.i + 1;
		}

		public static object P100()
		{
			// n: total
			// m: blue
			// n * (n-1) = 2 * m * (m-1)
			// x := 2 * n - 1
			// y := 2 * m - 1

			// Pell's equation
			// x^2 - 2 y^2 = -1
			// 初期値: (x0, y0) = (1, 1)
			// 解 (x, y) は 1 + √2 の奇数乗から求められます。

			const long nMin = 1000000000000;

			var (n, m) = ContinuedFractions.Pells_1(2)
				.Select(p => (n: (p.x + 1) / 2, m: (p.y + 1) / 2))
				.First(p => p.n > nMin);
			return m;
		}

		const string Url099 = "https://projecteuler.net/project/resources/p099_base_exp.txt";
	}
}
