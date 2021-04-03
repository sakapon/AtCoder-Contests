using System;

namespace CoderLib8.Extra
{
	static class PrimesHelper
	{
		// n = 20000000 の例:
		// Test: https://codeforces.com/contest/1499/problem/D
		// n 以下のすべての数に対する、素因数の種類の数 O(n)?
		static int[] GetPrimeTypes(int n)
		{
			var t = new int[n + 1];
			for (int p = 2; p <= n; ++p) if (t[p] == 0) for (int x = p; x <= n; x += p) ++t[x];
			return t;
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
	}
}
