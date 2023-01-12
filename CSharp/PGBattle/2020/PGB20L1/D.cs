using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMapList(n + 1, es, true);
		foreach (var l in map)
		{
			l.Sort();
		}

		var (c, p) = Bfs(n + 1, v => map[v].ToArray(), 1, n);
		if (c[n] == long.MaxValue) return -1;
		return string.Join(" ", GetPathVertexes(p, n));
	}

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

	public static (long[] costs, int[] prevs) Bfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var prevs = Array.ConvertAll(costs, _ => -1);
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
				prevs[nv] = v;
				if (nv == ev) return (costs, prevs);
				q.Enqueue(nv);
			}
		}
		return (costs, prevs);
	}

	public static int[] GetPathVertexes(int[] p, int ev)
	{
		var path = new Stack<int>();
		path.Push(ev);
		for (var e = p[ev]; e != -1; e = p[e])
			path.Push(e);
		return path.ToArray();
	}
}
