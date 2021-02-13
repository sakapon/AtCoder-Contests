using System;
using System.Linq;

class Y
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int r, int c) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (h, w, n) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2()).OrderBy(p => p.r).ThenBy(p => p.c).Append((r: h, c: w)).ToArray();

		var mc = new MCombination(h + w);
		long DeltaNcr((int r, int c) p1, (int r, int c) p2) => mc.MNcr(p2.r + p2.c - p1.r - p1.c, p2.r - p1.r);

		var dp = Array.ConvertAll(ps, p => DeltaNcr((1, 1), p));
		for (int i = 0; i <= n; i++)
			for (int j = i + 1; j <= n; j++)
			{
				if (ps[i].c > ps[j].c) continue;
				dp[j] -= dp[i] * DeltaNcr(ps[i], ps[j]);
				dp[j] = MInt(dp[j]);
			}
		Console.WriteLine(dp[n]);
	}

	const long M = 1000000007;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}

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
	public long MNpr(int n, int r) => f[n] * f_[n - r] % M;
	public long MNcr(int n, int r) => f[n] * f_[n - r] % M * f_[r] % M;
}
