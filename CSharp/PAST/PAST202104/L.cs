using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

class L
{
	static bool[] ft = new[] { false, true };
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int x, int y, int r) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		var cs = Array.ConvertAll(new bool[m], _ => Read3());

		var es = new List<(int, int, double)>();
		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				es.Add((i, j, GetDistance(ps[i], ps[j])));
			}
		}

		var r = double.MaxValue;
		var rm = Enumerable.Range(0, m).ToArray();

		Power(ft, m, b =>
		{
			var es2 = es.ToList();
			var cs2 = rm.Where(j => b[j]).Select(j => cs[j]).ToArray();

			for (int i = 0; i < cs2.Length; i++)
			{
				for (int j = i + 1; j < cs2.Length; j++)
				{
					es2.Add((n + i, n + j, GetDistance(cs2[i], cs2[j])));
				}
			}

			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < cs2.Length; j++)
				{
					es2.Add((i, n + j, GetDistance(ps[i], cs2[j])));
				}
			}

			var mes = Kruskal(n + cs2.Length, es2.ToArray());
			r = Math.Min(r, mes.Sum(e => e.Item3));
		});

		return r;
	}

	static double GetDistance((int x, int y) p1, (int x, int y) p2)
	{
		var dx = p1.x - p2.x;
		var dy = p1.y - p2.y;
		return Math.Sqrt(dx * dx + dy * dy);
	}

	static double GetDistance((int x, int y, int r) c1, (int x, int y, int r) c2)
	{
		var d = GetDistance((c1.x, c1.y), (c2.x, c2.y));
		if (d > c1.r + c2.r) return d - c1.r - c2.r;
		if (d < Math.Abs(c1.r - c2.r)) return Math.Abs(c1.r - c2.r) - d;
		return 0;
	}

	static double GetDistance((int x, int y) p, (int x, int y, int r) c)
	{
		var d = GetDistance(p, (c.x, c.y));
		return Math.Abs(d - c.r);
	}

	public static void Power<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2);
				else action(p);
			}
		}
	}

	static (int, int, double)[] Kruskal(int n, (int, int, double)[] es)
	{
		var uf = new UF(n);
		var minEdges = new List<(int, int, double)>();

		foreach (var e in es.OrderBy(e => e.Item3))
		{
			if (uf.AreUnited(e.Item1, e.Item2)) continue;
			uf.Unite(e.Item1, e.Item2);
			minEdges.Add(e);
		}
		return minEdges.ToArray();
	}
}
