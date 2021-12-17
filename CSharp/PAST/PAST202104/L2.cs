using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

class L2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int x, int y, int r) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => { var a = Read(); return (x: a[0], y: a[1], r: 0); });
		var cs = Array.ConvertAll(new bool[m], _ => Read3());

		var nm = n + m;
		var pcs = ps.Concat(cs).ToArray();

		var dmap = new double[nm, nm];
		for (int i = 0; i < nm; i++)
		{
			for (int j = 0; j < nm; j++)
			{
				dmap[i, j] = GetDistance(pcs[i], pcs[j]);
			}
		}

		var r = double.MaxValue;
		var rn = Enumerable.Range(0, n).ToArray();

		AllBoolCombination(m, b =>
		{
			var ilist = rn.ToList();
			for (int i = 0; i < m; i++)
			{
				if (b[i])
				{
					ilist.Add(n + i);
				}
			}

			var es = new List<(int, int, double)>();
			for (int i = 0; i < ilist.Count; i++)
			{
				for (int j = i + 1; j < ilist.Count; j++)
				{
					es.Add((i, j, dmap[ilist[i], ilist[j]]));
				}
			}

			var mes = Kruskal(ilist.Count, es.ToArray());
			r = Math.Min(r, mes.Sum(e => e.Item3));
			return false;
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

	public static void AllBoolCombination(int n, Func<bool[], bool> action)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		for (int x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;
			if (action(b)) break;
		}
	}

	public static (int, int, double)[] Kruskal(int n, (int, int, double)[] ues)
	{
		var uf = new UF(n);
		var mes = new List<(int, int, double)>();

		foreach (var e in ues.OrderBy(e => e.Item3))
		{
			if (uf.AreUnited(e.Item1, e.Item2)) continue;
			uf.Unite(e.Item1, e.Item2);
			mes.Add(e);
		}
		return mes.ToArray();
	}
}
