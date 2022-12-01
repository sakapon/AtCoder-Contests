using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8.Linq;
using EulerLib8.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerTest.Page001
{
	[TestClass]
	public class P031_040
	{
		#region Test Methods
		[TestMethod] public void T031() => TestHelper.Execute();
		[TestMethod] public void T032() => TestHelper.Execute();
		[TestMethod] public void T033() => TestHelper.Execute();
		[TestMethod] public void T034() => TestHelper.Execute();
		[TestMethod] public void T035() => TestHelper.Execute();
		[TestMethod] public void T036() => TestHelper.Execute();
		[TestMethod] public void T037() => TestHelper.Execute();
		[TestMethod] public void T038() => TestHelper.Execute();
		[TestMethod] public void T039() => TestHelper.Execute();
		[TestMethod] public void T040() => TestHelper.Execute();
		#endregion

		public static object P031()
		{
			const int n = 200;
			var coins = new[] { 1, 2, 5, 10, 20, 50, 100, 200 };

			var dp = new long[n + 1];
			dp[0] = 1;

			foreach (var c in coins)
			{
				for (int i = n - 1; i >= 0; i--)
				{
					if (dp[i] == 0) continue;

					for (int d = c; d <= n; d += c)
					{
						if (i + d <= n) dp[i + d] += dp[i];
					}
				}
			}
			return dp[n];
		}

		public static object P032()
		{
			return 0;
		}

		public static object P033()
		{
			return 0;
		}

		public static object P034()
		{
			return 0;
		}

		public static object P035()
		{
			return 0;
		}

		public static object P036()
		{
			return 0;
		}

		public static object P037()
		{
			return 0;
		}

		public static object P038()
		{
			return 0;
		}

		public static object P039()
		{
			const int p_max = 1000;

			var r = new int[p_max + 1];
			foreach (var (a, b, c) in SpecialSeqs.PythagoreanTriples(30))
			{
				var s = a + b + c;
				for (var p = s; p <= p_max; p += s)
				{
					r[p]++;
				}
			}
			return Enumerable.Range(1, p_max).FirstMax(p => r[p]);
		}

		public static object P040()
		{
			return 0;
		}
	}
}
