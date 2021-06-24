using System;
using System.Collections.Generic;

class Q072
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		int ToId(int i, int j) => i * w + j;

		var map = Array.ConvertAll(new bool[h * w], _ => new List<int>());
		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (c[i][j] == '#') continue;

				var v = ToId(i, j);
				if (i > 0 && c[i - 1][j] == '.') map[v].Add(ToId(i - 1, j));
				if (j > 0 && c[i][j - 1] == '.') map[v].Add(ToId(i, j - 1));
				if (i < h - 1 && c[i + 1][j] == '.') map[v].Add(ToId(i + 1, j));
				if (j < w - 1 && c[i][j + 1] == '.') map[v].Add(ToId(i, j + 1));
			}
		}

		var r = -1;
		var u = new bool[h * w];

		for (int v = 0; v < h * w; v++)
		{
			Array.Clear(u, 0, u.Length);
			Dfs(v, -1, v, 0);
		}
		return r;

		void Dfs(int v, int pv, int sv, int d)
		{
			if (u[v])
			{
				if (v == sv) r = Math.Max(r, d);
				return;
			}

			u[v] = true;

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				Dfs(nv, v, sv, d + 1);
			}
		}
	}
}
