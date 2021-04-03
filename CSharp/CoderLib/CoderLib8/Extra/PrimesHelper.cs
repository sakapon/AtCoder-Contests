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
	}
}
