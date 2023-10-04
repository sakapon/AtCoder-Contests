using System;
using System.Collections.Generic;
using System.Linq;

class A2310
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var sseq = s.SelectMany(t => t).ToArray();
		var sv = Array.IndexOf(sseq, 's');
		var ev = Array.IndexOf(sseq, 'g');

		var map = GetUnweightedAdjacencyList(s);
		var r = ByBFS(map, sv);
		return r[ev];
	}

	public static List<int>[] GetUnweightedAdjacencyList(string[] s, char wall = '#')
	{
		var h = s.Length;
		var w = h == 0 ? 0 : s[0].Length;
		var map = Array.ConvertAll(new bool[h * w], _ => new List<int>());
		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				if (s[i][j] == wall || s[i][j - 1] == wall) continue;
				var v = w * i + j;
				map[v].Add(v - 1);
				map[v - 1].Add(v);
			}
		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				if (s[i][j] == wall || s[i - 1][j] == wall) continue;
				var v = w * i + j;
				map[v].Add(v - w);
				map[v - w].Add(v);
			}
		return map;
	}

	public static bool[] ByBFS(List<int>[] map, int sv)
	{
		var n = map.Length;
		var u = new bool[n];
		var q = new Queue<int>();
		u[sv] = true;
		q.Enqueue(sv);

		while (q.TryDequeue(out var v))
		{
			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				u[nv] = true;
				q.Enqueue(nv);
			}
		}
		return u;
	}

	public static bool[] ByBFS2(List<int>[] map, int sv)
	{
		var n = map.Length;
		var u = new bool[n];
		var q = new Queue<int>();
		q.Enqueue(sv);

		while (q.TryDequeue(out var v))
		{
			if (u[v]) continue;
			u[v] = true;
			foreach (var nv in map[v])
			{
				q.Enqueue(nv);
			}
		}
		return u;
	}

	public static bool[] ByDFS(List<int>[] map, int sv)
	{
		var n = map.Length;
		var u = new bool[n];
		u[sv] = true;
		DFS(sv);
		return u;

		void DFS(int v)
		{
			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				u[nv] = true;
				DFS(nv);
			}
		}
	}

	public static bool[] ByDFS2(List<int>[] map, int sv)
	{
		var n = map.Length;
		var u = new bool[n];
		DFS(sv);
		return u;

		void DFS(int v)
		{
			if (u[v]) return;
			u[v] = true;
			foreach (var nv in map[v])
			{
				DFS(nv);
			}
		}
	}
}
