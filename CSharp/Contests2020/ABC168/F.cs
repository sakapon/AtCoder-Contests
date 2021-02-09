﻿using System;
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
		var uf = Unite(map.Height, map.Width, p => map[p] != -1);
		if (uf.AreUnited(sv, ev)) return "INF";

		var area = 0L;
		for (int i = 1; i < map.Height; i += 2)
			for (int j = 1; j < map.Width; j += 2)
			{
				if (!uf.AreUnited(sv, new Point(i, j))) continue;
				area += map[i, j];
			}
		return area;
	}

	public static GridUF Unite(int height, int width, Func<Point, bool> isRoad)
	{
		var uf = new GridUF(height, width);
		for (int j = 1; j < width; ++j)
		{
			var p = new Point(0, j);
			var p_ = new Point(0, j - 1);
			if (isRoad(p) && isRoad(p_)) uf.Unite(p, p_);
		}
		for (int i = 1; i < height; ++i)
		{
			var p = new Point(i, 0);
			var p_ = new Point(i - 1, 0);
			if (isRoad(p) && isRoad(p_)) uf.Unite(p, p_);
		}

		Point np;
		for (int i = 1; i < height; ++i)
			for (int j = 1; j < width; ++j)
			{
				var p = new Point(i, j);
				if (!isRoad(p)) continue;
				if (isRoad(np = new Point(i, j - 1))) uf.Unite(p, np);
				if (isRoad(np = new Point(i - 1, j))) uf.Unite(p, np);
			}
		return uf;
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

class GridUF
{
	GridMap<Point> p;
	GridMap<int> sizes;
	public int GroupsCount;
	public GridUF(int h, int w)
	{
		p = GridMap.Create(h, w, new Point());
		for (int i = 0; i < h; ++i)
			for (int j = 0; j < w; ++j)
				p[i, j] = new Point(i, j);
		sizes = GridMap.Create(h, w, 1);
		GroupsCount = h * w;
	}

	public Point GetRoot(Point x) => p[x] == x ? x : p[x] = GetRoot(p[x]);
	public int GetSize(Point x) => sizes[GetRoot(x)];

	public bool AreUnited(Point x, Point y) => GetRoot(x) == GetRoot(y);
	public bool Unite(Point x, Point y)
	{
		if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

		// 要素数が大きいほうのグループにマージします。
		if (sizes[x] < sizes[y]) Merge(y, x);
		else Merge(x, y);
		return true;
	}
	protected virtual void Merge(Point x, Point y)
	{
		p[y] = x;
		sizes[x] += sizes[y];
		--GroupsCount;
	}
}
