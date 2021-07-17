using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = Array.ConvertAll(new bool[n], _ => Read());

		var mk = k * k / 2 + 1;

		return Last(0, a.Max(r => r.Max()), m =>
		{
			var raq = new StaticRAQ2(n, n);

			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					if (a[i][j] >= m)
					{
						raq.Add(i, j, i + k, j + k, 1);
					}
				}
			}

			var all = raq.GetAll0();

			for (int i = k - 1; i < n; i++)
			{
				for (int j = k - 1; j < n; j++)
				{
					if (all[i, j] < mk)
					{
						return false;
					}
				}
			}
			return true;
		});
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
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
