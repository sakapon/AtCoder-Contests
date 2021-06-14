using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();

		if (n - m > k) return 0;

		// 余事象
		var r = 0L;
		var mc = new MCombination(n + m);

		for (int i = 0; i < n - k; i++)
		{
			var all1 = k + 2 * i - 1;
			if (all1 < i) all1 = i;
			r += mc.MNcr(all1, i) * mc.MNcr(n + m - k - 2 * i - 1, m - i);
			r %= M;
		}
		return MInt(mc.MNcr(n + m, n) - r);
	}

	const long M = 1000000007;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}

public class MCombination
{
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
}
