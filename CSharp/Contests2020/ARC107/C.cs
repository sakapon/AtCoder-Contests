using System;
using System.Linq;

class C
{
	const long M = 998244353;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, k) = Read2();
		var a = Array.ConvertAll(new bool[n], _ => Read());

		var r = 1L;
		var rn = Enumerable.Range(0, n).ToArray();
		var comb = new MCombination(n);

		var ufi = new UF(n);
		for (int i1 = 0; i1 < n; i1++)
			for (int i2 = i1 + 1; i2 < n; i2++)
				if (rn.All(j => a[i1][j] + a[i2][j] <= k))
					ufi.Unite(i1, i2);

		var ufj = new UF(n);
		for (int j1 = 0; j1 < n; j1++)
			for (int j2 = j1 + 1; j2 < n; j2++)
				if (rn.All(i => a[i][j1] + a[i][j2] <= k))
					ufj.Unite(j1, j2);

		foreach (var g in ufi.ToGroups())
		{
			r *= comb.MFactorial(g.Length);
			r %= M;
		}
		foreach (var g in ufj.ToGroups())
		{
			r *= comb.MFactorial(g.Length);
			r %= M;
		}
		Console.WriteLine(r);
	}
}

class UF
{
	int[] p, sizes;
	public int GroupsCount;
	public UF(int n)
	{
		p = Enumerable.Range(0, n).ToArray();
		sizes = Array.ConvertAll(p, _ => 1);
		GroupsCount = n;
	}

	public int GetRoot(int x) => p[x] == x ? x : p[x] = GetRoot(p[x]);
	public int GetSize(int x) => sizes[GetRoot(x)];

	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public bool Unite(int x, int y)
	{
		if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

		// 要素数が大きいほうのグループにマージします。
		if (sizes[x] < sizes[y]) Merge(y, x);
		else Merge(x, y);
		return true;
	}
	protected virtual void Merge(int x, int y)
	{
		p[y] = x;
		sizes[x] += sizes[y];
		--GroupsCount;
	}
	public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
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
	public long MNpr(int n, int r) => f[n] * f_[n - r] % M;
	public long MNcr(int n, int r) => f[n] * f_[n - r] % M * f_[r] % M;
}
