using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(n + 1, es, true);
		return Enumerable.Range(1, n).Sum(sv => Dfs(n + 1, v => map[v].ToArray(), sv).Count(x => x));
	}

	static List<int>[] ToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}

	static bool[] Dfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
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
