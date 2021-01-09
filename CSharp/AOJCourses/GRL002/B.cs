using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], r = h[2];
		var es = Array.ConvertAll(new int[h[1]], _ => Read());

		Console.WriteLine(MinCostArborescence(n, r, es));
	}

	// n: 個数
	// es: { from, to, cost }
	static int MinCostArborescence(int n, int sv, int[][] es)
	{
		// 逆向き。
		var map = Array.ConvertAll(new int[n], _ => new List<int[]>());
		foreach (var e in es)
			map[e[1]].Add(e);

		var uf = new UF(n);
		return MinCostArborescence(n, sv, map, uf);
	}

	static int MinCostArborescence(int n, int sv, List<int[]>[] map, UF uf)
	{
		// 入る辺のうち最小のもの。
		var minEdges = new int[n][];

		for (int v = 0; v < n; v++)
		{
			if (v == sv || map[v]?.Any() != true) continue;
			var min = map[v].Min(e => e[2]);
			minEdges[v] = map[v].First(e => e[2] == min);
			uf.Unite(v, minEdges[v][0]);
		}

		var rn = Enumerable.Range(0, n).ToArray();
		if (rn.All(v => uf.AreUnited(v, sv))) return minEdges.Sum(e => e?[2] ?? 0);

		// 根と非連結になっているパスのうち、サイクルとなっている部分を縮約します。
		// 0 型と 6 型のいずれか。
		var sum = 0;
		var u = new bool[n];
		var reductMap = new int[n];
		for (int v = 0; v < n; v++)
		{
			if (uf.AreUnited(v, sv))
			{
				map[v].Clear();
				sum += minEdges[v]?[2] ?? 0;
				reductMap[v] = sv;
				continue;
			}
			if (u[v]) continue;

			var cycle = Array.FindAll(rn, i => uf.AreUnited(v, i));
			foreach (var i in cycle) u[i] = true;

			// vc0 に縮約します。
			var vc0 = cycle[0];

			var gs2 = cycle.Where(i => minEdges[i] != null).Select(i => minEdges[i][0]).GroupBy(i => i).Where(g => g.Count() == 2).ToArray();
			if (gs2.Any())
			{
				// 6 型の場合。
				vc0 = gs2[0].Key;
				var path = new List<int> { vc0 };
				for (var t = vc0; (t = minEdges[t][0]) != vc0;)
					path.Add(t);
				cycle = path.ToArray();
			}

			var cycleSum = cycle.Sum(i => minEdges[i]?[2] ?? 0);

			foreach (var i in cycle)
			{
				reductMap[i] = vc0;
				map[i].RemoveAll(e => uf.AreUnited(e[0], e[1]));
				if (i == vc0)
				{
					foreach (var e in map[i])
					{
						e[2] += cycleSum - minEdges[i][2];
					}
				}
				else
				{
					foreach (var e in map[i])
					{
						e[2] += cycleSum - minEdges[i][2];
						map[vc0].Add(e);
					}
					map[i].Clear();
				}
			}
		}

		foreach (var es in map)
			foreach (var e in es)
			{
				e[0] = reductMap[e[0]];
				e[1] = reductMap[e[1]];
			}

		return MinCostArborescence(n, sv, map, uf) + sum;
	}
}
