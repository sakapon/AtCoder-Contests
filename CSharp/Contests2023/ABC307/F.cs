using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var K = int.Parse(Console.ReadLine());
		var a = Read();
		var D = int.Parse(Console.ReadLine());
		var x = Read();

		var map = ToWeightedListMap(n + 1, es, true);

		var costs = Array.ConvertAll(map, _ => (d: long.MaxValue, c: 0L));
		var q = new SortedSet<(long d, long c, int v)>();
		var q2 = new SortedSet<(long w, int v)>();
		foreach (var sv in a)
		{
			costs[sv] = (0, 0);
			q.Add((0, 0, sv));
		}

		for (int i = 0; i <= D; i++)
		{
			while (q2.Count > 0)
			{
				var dist = x[i - 1];
				var (w, nv) = q2.Min;
				var nc = -dist + w;
				if (nc > 0) break;
				q2.Remove((w, nv));

				if (costs[nv].CompareTo((i, nc)) <= 0) continue;
				if (costs[nv] != (long.MaxValue, 0L)) q.Remove((costs[nv].d, costs[nv].c, nv));
				q.Add((i, nc, nv));
				costs[nv] = (i, nc);
			}

			while (q.Count > 0)
			{
				var (d, c, v) = q.Min;
				q.Remove((d, c, v));

				foreach (var e in map[v])
				{
					var nv = e[1];
					var nc = c + e[2];
					if (nc <= 0)
					{
						if (costs[nv].CompareTo((d, nc)) <= 0) continue;
						if (costs[nv] != (long.MaxValue, 0L)) q.Remove((costs[nv].d, costs[nv].c, nv));
						q.Add((d, nc, nv));
						costs[nv] = (d, nc);
					}
					else
					{
						q2.Add((e[2], nv));
					}
				}
			}
		}

		return string.Join("\n", costs[1..].Select(p => p.d).Select(d => d == long.MaxValue ? -1 : d));
	}

	public static List<int[]>[] ToWeightedListMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (twoWay) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}
