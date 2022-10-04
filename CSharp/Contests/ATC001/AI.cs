using System;
using System.Collections.Generic;
using System.Linq;

class AI
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var map = GetAdjacencyList(h, w, s);
		var sseq = s.SelectMany(a => a).ToArray();
		var sv = Array.IndexOf(sseq, 's');
		var ev = Array.IndexOf(sseq, 'g');

		var r = Dfs(h * w, v => map[v].ToArray(), sv, ev);
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

	// 再帰関数よりも Stack のほうが高速です。
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
