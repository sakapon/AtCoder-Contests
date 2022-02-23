using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var d = Read();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		if (d.Sum() != 2 * (n - 1)) return -1;

		var map = ToMap(n + 1, es, false);

		for (int i = 0; i < n; i++)
		{
			d[i] -= map[i + 1].Length;
			if (d[i] < 0) return -1;
		}

		var uf = new UF(n);
		foreach (var e in es)
		{
			var a = e[0] - 1;
			var b = e[1] - 1;
			if (!uf.Unite(a, b)) return -1;
		}

		var gs = uf.ToGroups();
		var gvs = gs
			.Select(g => g.SelectMany(v => Enumerable.Repeat(v, d[v])).ToArray())
			.OrderBy(g => g.Length)
			.ToArray();
		if (gvs[0].Length == 0) return -1;

		var r = new List<string>();
		var q = new Queue<int>(gvs[^1]);

		for (int gi = gs.Length - 2; gi >= 0; gi--)
		{
			var vs = gvs[gi];
			if (q.Count == 0) return -1;
			r.Add($"{q.Dequeue() + 1} {vs[0] + 1}");

			foreach (var nv in vs[1..])
			{
				q.Enqueue(nv);
			}
		}

		return string.Join("\n", r);
	}

	public static int[][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
	public static List<int>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
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
