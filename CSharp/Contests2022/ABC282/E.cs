using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, M) = Read2();
		var a = Read();

		var es = new List<int[]>();

		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				var v = BigInteger.ModPow(a[i], a[j], M) + BigInteger.ModPow(a[j], a[i], M);
				v %= M;
				es.Add(new[] { i, j, (int)v });
			}
		}

		var mes = Kruskal(n, es.ToArray());
		return mes.Sum(e => (long)e[2]);
	}

	public static int[][] Kruskal(int n, int[][] ues)
	{
		var uf = new UF(n);
		var mes = new List<int[]>();

		foreach (var e in ues.OrderBy(e => -e[2]))
		{
			if (uf.Unite(e[0], e[1])) mes.Add(e);
		}
		return mes.ToArray();
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
