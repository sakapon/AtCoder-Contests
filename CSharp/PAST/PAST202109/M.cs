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
		Array.Fill(exp, (1, max));
		exp[1] = (1, 0);

		var x = max;
		var xmin = 0L;
		var xmax = max;

		if (!Dfs(1, -1)) return -1;

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

				var (nsign, nd) = (-sign, e[2] - d);

				if (exp[nv].d == max)
				{
					exp[nv] = (nsign, nd);
					if (nsign > 0)
					{
						xmin = Math.Max(xmin, -nd);
					}
					else
					{
						xmax = Math.Min(xmax, nd);
					}
					if (!Dfs(nv, v)) return false;
				}
				else
				{
					var (nsign0, nd0) = exp[nv];
					if (nsign == nsign0)
					{
						if (nd != nd0) return false;
					}
					else
					{
						if ((nd - nd0) % 2 != 0) return false;
						var nx = nsign0 * (nd - nd0) / 2;
						if (x == max)
						{
							x = nx;
						}
						else
						{
							if (nx != x) return false;
						}
					}
				}
			}
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
