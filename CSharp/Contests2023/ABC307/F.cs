using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

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
		foreach (var sv in a)
		{
			costs[sv] = (0, 0);
			q.Add((0, 0, sv));
		}

		var set = new WBSet<(long c, long d)>();
		for (int d = 1; d <= D; d++)
		{
			set.Add((x[d - 1], d));
		}

		while (q.Count > 0)
		{
			var (d, c, v) = q.Min;
			q.Remove((d, c, v));

			while (D - set.Count < d)
			{
				var d0 = D - set.Count + 1;
				set.Remove((x[d0 - 1], d0));
			}

			foreach (var e in map[v])
			{
				var nv = e[1];
				var (nc, nd) = e[2] <= -c ? (c + e[2], d) : FindNextDay();
				if (nd > D) continue;

				// 次の日以降で e[2] 以上の距離がある最小の日 (WA)
				(long c, long d) FindNextDay()
				{
					var node = set.GetFirst(x => x.CompareTo((e[2], d + 1)) >= 0);
					if (node == null) return (0, D + 1);
					var (nc, nd) = node.Item;
					return (-nc + e[2], nd);
				}

				if (costs[nv].CompareTo((nd, nc)) <= 0) continue;
				if (costs[nv] != (long.MaxValue, 0L)) q.Remove((costs[nv].d, costs[nv].c, nv));
				q.Add((nd, nc, nv));
				costs[nv] = (nd, nc);
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
