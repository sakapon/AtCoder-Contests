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
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap2(n + 1, es, true);

		var (d, prevs) = Bfs2(n + 1, v => map[v], 1, n);
		var path = GetPathEdges(prevs, n);

		var r = Array.ConvertAll(new bool[m], _ => d[n]);

		var map2 = ToMapList(n + 1, es, true);
		foreach (var e in path)
		{
			var id = e[2];

			map2[e[0]].Remove(e[1]);

			var d2 = Bfs(n + 1, v => map2[v].ToArray(), 1, n);
			r[id] = d2[n];

			map2[e[0]].Add(e[1]);
		}

		return string.Join("\n", r.Select(x => x == long.MaxValue ? -1 : x));
	}

	public static int[][][] ToMap2(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList2(n, es, directed), l => l.ToArray());
	public static List<int[]>[] ToMapList2(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());

		// e[2]: ID
		for (int i = 0; i < es.Length; i++)
		{
			var e = es[i];
			map[e[0]].Add(new[] { e[0], e[1], i });
		}
		return map;
	}

	public static (long[] costs, int[][] prevs) Bfs2(int n, Func<int, int[][]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var prevs = new int[n][];
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			foreach (var e in nexts(v))
			{
				var nv = e[1];
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				prevs[nv] = e;
				if (nv == ev) return (costs, prevs);
				q.Enqueue(nv);
			}
		}
		return (costs, prevs);
	}

	public static int[][] GetPathEdges(int[][] inEdges, int ev)
	{
		var path = new Stack<int[]>();
		for (var e = inEdges[ev]; e != null; e = inEdges[e[0]])
			path.Push(e);
		return path.ToArray();
	}

	public static int[][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
	public static List<int>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
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
