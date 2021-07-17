using System;

namespace CoderLib6.Values
{
	// M が素数のときに限定されるメソッド: MInv, MNcr
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/7/DPL/5
	// Test: https://atcoder.jp/contests/atc002/tasks/atc002_b
	static class ModuloHelper
	{
		//const long M = 998244353;
		const long M = 1000000007;
		const long MHalf = (M + 1) / 2;
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
		static long MInv(long x) => MPow(x, M - 2);
		// x と M が互いに素の場合 (Euler の定理)
		//static long MInv(long x) => MPow(x, Totient(M) - 1);

		// n >= 0
		static long MFactorial(int n) { for (long x = 1, i = 1; ; x = x * ++i % M) if (i >= n) return x; }
		static long MNpr(int n, int r)
		{
			if (n < r) return 0;
			for (long x = 1, i = n - r; ; x = x * ++i % M) if (i >= n) return x;
		}
		static long MNcr(int n, int r) => n < r ? 0 : n - r < r ? MNcr(n, n - r) : MNpr(n, r) * MInv(MFactorial(r)) % M;
		static long MCatalan(int n) => MNpr(2 * n, n) * MInv(MFactorial(n + 1)) % M;

		static long MInt(long x) => (x %= M) < 0 ? x + M : x;
		static long MNeg(long x) => MInt(-x);
		static long MAdd(long x, long y) => MInt(x + y);
		static long MSub(long x, long y) => MInt(x - y);
		static long MMul(long x, long y) => MInt(x * y);
		static long MDiv(long x, long y) => MInt(x * MInv(y));

		static long[] MPows(long b, int n)
		{
			var p = new long[n + 1];
			p[0] = 1;
			for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b % M;
			return p;
		}

		static MInt[] MFactorials(int n)
		{
			var f = new MInt[n + 1];
			f[0] = 1;
			for (int i = 0; i < n; ++i) f[i + 1] = f[i] * (i + 1);
			return f;
		}

		static MInt[] MNprs(int n)
		{
			var p = new MInt[n + 1];
			p[0] = 1;
			for (int i = 0; i < n; ++i) p[i + 1] = p[i] * (n - i);
			return p;
		}

		static MInt[] MNcrs(int n)
		{
			var c = new MInt[n + 1];
			c[0] = 1;
			for (int i = 0; i < n; ++i) c[i + 1] = c[i] * (n - i) / (i + 1);
			return c;
		}
	}
}
