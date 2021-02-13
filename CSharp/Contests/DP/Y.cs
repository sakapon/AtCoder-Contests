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

		long DeltaNcr((int r, int c) p1, (int r, int c) p2) => MNcr(p2.r + p2.c - p1.r - p1.c, p2.r - p1.r);

		var dp = Array.ConvertAll(ps, p => DeltaNcr((1, 1), p));
		for (int i = 0; i <= n; i++)
		{
			var (r, c) = ps[i];
			for (int j = i + 1; j <= n; j++)
			{
				var (nr, nc) = ps[j];
				if (c > nc) continue;
				dp[j] -= dp[i] * DeltaNcr(ps[i], ps[j]);
				dp[j] = MInt(dp[j]);
			}
		}
		Console.WriteLine(dp[n]);
	}

	const long M = 1000000007;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);

	// n >= 0
	static long MFactorial(int n) { for (long x = 1, i = 1; ; x = x * ++i % M) if (i >= n) return x; }
	static long MNpr(int n, int r)
	{
		if (n < r) return 0;
		for (long x = 1, i = n - r; ; x = x * ++i % M) if (i >= n) return x;
	}
	static long MNcr(int n, int r) => n < r ? 0 : n - r < r ? MNcr(n, n - r) : MNpr(n, r) * MInv(MFactorial(r)) % M;
}
