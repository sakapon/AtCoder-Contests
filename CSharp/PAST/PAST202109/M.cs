using System;
using System.Collections.Generic;
using System.Linq;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(n + 1, es, false);
		const long max = 1L << 60;

		// ±x+d
		var exp = new (int sign, long d)[n + 1];
		exp[1] = (1, 0);

		var x = max;
		if (!Dfs(1, -1)) return -1;

		var xmin = exp.Where(p => p.sign == 1).Max(p => -p.d);
		var xmax = exp.Where(p => p.sign == -1).Min(p => p.d);

		if (xmin > xmax) return -1;
		if (x == max)
		{
			x = xmin;
		}
		else
		{
			if (!(xmin <= x && x <= xmax)) return -1;
		}

		return string.Join("\n", exp[1..].Select(p => p.sign * x + p.d));

		bool Dfs(int v, int pv)
		{
			var (sign, d) = exp[v];

			foreach (var e in map[v])
			{
				var nv = e[1];
				if (nv == pv) continue;

				var np = (-sign, e[2] - d);

				if (exp[nv].sign == 0)
				{
					exp[nv] = np;
					if (!Dfs(nv, v)) return false;
				}
				else
				{
					if (!AddCondition(exp[nv], np)) return false;
				}
			}
			return true;
		}

		bool AddCondition((int, long) p1, (int, long) p2)
		{
			var (sign1, d1) = p1;
			var (sign2, d2) = p2;

			if (sign1 == sign2) return d1 == d2;

			if ((d2 - d1) % 2 != 0) return false;
			var nx = sign1 * (d2 - d1) / 2;

			if (x != max) return nx == x;

			x = nx;
			return true;
		}
	}

	public static int[][][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
	static List<int[]>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}
