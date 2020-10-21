using System;
using System.Linq;

class E2
{
	const long M = 1000000007;
	static void Main()
	{
		var z = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int h = z[0], w = z[1];
		var s = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var k = s.Sum(t => t.Count(c => c == '.'));
		var p2 = new long[k + 1];
		p2[0] = 1;
		for (int i = 0; i < k; ++i) p2[i + 1] = p2[i] * 2 % M;

		var raq = new StaticRAQ2(h, w);
		raq.Add(0, 0, h, w, -1);

		for (int i = 0; i < h; i++)
		{
			var start = -1;
			for (int j = 0; j <= w; j++)
			{
				if (j < w && s[i][j] == '.')
				{
					if (start == -1) start = j;
				}
				else
				{
					if (start != -1) raq.Add(i, start, i + 1, j, j - start);
					start = -1;
				}
			}
		}
		for (int j = 0; j < w; j++)
		{
			var start = -1;
			for (int i = 0; i <= h; i++)
			{
				if (i < h && s[i][j] == '.')
				{
					if (start == -1) start = i;
				}
				else
				{
					if (start != -1) raq.Add(start, j, i, j + 1, i - start);
					start = -1;
				}
			}
		}
		var sum = raq.GetAll();

		var r = 0L;
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
			{
				if (sum[i, j] == -1) continue;
				r = (r + (p2[sum[i, j]] - 1 + M) % M * p2[k - sum[i, j]]) % M;
			}
		Console.WriteLine(r);
	}
}

class StaticRAQ2
{
	int nx, ny;
	int[,] d;
	public StaticRAQ2(int _nx, int _ny) { nx = _nx; ny = _ny; d = new int[nx, ny]; }

	// O(1)
	// 範囲外のインデックスも可。
	public void Add(int x1, int y1, int x2, int y2, int v)
	{
		d[Math.Max(0, x1), Math.Max(0, y1)] += v;
		if (y2 < ny) d[Math.Max(0, x1), y2] -= v;
		if (x2 < nx) d[x2, Math.Max(0, y1)] -= v;
		if (x2 < nx && y2 < ny) d[x2, y2] += v;
	}

	// O(nx ny)
	public int[,] GetAll()
	{
		var a = new int[nx, ny];
		for (int i = 0; i < nx; ++i) a[i, 0] = d[i, 0];
		for (int i = 0; i < nx; ++i)
			for (int j = 1; j < ny; ++j) a[i, j] = a[i, j - 1] + d[i, j];
		for (int j = 0; j < ny; ++j)
			for (int i = 1; i < nx; ++i) a[i, j] += a[i - 1, j];
		return a;
	}
}
