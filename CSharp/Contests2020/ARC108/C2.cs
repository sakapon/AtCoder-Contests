using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var es = Array.ConvertAll(new bool[m], _ => Read());

		es = GetTree(n + 1, es, false, 1);
		var map = EdgesToMap2(n + 1, es, true);
		var r = new int[n + 1];
		r[1] = 1;

		void Dfs(int v, int pv = -1)
		{
			foreach (var e in map[v])
			{
				if (e[1] == pv) continue;

				if (e[2] == r[v])
					r[e[1]] = e[2] == n ? 1 : e[2] + 1;
				else
					r[e[1]] = e[2];

				Dfs(e[1], v);
			}
		}

		Dfs(1);
		Console.WriteLine(string.Join("\n", r.Skip(1)));
	}

	static List<int[]>[] EdgesToMap2(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}

	static int[][] GetTree(int n, int[][] es, bool directed, int rv)
	{
		var map = EdgesToMap2(n, es, directed);
		var des = new List<int[]>();
		var u = new bool[n];
		Dfs(rv);
		return des.ToArray();

		void Dfs(int v)
		{
			u[v] = true;
			foreach (var e in map[v])
			{
				if (u[e[1]]) continue;
				des.Add(e);
				Dfs(e[1]);
			}
		}
	}
}
