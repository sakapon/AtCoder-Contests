using System;
using System.Collections.Generic;
using Bang.Graphs.Grid.Spp;

class F2
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

		// 柵に使われる座標
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

		var lineMap_x = GridMap.Create(xmap.Count - 1, ymap.Count, false);
		var lineMap_y = GridMap.Create(xmap.Count, ymap.Count - 1, false);
		foreach (var (a, b, c) in ls_x)
		{
			var j = ymap[c];
			for (int i = xmap[a]; i < xmap[b]; i++)
				lineMap_x[i, j] = true;
		}
		foreach (var (d, e, f) in ls_y)
		{
			var i = xmap[d];
			for (int j = ymap[e]; j < ymap[f]; j++)
				lineMap_y[i, j] = true;
		}

		var (h, w) = (xmap.Count - 1, ymap.Count - 1);
		var uf = new GridUF(h, w);
		for (int i = 0; i < h; i++)
			for (int j = 1; j < w; j++)
				if (!lineMap_x[i, j])
					uf.Unite(new Point(i, j - 1), new Point(i, j));
		for (int j = 0; j < w; j++)
			for (int i = 1; i < h; i++)
				if (!lineMap_y[i, j])
					uf.Unite(new Point(i - 1, j), new Point(i, j));

		Point sv = (xmap[0], ymap[0]);
		Point ev = (0, 0);
		if (uf.AreUnited(sv, ev)) return "INF";

		var area = 0L;
		for (int i = 0; i < h; i++)
		{
			var th = xmap.ReverseMap[i + 1] - xmap.ReverseMap[i];
			for (int j = 0; j < w; j++)
				if (uf.AreUnited(sv, new Point(i, j)))
					area += th * (ymap.ReverseMap[j + 1] - ymap.ReverseMap[j]);
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
