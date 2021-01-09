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

		var colorsMap = Array.ConvertAll(new bool[n + 1], _ => PQ<(int qi, int c)>.Create(x => x.qi, true));
		for (int qi = 0; qi < qc; qi++)
		{
			var q = Read();
			colorsMap[q[0]].Push((qi, q[2]));
			colorsMap[q[1]].Push((qi, q[2]));
		}

		var uf = new UF<PQ<(int qi, int c)>>(n + 1, (q1, q2) =>
		{
			if (q1.Count < q2.Count) (q1, q2) = (q2, q1);
			q1.PushRange(q2.ToArray());
			return q1;
		},
		colorsMap);

		var colors = new int[n];

		void Dfs(int v, int pv = -1)
		{
			foreach (var e in map[v])
			{
				if (e[1] == pv) continue;

				Dfs(e[1], v);

				var q = uf.GetValue(e[1]);
				while (q.Any())
				{
					var (qi, c) = q.Pop();
					if (q.Any() && q.First.qi == qi)
					{
						q.Pop();
					}
					else
					{
						q.Push((qi, c));
						break;
					}
				}
				if (q.Any()) colors[e[2]] = q.First.c;
				uf.Unite(e[1], v);
			}
		}

		Dfs(1);
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
