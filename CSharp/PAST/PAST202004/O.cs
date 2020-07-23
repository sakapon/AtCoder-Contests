using System;
using System.Collections.Generic;
using System.Linq;

class O
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var rs = new int[m].Select(_ => Read()).Select((x, id) => (id, a: x[0], b: x[1], c: (long)x[2])).ToArray();
		var n2 = Enumerable.Range(0, 20).Select(i => 1 << i).First(x => x >= n - 1);

		var uf = new UF(n + 1);
		var minEdges = new List<int>();
		var extraEdges = new int[n2].Select(_ => new List<int>()).ToArray();
		var maxCost = new long[m];

		foreach (var r in rs.OrderBy(r => r.c))
		{
			if (!uf.AreUnited(r.a, r.b))
			{
				uf.Unite(r.a, r.b);
				minEdges.Add(r.id);
			}
			else
			{
				extraEdges[n2 >> 1].Add(r.id);
			}
		}
		var minCost = minEdges.Sum(id => rs[id].c);

		// 並列二分探索。
		for (int f = n2 >> 1; f > 0; f >>= 1)
		{
			var uf2 = new UF(n + 1);
			for (int i = 0; i < n2; i++)
			{
				// unite する前に判定します。
				if (i % f == 0 && i / f % 2 == 1)
				{
					foreach (var id in extraEdges[i])
					{
						var (_, a, b, _) = rs[id];
						if (f > 1)
						{
							if (uf2.AreUnited(a, b)) extraEdges[i - (f >> 1)].Add(id);
							else extraEdges[i + (f >> 1)].Add(id);
						}
						else
						{
							var maxIndex = uf2.AreUnited(a, b) ? i - 1 : i;
							maxCost[id] = rs[minEdges[maxIndex]].c;
						}
					}
					extraEdges[i].Clear();
				}

				if (i < n - 1)
				{
					var r = rs[minEdges[i]];
					uf2.Unite(r.a, r.b);
				}
			}
		}

		Console.WriteLine(string.Join("\n", maxCost.Select((x, id) => x == 0 ? minCost : minCost + rs[id].c - x)));
	}
}
