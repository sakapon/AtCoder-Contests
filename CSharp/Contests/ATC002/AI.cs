using System;
using System.Collections.Generic;
using System.Linq;

class AI
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var (si, sj) = Read2();
		var (ei, ej) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		si--; sj--;
		ei--; ej--;
		var sv = w * si + sj;
		var ev = w * ei + ej;

		var map = GetAdjacencyList(h, w, s);
		var r = Bfs(h * w, v => map[v].ToArray(), sv, ev);
		return r[ev];
	}

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

	public static long[] Bfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			foreach (var nv in nexts(v))
			{
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				if (nv == ev) return costs;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}
