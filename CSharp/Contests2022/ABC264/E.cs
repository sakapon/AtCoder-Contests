using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, ec) = Read3();
		var es = Array.ConvertAll(new bool[ec], _ => Read());
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

		// 発電所: 0
		var uf = new UF(n + 1);
		foreach (var ei in Enumerable.Range(1, ec).Except(qs))
		{
			var e = es[ei - 1];
			if (e[0] > n) e[0] = 0;
			if (e[1] > n) e[1] = 0;
			uf.Unite(e[0], e[1]);
		}

		var r = new int[qc];
		for (int qi = qc - 1; qi >= 0; qi--)
		{
			r[qi] = uf.GetSize(0) - 1;

			var e = es[qs[qi] - 1];
			if (e[0] > n) e[0] = 0;
			if (e[1] > n) e[1] = 0;
			uf.Unite(e[0], e[1]);
		}
		return string.Join("\n", r);
	}
}

public class UF
{
	int[] p, sizes;
	public int GroupsCount { get; private set; }

	public UF(int n)
	{
		p = Array.ConvertAll(new bool[n], _ => -1);
		sizes = Array.ConvertAll(p, _ => 1);
		GroupsCount = n;
	}

	public int GetRoot(int x) => p[x] == -1 ? x : p[x] = GetRoot(p[x]);
	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public int GetSize(int x) => sizes[GetRoot(x)];

	public bool Unite(int x, int y)
	{
		if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

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
	public ILookup<int, int> ToGroups() => Enumerable.Range(0, p.Length).ToLookup(GetRoot);
}
