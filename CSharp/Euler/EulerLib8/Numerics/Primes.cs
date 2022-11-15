using System;
using System.Collections.Generic;

namespace EulerLib8.Numerics
{
	public static class Primes
	{
		public static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
		public static int Lcm(int a, int b) => a / Gcd(a, b) * b;
		// モノイドとする場合 (単位元は 0)
		//static int Gcd(int a, int b) { if (b == 0) return a; for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }

		public static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
		public static long Lcm(long a, long b) => a / Gcd(a, b) * b;

		// 素因数分解 O(√n)
		// n = 1 の場合は空の配列。
		// √n を超える素因数はたかだか 1 個であり、その次数は 1。
		// 候補 x を 2 または奇数に限定することで高速化できます。
		public static long[] Factorize(long n)
		{
			var r = new List<long>();
			for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
			if (n > 1) r.Add(n);
			return r.ToArray();
		}
		// To Dictionary:
		// Factorize(n).GroupBy(p => p).ToDictionary(g => g.Key, g => g.Count())

		// n 以下の素数 O(n)?
		// b の値は、合成数のとき true です。
		// 候補 x を奇数に限定することで高速化できます。
		public static int[] GetPrimes(int n)
		{
			var b = new bool[n + 1];
			for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
			var r = new List<int>();
			for (int x = 2; x <= n; ++x) if (!b[x]) r.Add(x);
			return r.ToArray();
		}
		public static bool[] GetIsPrimes(int n)
		{
			var b = new bool[n + 1];
			Array.Fill(b, true);
			b[0] = b[1] = false;
			for (int p = 2; p * p <= n; ++p) if (b[p]) for (int x = p * p; x <= n; x += p) b[x] = false;
			return b;
		}
	}
}
