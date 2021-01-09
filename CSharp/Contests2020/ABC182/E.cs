using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var z = Read();
		int h = z[0], w = z[1], n = z[2], m = z[3];
		var abs = Array.ConvertAll(new bool[n], _ => Read().Select(v => v - 1).ToArray());
		var cds = Array.ConvertAll(new bool[m], _ => Read().Select(v => v - 1).ToArray());

		var a = new bool[h, w];
		foreach (var p in cds)
		{
			a[p[0], p[1]] = true;
		}

		var rh = Enumerable.Range(0, h).ToArray();
		var rw = Enumerable.Range(0, w).ToArray();

		var bRow = rh.Select(i => rw.Where(j => a[i, j]).ToArray()).ToArray();
		var bCol = rw.Select(j => rh.Where(i => a[i, j]).ToArray()).ToArray();

		var raq = new StaticRAQ2(h, w);

		foreach (var p in abs)
		{
			var r = bRow[p[0]];
			var c = bCol[p[1]];

			var j = First(0, r.Length, x => r[x] > p[1]);
			raq.Add(p[0], j == 0 ? 0 : r[j - 1] + 1, p[0] + 1, j == r.Length ? w : r[j], 1);

			var i = First(0, c.Length, x => c[x] > p[0]);
			raq.Add(i == 0 ? 0 : c[i - 1] + 1, p[1], i == c.Length ? h : c[i], p[1] + 1, 1);
		}

		var d = raq.GetAll0();
		var sum = 0;

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				if (d[i, j] > 0) sum++;
		Console.WriteLine(sum);
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}

class StaticRAQ2
{
	int nx, ny;
	long[,] d;
	public StaticRAQ2(int _nx, int _ny) { nx = _nx; ny = _ny; d = new long[nx, ny]; }

	// O(1)
	// 範囲外のインデックスも可。
	public void Add(int x1, int y1, int x2, int y2, long v)
	{
		d[Math.Max(0, x1), Math.Max(0, y1)] += v;
		if (y2 < ny) d[Math.Max(0, x1), y2] -= v;
		if (x2 < nx) d[x2, Math.Max(0, y1)] -= v;
		if (x2 < nx && y2 < ny) d[x2, y2] += v;
	}

	// O(nx ny)
	public long[,] GetAll()
	{
		var a = new long[nx, ny];
		for (int i = 0; i < nx; ++i) a[i, 0] = d[i, 0];
		for (int i = 0; i < nx; ++i)
			for (int j = 1; j < ny; ++j) a[i, j] = a[i, j - 1] + d[i, j];
		for (int j = 0; j < ny; ++j)
			for (int i = 1; i < nx; ++i) a[i, j] += a[i - 1, j];
		return a;
	}

	// O(nx ny)
	// d をそのまま使います。
	public long[,] GetAll0()
	{
		for (int i = 0; i < nx; ++i)
			for (int j = 1; j < ny; ++j) d[i, j] += d[i, j - 1];
		for (int j = 0; j < ny; ++j)
			for (int i = 1; i < nx; ++i) d[i, j] += d[i - 1, j];
		return d;
	}
}
