using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var u = new bool[2 * n, 2 * n];
		foreach (var (a, b) in es)
		{
			u[a - 1, b - 1] = true;
			u[b - 1, a - 1] = true;
		}

		var mc = new MCombination(n);

		var dp2 = new MemoDP2<long>(2 * n + 1, 2 * n + 1, -1, (dp, l, r) =>
		{
			var v = 0L;

			for (int c = l + 2; c <= r; c += 2)
			{
				if (u[l, c - 1])
				{
					v += dp[l + 1, c - 1] * dp[c, r] % M * mc.MNcr((r - l) / 2, (r - c) / 2);
					v %= M;
				}
			}
			return v;
		});

		for (int i = 0; i <= 2 * n; i++)
		{
			dp2[i, i] = 1;
		}
		return dp2[0, 2 * n];
	}

	const long M = 998244353;
}

class MemoDP2<T>
{
	static readonly Func<T, T, bool> TEquals = EqualityComparer<T>.Default.Equals;
	public T[,] Raw { get; }
	T iv;
	Func<MemoDP2<T>, int, int, T> rec;

	public MemoDP2(int n1, int n2, T iv, Func<MemoDP2<T>, int, int, T> rec)
	{
		Raw = new T[n1, n2];
		for (int i = 0; i < n1; ++i)
			for (int j = 0; j < n2; ++j)
				Raw[i, j] = iv;
		this.iv = iv;
		this.rec = rec;
	}

	public T this[int i, int j]
	{
		get => TEquals(Raw[i, j], iv) ? Raw[i, j] = rec(this, i, j) : Raw[i, j];
		set => Raw[i, j] = value;
	}
}

public class MCombination
{
	const long M = 998244353;
	//const long M = 1000000007;
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
