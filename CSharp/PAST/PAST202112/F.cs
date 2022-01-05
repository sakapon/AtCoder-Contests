using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b) = Read2();
		a--;
		b--;
		var s = Array.ConvertAll(new bool[3], _ => Console.ReadLine());

		var nexts = new List<(int i, int j)>();

		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (s[i][j] == '#')
				{
					nexts.Add((i - 1, j - 1));
				}
			}
		}

		var r = 0;
		var u = new bool[9, 9];

		Dfs(a, b);
		return r;

		void Dfs(int i, int j)
		{
			if (u[i, j]) return;
			r++;
			u[i, j] = true;

			foreach (var (di, dj) in nexts)
			{
				var (ni, nj) = (i + di, j + dj);
				if (0 <= ni && ni < 9 && 0 <= nj && nj < 9) Dfs(ni, nj);
			}
		}
	}
}
