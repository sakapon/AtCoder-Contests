using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();
		var q = Read();

		var uf = new UF(n);

		for (int i = 0; i < n; i++)
		{
			uf.Unite(p[i] - 1, q[i] - 1);
		}

		var gs = uf.ToGroups();
		return gs.Select(g => (Comb0(g.Length) + Comb1(g.Length)) % M).Aggregate((x, y) => x * y % M);
	}

	// n 個のグループにおける方法の数 (カード 0 を使わない)
	static long Comb0(int n)
	{
		if (n == 1) return 0;

		// カード i を使わない
		var dp0 = new long[n];
		dp0[0] = 1;
		// カード i を使う
		var dp1 = new long[n];

		for (int i = 1; i < n; i++)
		{
			dp0[i] += dp1[i - 1];
			dp1[i] += dp0[i - 1] + dp1[i - 1];

			dp0[i] %= M;
			dp1[i] %= M;
		}
		return dp0[^2] + dp1[^2];
	}

	// n 個のグループにおける方法の数 (カード 0 を使う)
	static long Comb1(int n)
	{
		// カード i を使わない
		var dp0 = new long[n];
		// カード i を使う
		var dp1 = new long[n];
		dp1[0] = 1;

		for (int i = 1; i < n; i++)
		{
			dp0[i] += dp1[i - 1];
			dp1[i] += dp0[i - 1] + dp1[i - 1];

			dp0[i] %= M;
			dp1[i] %= M;
		}
		return dp0[^1] + dp1[^1];
	}

	const long M = 998244353;
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
