using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Spp;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var s = GraphConsole.ReadGrid(h);

		var k = s.Sum(l => l.Count(x => x == '#'));

		var map = new GridListMap<Point>(h, w);
		for (int i = 0; i < h; i++)
			for (int j = 1; j < w; j++)
			{
				Point v = (i, j);
				Point nv = (i, j - 1);
				if (s.GetValue(v) == '#' && s.GetValue(nv) == '#')
				{
					map.Add(v, nv);
					map.Add(nv, v);
				}
			}
		for (int j = 0; j < w; j++)
			for (int i = 1; i < h; i++)
			{
				Point v = (i, j);
				Point nv = (i - 1, j);
				if (s.GetValue(v) == '#' && s.GetValue(nv) == '#')
				{
					map.Add(v, nv);
					map.Add(nv, v);
				}
			}

		var q = new Stack<Point>();
		var u = new GridMap<bool>(h, w, false);

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == '.') continue;
				q.Clear();
				u = new GridMap<bool>(h, w, false);
				Dfs((i, j), (-1, -1));
			}

		void Dfs(Point v, Point pv)
		{
			q.Push(v);
			u[v] = true;
			if (q.Count == k)
			{
				Console.WriteLine(k);
				Console.WriteLine(string.Join("\n", q.Select(p => p + (1, 1))));
				Environment.Exit(0);
			}

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				if (u[nv]) continue;
				Dfs(nv, v);
			}
			q.Pop();
			u[v] = false;
		}
	}
}
