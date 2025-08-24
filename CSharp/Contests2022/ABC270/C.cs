using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x, y) = Read3();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var root = x;
		var map = ToMap(n + 1, es, true);

		var path = new List<int>();
		DFS(root, -1);
		return string.Join(" ", path);

		bool DFS(int v, int pv)
		{
			path.Add(v);
			if (v == y) return true;

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				if (DFS(nv, v)) return true;
			}
			path.RemoveAt(path.Count - 1);
			return false;
		}
	}

	public static int[][] ToMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (twoWay) map[e[1]].Add(e[0]);
		}
		return Array.ConvertAll(map, l => l.ToArray());
	}
}
