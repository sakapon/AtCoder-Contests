using System;
using System.Collections.Generic;
using System.Linq;

class A14
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var es = Array.ConvertAll(new bool[h[1]], _ => Read());

		var r = Dijklmna(h[0], es, true, h[2]);
		Console.WriteLine(string.Join("\n", r.Item1.Select(x => x == long.MaxValue ? "INF" : $"{x}")));
	}

	static Tuple<long[], int[][]> Dijklmna(int n, int[][] es, bool directed, int sv, int ev = -1)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}

		var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var inEdges = new int[n][];
		var q = new Queue<int>();
		cs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();

			foreach (var e in map[v])
			{
				if (cs[e[1]] <= cs[v] + e[2]) continue;
				cs[e[1]] = cs[v] + e[2];
				inEdges[e[1]] = e;
				q.Enqueue(e[1]);
			}
		}
		return Tuple.Create(cs, inEdges);
	}
}
