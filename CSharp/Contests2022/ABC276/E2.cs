using System;
using System.Collections.Generic;
using System.Linq;

class E2
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
		var svs = map[sv];

		c[si] = c[si].Replace('S', '#');
		map = GetAdjacencyList(h, w, c);

		for (int i = 0; i < svs.Count - 1; i++)
		{
			var u = Dfs(h * w, v => map[v].ToArray(), svs[i]);

			for (int j = i + 1; j < svs.Count; j++)
			{
				if (u[svs[j]]) return true;
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

	public static bool[] Dfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var u = new bool[n];
		var q = new Stack<int>();
		u[sv] = true;
		q.Push(sv);

		while (q.Count > 0)
		{
			var v = q.Pop();

			foreach (var nv in nexts(v))
			{
				if (u[nv]) continue;
				u[nv] = true;
				if (nv == ev) return u;
				q.Push(nv);
			}
		}
		return u;
	}
}
