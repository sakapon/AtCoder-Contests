using System;
using System.Collections.Generic;
using System.Linq;
using TreesLab.WBTrees;

class F
{
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var s = Read2();
		var g = Read2();
		var stops = Array.ConvertAll(new bool[n], _ => Read2()).ToHashSet();

		var stops_xy = new Dictionary<int, WBSet<int>>();
		var stops_yx = new Dictionary<int, WBSet<int>>();
		var ps_xy = new Dictionary<int, WBSet<int>>();
		var ps_yx = new Dictionary<int, WBSet<int>>();

		if (!ps_xy.ContainsKey(s.x)) ps_xy[s.x] = new WBSet<int>();
		ps_xy[s.x].Add(s.y);
		if (!ps_yx.ContainsKey(s.y)) ps_yx[s.y] = new WBSet<int>();
		ps_yx[s.y].Add(s.x);

		foreach (var (x, y) in stops)
		{
			if (!stops_xy.ContainsKey(x)) stops_xy[x] = new WBSet<int>();
			stops_xy[x].Add(y);

			if (!stops_yx.ContainsKey(y)) stops_yx[y] = new WBSet<int>();
			stops_yx[y].Add(x);

			if (!ps_xy.ContainsKey(x - 1)) ps_xy[x - 1] = new WBSet<int>();
			ps_xy[x - 1].Add(y);

			if (!ps_xy.ContainsKey(x + 1)) ps_xy[x + 1] = new WBSet<int>();
			ps_xy[x + 1].Add(y);

			if (!ps_yx.ContainsKey(y - 1)) ps_yx[y - 1] = new WBSet<int>();
			ps_yx[y - 1].Add(x);

			if (!ps_yx.ContainsKey(y + 1)) ps_yx[y + 1] = new WBSet<int>();
			ps_yx[y + 1].Add(x);
		}

		var id_p = new List<(int, int)>();
		var p_id = new Dictionary<(int, int), int>();
		id_p.Add(s);
		id_p.Add(g);
		p_id[s] = 0;
		p_id[g] = 1;

		var r = Bfs(4 * n, id =>
		{
			var (px, py) = id_p[id];

			var nids = new List<int>();

			if (stops.Contains((px - 1, py)))
			{
				var stopx = stops_yx[py].GetFirst(v => v > px).GetItemOrDefault(max);
				if (ps_yx.ContainsKey(py))
					foreach (var x in ps_yx[py].GetItems(v => v > px, v => v < stopx))
						AddId(x, py);
			}
			if (stops.Contains((px + 1, py)))
			{
				var stopx = stops_yx[py].GetLast(v => v < px).GetItemOrDefault(0);
				if (ps_yx.ContainsKey(py))
					foreach (var x in ps_yx[py].GetItems(v => v > stopx, v => v < px))
						AddId(x, py);
			}
			if (stops.Contains((px, py - 1)))
			{
				var stopy = stops_xy[px].GetFirst(v => v > py).GetItemOrDefault(max);
				if (ps_xy.ContainsKey(px))
					foreach (var y in ps_xy[px].GetItems(v => v > py, v => v < stopy))
						AddId(px, y);
			}
			if (stops.Contains((px, py + 1)))
			{
				var stopy = stops_xy[px].GetLast(v => v < py).GetItemOrDefault(0);
				if (ps_xy.ContainsKey(px))
					foreach (var y in ps_xy[px].GetItems(v => v > stopy, v => v < py))
						AddId(px, y);
			}

			return nids.ToArray();

			void AddId(int x, int y)
			{
				if (p_id.ContainsKey((x, y)))
				{
					nids.Add(p_id[(x, y)]);
				}
				else
				{
					var nid = id_p.Count;
					id_p.Add((x, y));
					p_id[(x, y)] = nid;
					nids.Add(nid);
				}
			}
		},
		1, 0);

		if (r[0] == long.MaxValue) return -1;
		return r[0];
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
