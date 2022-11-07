using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var map = GetAdjacencyList(h, w, c);
		var si = Enumerable.Range(0, h).First(i => c[i].Contains('S'));
		var sj = c[si].IndexOf('S');
		var sv = si * w + sj;

		var colors = new int[h * w];
		var q = new Queue<int>();
		colors[sv] = -1;

		for (int i = 0; i < map[sv].Count; i++)
		{
			colors[map[sv][i]] = i + 1;
			q.Enqueue(map[sv][i]);
		}

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = colors[v];

			foreach (var nv in map[v])
			{
				if (colors[nv] == -1) continue;
				if (colors[nv] == nc) continue;
				if (colors[nv] > 0) return true;

				colors[nv] = nc;
				q.Enqueue(nv);
			}
		}

		return false;
	}

	// undirected
	public static List<int>[] GetAdjacencyList(int h, int w, string[] s, char wall = '#')
	{
		var map = Array.ConvertAll(new bool[h * w], _ => new List<int>());
		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				var v = i * w + j;
				if (s[i][j] == wall || s[i][j - 1] == wall) continue;
				map[v].Add(v - 1);
				map[v - 1].Add(v);
			}
		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				var v = i * w + j;
				if (s[i][j] == wall || s[i - 1][j] == wall) continue;
				map[v].Add(v - w);
				map[v - w].Add(v);
			}
		return map;
	}
}
