using System;
using System.Collections.Generic;

class KShortestWalk
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1], s = h[2], t = h[3], k = h[4];
		var es = Array.ConvertAll(new bool[m], _ => Read());

		Console.WriteLine(string.Join("\n", Dijkstra(n, es, s, t, k, true)));
	}

	struct Edge
	{
		public int From, To, Weight;
	}

	static List<Edge>[] ToMap2(int n, int[][] es, bool directed = false)
	{
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<Edge>());
		foreach (var e in es)
		{
			map[e[0]].Add(new Edge { From = e[0], To = e[1], Weight = e[2] });
			if (!directed) map[e[1]].Add(new Edge { From = e[1], To = e[0], Weight = e[2] });
		}
		return map;
	}

	static long[] Dijkstra(int n, int[][] es, int sv, int ev, int k, bool directed = false)
	{
		var map = ToMap2(n, es, directed);

		var r = new List<long>();
		var q = PQ<(int v, long d)>.Create(x => x.d);
		q.Push((sv, 0));

		while (q.Count > 0)
		{
			var (v, qd) = q.Pop().value;
			if (v == ev)
			{
				r.Add(qd);
				if (r.Count == k) return r.ToArray();
			}

			foreach (var e in map[v])
			{
				q.Push((e.To, qd + e.Weight));
			}
		}
		while (r.Count < k) r.Add(-1);
		return r.ToArray();
	}
}
