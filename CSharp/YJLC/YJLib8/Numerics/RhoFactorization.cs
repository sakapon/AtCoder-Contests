using System;
using System.Collections.Generic;
using System.Numerics;

// Test: https://algo-method.com/tasks/553
// Test: https://atcoder.jp/contests/abc284/tasks/abc284_d

namespace YJLib8.Numerics
{
	public static class RhoFactorization
	{
		// Miller-Rabin primality test
		static readonly long[] MRBases = new[] { 2L, 325, 9375, 28178, 450775, 9780504, 1795265022 };
		public static bool IsPrime(long n)
		{
			if (n <= 1) return false;
			if (n == 2) return true;
			if ((n & 1) == 0) return false;

			var s = 0;
			var d = n - 1;
			while ((d & 1) == 0) { ++s; d >>= 1; }

			foreach (var a in MRBases)
			{
				if (a % n == 0) return true;
				var x = BigInteger.ModPow(a, d, n);
				if (x == 1) continue;
				var r = 0;
				for (; r < s; ++r, x = x * x % n) if (x == n - 1) break;
				if (r == s) return false;
			}
			return true;
		}

		static long GCD(long a, long b) { if (b == 0) return a; for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }

		// Pollard's rho algorithm
		// n: 奇数かつ合成数
		static long PollardRho(long n)
		{
			long f(BigInteger x) => (long)((x * x + 1) % n);

			// 初期値を変更すると速い場合があります。
			for (long x0 = 100; ; ++x0)
			{
				var d = 1L;
				for (long x = x0, y = f(x); d == 1; x = f(x), y = f(f(y)))
					d = GCD(x > y ? x - y : y - x, n);
				if (d != n) return d;
			}
		}

		public static long[] Factorize(long n)
		{
			var ps = new List<long>();
			while ((n & 1) == 0) { ps.Add(2); n >>= 1; }

			var q = new Stack<long>();
			if (n > 1) q.Push(n);

			while (q.Count > 0)
			{
				var x = q.Pop();
				if (IsPrime(x))
				{
					ps.Add(x);
				}
				else
				{
					var d = PollardRho(x);
					q.Push(d);
					q.Push(x / d);
				}
			}
			var r = ps.ToArray();
			Array.Sort(r);
			return r;
		}
	}
}
