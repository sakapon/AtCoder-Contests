using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var rs = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var raq = new StaticRAQ2(1000, 1000);
		foreach (var r in rs)
			raq.Add(r[0], r[1], r[2], r[3], 1);
		var s = raq.GetAll();

		var M = 0L;
		for (int i = 0; i < 1000; i++)
			for (int j = 0; j < 1000; j++)
				M = Math.Max(M, s[i, j]);
		Console.WriteLine(M);
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
		if (x2 < 0 || nx <= x1) return;
		if (y2 < 0 || ny <= y1) return;
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
