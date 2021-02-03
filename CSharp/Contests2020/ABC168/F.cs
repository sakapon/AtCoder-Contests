using System;
using System.Collections.Generic;
using Bang.Graphs.Grid.Spp;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ls_x = Array.ConvertAll(new bool[n], _ => Read3L());
		var ls_y = Array.ConvertAll(new bool[m], _ => Read3L());

		var xs = new List<long> { 0, -1 << 30, 1 << 30 };
		var ys = new List<long> { 0, -1 << 30, 1 << 30 };
		foreach (var (a, b, c) in ls_x)
		{
			xs.Add(a);
			xs.Add(b);
			ys.Add(c);
		}
		foreach (var (d, e, f) in ls_y)
		{
			xs.Add(d);
			ys.Add(e);
			ys.Add(f);
		}
		var xmap = new CompressionHashMap(xs.ToArray());
		var ymap = new CompressionHashMap(ys.ToArray());

		var map = GridMap.Create(2 * xmap.Count - 1, 2 * ymap.Count - 1, 0L);

		// outer
		for (int i = 0; i < map.Height; i++)
		{
			map[i, 0] = -1;
			map[i, map.Width - 1] = -1;
		}
		for (int j = 0; j < map.Width; j++)
		{
			map[0, j] = -1;
			map[map.Height - 1, j] = -1;
		}

		// lines
		foreach (var (a, b, c) in ls_x)
		{
			var (ai, bi, cj) = (xmap[a] << 1, xmap[b] << 1, ymap[c] << 1);
			for (int i = ai; i <= bi; i++)
				map[i, cj] = -1;
		}
		foreach (var (d, e, f) in ls_y)
		{
			var (di, ej, fj) = (xmap[d] << 1, ymap[e] << 1, ymap[f] << 1);
			for (int j = ej; j <= fj; j++)
				map[di, j] = -1;
		}

		// regions
		for (int i = 1; i < map.Height; i += 2)
		{
			var th = xmap.ReverseMap[(i >> 1) + 1] - xmap.ReverseMap[i >> 1];
			for (int j = 1; j < map.Width; j += 2)
				map[i, j] = th * (ymap.ReverseMap[(j >> 1) + 1] - ymap.ReverseMap[j >> 1]);
		}

		Point sv = (xmap[0] << 1, ymap[0] << 1);
		Point ev = (1, 1);
		var r = Dfs2(map.Height, map.Width,
			(v, action) =>
			{
				Point nv;
				if (map[nv = new Point(v.i - 1, v.j)] != -1) action(nv);
				if (map[nv = new Point(v.i + 1, v.j)] != -1) action(nv);
				if (map[nv = new Point(v.i, v.j - 1)] != -1) action(nv);
				if (map[nv = new Point(v.i, v.j + 1)] != -1) action(nv);
			},
			sv, ev);

		var area = 0L;
		if (r[ev]) return "INF";
		for (int i = 1; i < map.Height; i += 2)
			for (int j = 1; j < map.Width; j += 2)
			{
				if (!r[i, j]) continue;
				area += map[i, j];
			}
		return area;
	}

	public static GridMap<bool> Dfs(int height, int width, Func<Point, Point[]> getNextVertexes, Point startVertex, Point endVertex)
	{
		var u = GridMap.Create(height, width, false);
		var q = new Stack<Point>();
		u[startVertex] = true;
		q.Push(startVertex);

		while (q.Count > 0)
		{
			var v = q.Pop();

			foreach (var nv in getNextVertexes(v))
			{
				if (u[nv]) continue;
				u[nv] = true;
				if (nv == endVertex) return u;
				q.Push(nv);
			}
		}
		return u;
	}

	public static GridMap<bool> Dfs2(int height, int width, Action<Point, Action<Point>> nextAction, Point startVertex, Point endVertex)
	{
		var u = GridMap.Create(height, width, false);
		var q = new Stack<Point>();
		u[startVertex] = true;
		q.Push(startVertex);

		while (q.Count > 0)
		{
			var v = q.Pop();

			var end = false;
			nextAction(v, nv =>
			{
				if (u[nv]) return;
				u[nv] = true;
				if (nv == endVertex) end = true;
				q.Push(nv);
			});
			if (end) return u;
		}
		return u;
	}
}

class CompressionHashMap
{
	public long[] Raw { get; }
	public long[] ReverseMap { get; }
	public Dictionary<long, int> Map { get; }
	public int this[long v] => Map[v];
	public int Count => ReverseMap.Length;

	int[] c;
	public int[] Compressed => c ??= Array.ConvertAll(Raw, v => Map[v]);

	public CompressionHashMap(long[] a)
	{
		// r = a.Distinct().OrderBy(v => v).ToArray();
		var hs = new HashSet<long>();
		foreach (var v in a) hs.Add(v);
		var r = new long[hs.Count];
		hs.CopyTo(r);
		Array.Sort(r);
		var map = new Dictionary<long, int>();
		for (int i = 0; i < r.Length; ++i) map[r[i]] = i;

		(Raw, ReverseMap, Map) = (a, r, map);
	}
}
