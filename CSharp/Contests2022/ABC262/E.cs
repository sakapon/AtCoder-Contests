using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var degs = new int[n];
		foreach (var e in es)
		{
			++degs[e[0] - 1];
			++degs[e[1] - 1];
		}

		var c0 = degs.Count(x => (x & 1) == 0);
		var c1 = n - c0;
		var comb = new MCombination(n);

		var sum = 0L;

		for (int r1 = 0; r1 <= k && r1 <= c1; r1 += 2)
		{
			var r0 = k - r1;
			if (r0 > c0) continue;
			sum += comb.MNcr(c0, r0) * comb.MNcr(c1, r1);
			sum %= M;
		}

		return sum;
	}

	const long M = 998244353;
}

public class MCombination
{
	const long M = 998244353;
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
