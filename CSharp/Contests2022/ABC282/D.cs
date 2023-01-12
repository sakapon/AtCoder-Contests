using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var r = 0L;
		var map = ToListMap(n + 1, es, true);
		var u = new int[n + 1];
		Array.Fill(u, -1);
		var path = new List<int>();

		for (int v = 1; v <= n; v++)
		{
			if (u[v] != -1) continue;
			u[v] = 0;
			path.Clear();
			path.Add(v);
			if (!Dfs(v, -1)) return 0;

			var counts = new long[2];
			counts[0] = path.Count(pv => u[pv] == 0);
			counts[1] = path.Count - counts[0];

			foreach (var pv in path)
			{
				r += counts[1 - u[pv]] - map[pv].Count;
				r += n - path.Count;
			}
		}
		return r / 2;

		bool Dfs(int v, int pv)
		{
			foreach (var nv in map[v])
			{
				if (nv == pv) continue;

				if (u[nv] == -1)
				{
					u[nv] = 1 - u[v];
				}
				else
				{
					if (u[nv] == 1 - u[v])
					{
						continue;
					}
					else
					{
						return false;
					}
				}

				path.Add(nv);
				if (!Dfs(nv, v)) return false;
			}
			return true;
		}
	}

	public static List<int>[] ToListMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (twoWay) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
