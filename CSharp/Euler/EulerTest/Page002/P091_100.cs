using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8.Linq;
using static EulerLib8.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
			return 0;
		}

		public static object P092()
		{
			return 0;
		}

		public static object P093()
		{
			return 0;
		}

		public static object P094()
		{
			return 0;
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
			return 0;
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

			const long n_min = 1000000000000;
			var (x, y) = (1L, 1L);
			var (n, m) = (1L, 1L);

			while (n < n_min)
			{
				(x, y) = (3 * x + 4 * y, 2 * x + 3 * y);
				(n, m) = ((x + 1) / 2, (y + 1) / 2);
			}
			return m;
		}

		const string Url099 = "https://projecteuler.net/project/resources/p099_base_exp.txt";
	}
}
