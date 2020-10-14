using System;
using System.Linq;

class A2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var rs = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var xs = rs.Select(r => r[0]).Concat(rs.Select(r => r[2])).Distinct().OrderBy(v => v).ToArray();
		var xd = Enumerable.Range(0, xs.Length).ToDictionary(i => xs[i]);
		var ys = rs.Select(r => r[1]).Concat(rs.Select(r => r[3])).Distinct().OrderBy(v => v).ToArray();
		var yd = Enumerable.Range(0, ys.Length).ToDictionary(i => ys[i]);

		var raq = new StaticRAQ2(xs.Length, ys.Length);
		foreach (var r in rs)
			raq.Add(xd[r[0]], yd[r[1]], xd[r[2]], yd[r[3]], 1);
		var s = raq.GetAll();

		var sum = 0L;
		for (int i = 0; i < xs.Length; i++)
			for (int j = 0; j < ys.Length; j++)
				if (s[i, j] > 0) sum += (long)(xs[i + 1] - xs[i]) * (ys[j + 1] - ys[j]);
		Console.WriteLine(sum);
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
}
