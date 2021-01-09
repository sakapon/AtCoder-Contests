using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var es = Array.ConvertAll(new bool[m], _ => Read());

		es = GetTree(n + 1, es);
		var map = EdgesToMap2(n + 1, es, false);
		var r = new int[n + 1];
		r[1] = 1;

		void Dfs(int v, int[] pe = null)
		{
			foreach (var e in map[v])
			{
				if (e[1] == pe?[0]) continue;

				if (e[2] == r[v])
					r[e[1]] = e[2] % n + 1;
				else
					r[e[1]] = e[2];

				Dfs(e[1], e);
			}
		}

		Dfs(1);
		Console.WriteLine(string.Join("\n", r.Skip(1)));
	}

	static List<int[]>[] EdgesToMap2(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}

	static int[][] GetTree(int n, int[][] ues)
	{
		var uf = new UF(n);
		var res = new List<int[]>();

		foreach (var e in ues)
		{
			if (uf.AreUnited(e[0], e[1])) continue;
			uf.Unite(e[0], e[1]);
			res.Add(e);
		}
		return res.ToArray();
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
}
