using System;
using System.Collections.Generic;
using System.Linq;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], qc = h[1];
		var es = new bool[n - 1].Select(_ => Read()).Select((e, ei) => new[] { e[0], e[1], ei + 1 }).ToArray();
		var map = EdgesToMap(n + 1, es, false);

		var colorsMap = Array.ConvertAll(new bool[n + 1], _ => new List<(int qi, int c)>());
		for (int qi = 0; qi < qc; qi++)
		{
			var q = Read();
			colorsMap[q[0]].Add((qi, q[2]));
			colorsMap[q[1]].Add((qi, q[2]));
		}

		var colors = new int[n];

		(PQ<(int qi, int c)>, HashSet<int>) Dfs(int v, int pv)
		{
			PQ<(int qi, int c)> q = null;
			HashSet<int> u = null;

			foreach (var e in map[v])
			{
				if (e[1] == pv) continue;

				var (rq, ru) = Dfs(e[1], v);

				while (rq.Any() && !ru.Contains(rq.First.qi))
					rq.Pop();
				if (rq.Any())
					colors[e[2]] = rq.First.c;

				if (q == null)
				{
					(q, u) = (rq, ru);
				}
				else
				{
					if (q.Count < rq.Count)
					{
						(q, rq) = (rq, q);
						(u, ru) = (ru, u);
					}

					foreach (var (qi, c) in rq)
					{
						if (!ru.Contains(qi)) continue;

						if (!u.Remove(qi))
						{
							q.Push((qi, c));
							u.Add(qi);
						}
					}
				}
			}

			if (q == null)
			{
				q = PQ<(int qi, int c)>.Create(x => x.qi, true);
				u = new HashSet<int>();
			}
			foreach (var (qi, c) in colorsMap[v])
			{
				if (!u.Remove(qi))
				{
					q.Push((qi, c));
					u.Add(qi);
				}
			}
			return (q, u);
		}

		Dfs(1, -1);
		Console.WriteLine(string.Join("\n", colors.Skip(1)));
	}

	static List<int[]>[] EdgesToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}
