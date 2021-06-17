using System;

namespace CoderLib6.Values
{
	// Test: https://atcoder.jp/contests/dp/tasks/dp_y
	public class MCombination
	{
		//const long M = 998244353;
		const long M = 1000000007;
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
		static long MInv(long x) => MPow(x, M - 2);

		static long[] MFactorials(int n)
		{
			var f = new long[n + 1];
			f[0] = 1;
			for (int i = 1; i <= n; ++i) f[i] = f[i - 1] * i % M;
			return f;
		}

		// nPr, nCr を O(1) で求めるため、階乗を O(n) で求めておきます。
		long[] f, f_;
		public MCombination(int nMax)
		{
			f = MFactorials(nMax);
			f_ = Array.ConvertAll(f, MInv);
		}

		public long MFactorial(int n) => f[n];
		public long MInvFactorial(int n) => f_[n];
		public long MNpr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M;
		public long MNcr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M * f_[r] % M;

		// nMax >= 2n としておく必要があります。
		public long MCatalan(int n) => f[2 * n] * f_[n] % M * f_[n + 1] % M;
	}
}
