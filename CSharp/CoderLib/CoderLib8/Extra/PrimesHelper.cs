using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Extra
{
	static class PrimesHelper
	{
		// n = 20000000 の例:
		// Test: https://codeforces.com/contest/1499/problem/D
		// n 以下のすべての数に対する、素因数の種類の数 O(n)?
		static int[] GetPrimeTypes(int n)
		{
			var c = new int[n + 1];
			for (int p = 2; p <= n; ++p)
				if (c[p] == 0)
					for (int x = p; x <= n; x += p)
						++c[x];
			return c;
		}

		// n 以下のすべての数に対する、素因数の個数 O(n)?
		// 10_000_000 程度までなら速い
		static int[] GetFactorCounts(int n)
		{
			var d = new int[n + 1];
			for (int p = 2; p * p <= n; ++p)
				if (d[p] == 0)
					for (int x = p * p; x <= n; x += p)
						d[x] = p;

			var c = new int[n + 1];
			for (int x = 2; x <= n; ++x)
				c[x] = d[x] == 0 ? 1 : c[x / d[x]] + 1;
			return c;
		}

		// Naive
		// 5_000_000 程度までなら速い
		static int[] GetFactorCounts0(int n)
		{
			var a = new int[n + 1];
			for (int i = 1; i <= n; ++i) a[i] = i;
			var c = new int[n + 1];

			for (int q = 2; q <= n; ++q)
				if (a[q] != 1)
				{
					var p = a[q];
					for (int x = q; x <= n; x += q)
					{
						a[x] /= p;
						++c[x];
					}
				}
			return c;
		}

		// n 以下のすべての数に対する、約数の個数 O(n)?
		// 10_000_000 程度までなら速い
		static int[] GetDivisorCounts(int n)
		{
			var d = new int[n + 1];
			var c = new int[n + 1];
			c[1] = 1;

			for (int p = 2; p <= n; ++p)
				if (d[p] == 0)
				{
					var k = 2;
					for (long q = p; q <= n; k++, q *= p)
					{
						c[q] = k;
						for (var x = q + q; x <= n; x += q)
							d[x] = (int)q;
					}
				}

			for (int x = 2; x <= n; ++x)
				if (c[x] == 0)
					c[x] = c[d[x]] * c[x / d[x]];
			return c;
		}

		// Naive
		// 5_000_000 程度までなら速い
		static int[] GetDivisorCounts0(int n)
		{
			var c = Array.ConvertAll(new bool[n + 1], _ => 1);
			c[0] = 0;
			for (int d = 2; d <= n; ++d)
				for (int x = d; x <= n; x += d)
					++c[x];
			return c;
		}

		// 高度合成数
		// 735134400 の約数は 1344 個
		static (long x, int c)[] GetHighlyComposite()
		{
			var ps = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
			var xm = 1000000000L;
			var r = new List<(long x, int c)>();
			Dfs(0, 1, 1);

			void Dfs(int i, long x0, int c0)
			{
				var c = c0;
				for (long x = x0; x <= xm; x *= ps[i], c += c0)
					if (i + 1 < ps.Length)
						Dfs(i + 1, x, c);
					else
						r.Add((x, c));
			}

			var cm = 0;
			return r.OrderBy(t => t.x).Where(t =>
			{
				if (t.c <= cm) return false;
				cm = t.c;
				return true;
			}).ToArray();
		}
	}
}
