using System;

class Q028
{
	const int h = 1000, w = 1000;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read4());

		var raq = new StaticRAQ2(h, w);
		foreach (var (lx, ly, rx, ry) in ps)
		{
			raq.Add(lx, ly, rx, ry, 1);
		}
		var all = raq.GetAll0();

		var r = new int[n];
		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (all[i, j] == 0) continue;
				r[all[i, j] - 1]++;
			}
		}
		return string.Join("\n", r);
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
