using System;
using System.Collections.Generic;
using System.Linq;

class I
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, m, k) = Read3();
		var h = Read();
		var c = Read();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		foreach (var e in es)
		{
			if (h[e[0] - 1] < h[e[1] - 1])
			{
				map[e[0]].Add(e[1]);
			}
			else
			{
				map[e[1]].Add(e[0]);
			}
		}
		var r = Bfs(n + 1, v => map[v].ToArray(), c);
		Console.WriteLine(string.Join("\n", r.Skip(1).Select(x => x == long.MaxValue ? -1 : x)));
	}

	public static long[] Bfs(int vertexesCount, Func<int, int[]> getNextVertexes, int[] startVertexIds, int endVertexId = -1)
	{
		var costs = Array.ConvertAll(new bool[vertexesCount], _ => long.MaxValue);
		var inVertexes = Array.ConvertAll(costs, _ => -1);
		var q = new Queue<int>();
		foreach (var v in startVertexIds)
		{
			costs[v] = 0;
			q.Enqueue(v);
		}

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			foreach (var nv in getNextVertexes(v))
			{
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				inVertexes[nv] = v;
				if (nv == endVertexId) return costs;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}
