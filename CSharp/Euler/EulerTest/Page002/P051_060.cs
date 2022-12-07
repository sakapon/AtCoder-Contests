﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EulerLib8.Linq;
using EulerLib8.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerTest.Page002
{
	[TestClass]
	public class P051_060
	{
		#region Test Methods
		[TestMethod] public void T051() => TestHelper.Execute();
		[TestMethod] public void T052() => TestHelper.Execute();
		[TestMethod] public void T053() => TestHelper.Execute();
		[TestMethod] public void T054() => TestHelper.Execute();
		[TestMethod] public void T055() => TestHelper.Execute();
		[TestMethod] public void T056() => TestHelper.Execute();
		[TestMethod] public void T057() => TestHelper.Execute();
		[TestMethod] public void T058() => TestHelper.Execute();
		[TestMethod] public void T059() => TestHelper.Execute();
		[TestMethod] public void T060() => TestHelper.Execute();
		#endregion

		public static object P051()
		{
			const int digits = 6;
			const int count = 8;
			var format = $"D{digits}";
			var n = (int)Math.Pow(10, digits);

			var b = Primes.GetIsPrimes(n);

			for (int x = 1; x < n; x += 2)
			{
				var s = x.ToString(format);
				if (!s.Contains('0')) continue;
				s = string.Join("", s.Select(c => c == '0' ? '1' : '0'));
				var d = int.Parse(s);

				var c = 0;
				for (int i = s[0] == '0' ? 0 : 1; i < 10; i++)
				{
					if (b[x + d * i])
					{
						c++;
					}
				}
				if (c >= count) return s[0] == '0' ? x : x + d;
			}
			return -1;
		}

		public static object P052()
		{
			return 142857;
		}

		public static object P053()
		{
			const int nMax = 100;

			var c = 0;
			var comb = new BigCombination(nMax);
			for (int n = 1; n <= nMax; n++)
			{
				for (int r = 0; r <= n; r++)
				{
					if (comb.Ncr(n, r) > 1000000) c++;
				}
			}
			return c;
		}

		public static object P054()
		{
			return 0;
		}

		public static object P055()
		{
			return 0;
		}

		public static object P056()
		{
			var r = 0L;
			for (int a = 1; a < 100; a++)
			{
				for (int b = 1; b < 100; b++)
				{
					var v = BigInteger.Pow(a, b);
					ArgHelper.ChFirstMax(ref r, v.ToString().Sum(c => c - '0'));
				}
			}
			return r;
		}

		public static object P057()
		{
			return ConvergentsSqrt2().Take(1000).Count(p => p.num.ToString().Length > p.denom.ToString().Length);

			static IEnumerable<(BigInteger num, BigInteger denom)> ConvergentsSqrt2()
			{
				BigInteger u = 2, v = 1;

				while (true)
				{
					yield return (u + v, u);
					(u, v) = (v + u * 2, u);
				}
			}
		}

		public static object P058()
		{
			return 0;
		}

		public static object P059()
		{
			return 0;
		}

		public static object P060()
		{
			return 0;
		}
	}
}
