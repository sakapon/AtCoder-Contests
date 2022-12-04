using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, qc) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var map = ToWeightedListMap(n + 1, es);

		var isInfs = new bool[n + 1];
		var roots = new int[n + 1];
		var costs = new long[n + 1];
		var q = new Stack<int>();

		for (int sv = 1; sv <= n; sv++)
		{
			if (roots[sv] != 0) continue;

			roots[sv] = sv;
			q.Push(sv);

			while (q.Count > 0)
			{
				var v = q.Pop();

				foreach (var e in map[v])
				{
					var nv = e[1];

					if (roots[nv] == 0)
					{
						roots[nv] = sv;
						costs[nv] = costs[v] + e[2];
						q.Push(nv);
					}
					else
					{
						if (costs[nv] != costs[v] + e[2])
						{
							isInfs[sv] = true;
						}
					}
				}
			}
		}

		var r = qs.Select(q =>
		{
			var (x, y) = (q[0], q[1]);
			if (roots[x] != roots[y]) return "nan";
			if (isInfs[roots[x]]) return "inf";
			return (costs[y] - costs[x]).ToString();
		});
		return string.Join("\n", r);
	}

	public static List<int[]>[] ToWeightedListMap(int n, int[][] es)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			map[e[1]].Add(new[] { e[1], e[0], -e[2] });
		}
		return map;
	}
}
