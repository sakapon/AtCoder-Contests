using System;
using System.Collections.Generic;
using System.Linq;

class TreeDiameter
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var (M, path) = Diameter(n, 0, es);
		Console.WriteLine($"{M} {path.Length}");
		Console.WriteLine(string.Join(" ", path));
	}

	static (long, int[]) Diameter(int n, int sv, int[][] es)
	{
		// 使われない頂点が存在してもかまいません。
		var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[1], e[2] });
			map[e[1]].Add(new[] { e[0], e[2] });
		}

		var (d1, _) = Distances(n, sv, map);
		var ev1 = Enumerable.Range(0, n + 1).OrderBy(v => -d1[v]).First();
		var (d2, from) = Distances(n, ev1, map);
		var ev2 = Enumerable.Range(0, n + 1).OrderBy(v => -d2[v]).First();
		return (d2[ev2], GetPath(from, ev2));
	}

	static (long[], int[]) Distances(int n, int sv, List<int[]>[] map)
	{
		var from = new int[n + 1];
		var d = new long[n + 1];
		var q = new Queue<int>();
		from[sv] = -1;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			foreach (var e in map[v])
			{
				if (e[0] == from[v]) continue;
				from[e[0]] = v;
				d[e[0]] = d[v] + e[1];
				q.Enqueue(e[0]);
			}
		}
		return (d, from);
	}

	static int[] GetPath(int[] from, int ev)
	{
		var path = new List<int>();
		for (var v = ev; v != -1; v = from[v])
			path.Add(v);
		return path.ToArray();
	}
}
