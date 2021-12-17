using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var u = new int[n + 1];
		var uf = new UF(n + 1);

		foreach (var (a, b) in es)
		{
			if (++u[a] > 2) return false;
			if (++u[b] > 2) return false;
			if (!uf.Unite(a, b)) return false;
		}
		return true;
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
