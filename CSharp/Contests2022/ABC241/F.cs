using System;
using System.Collections.Generic;
using System.Linq;
using TreesLab.WBTrees;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var s = Read2();
		var g = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var ps_xy = new Dictionary<int, WBSet<int>>();
		var ps_yx = new Dictionary<int, WBSet<int>>();

		foreach (var (x, y) in ps)
		{
			if (!ps_xy.ContainsKey(x)) ps_xy[x] = new WBSet<int>();
			ps_xy[x].Add(y);

			if (!ps_yx.ContainsKey(y)) ps_yx[y] = new WBSet<int>();
			ps_yx[y].Add(x);
		}

		var id_p = new List<(int, int)>();
		var p_id = new Dictionary<(int, int), int>();
		AddPoint(s);
		AddPoint(g);

		int AddPoint((int x, int y) p)
		{
			if (!p_id.ContainsKey(p))
			{
				p_id[p] = id_p.Count;
				id_p.Add(p);
			}
			return p_id[p];
		}

		var r = Bfs(4 * n, id =>
		{
			var (px, py) = id_p[id];

			var nids = new List<int>();

			if (ps_xy.ContainsKey(px))
			{
				{ if (ps_xy[px].GetLast(v => v < py).TryGetItem(out var y)) nids.Add(AddPoint((px, y + 1))); }
				{ if (ps_xy[px].GetFirst(v => v > py).TryGetItem(out var y)) nids.Add(AddPoint((px, y - 1))); }
			}
			if (ps_yx.ContainsKey(py))
			{
				{ if (ps_yx[py].GetLast(v => v < px).TryGetItem(out var x)) nids.Add(AddPoint((x + 1, py))); }
				{ if (ps_yx[py].GetFirst(v => v > px).TryGetItem(out var x)) nids.Add(AddPoint((x - 1, py))); }
			}

			return nids.ToArray();
		},
		0, 1);

		if (r[1] == long.MaxValue) return -1;
		return r[1];
	}

	public static long[] Bfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			foreach (var nv in nexts(v))
			{
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				if (nv == ev) return costs;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}
