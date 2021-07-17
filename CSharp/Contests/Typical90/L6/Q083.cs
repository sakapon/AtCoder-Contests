using System;
using System.Collections.Generic;
using System.Linq;

class Q083
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var rm = (int)Math.Sqrt(m);
		var map = ToMap(n + 1, es, false);
		var stars = Array.ConvertAll(map, l => l.Count > rm);

		var qmap = Array.ConvertAll(map, _ => new List<int>());
		for (int v = 1; v <= n; v++)
			if (stars[v])
				foreach (var nv in map[v])
					qmap[nv].Add(v);

		for (int v = 1; v <= n; v++)
			map[v].Add(v);

		var orders = Array.ConvertAll(map, _ => -1);
		var lazy = Array.ConvertAll(map, _ => -1);

		int GetColor(int v)
		{
			var qi = qmap[v].Count == 0 ? -1 : qmap[v].Max(nv => lazy[nv]);
			qi = Math.Max(qi, orders[v]);
			return qi == -1 ? 1 : qs[qi].y;
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int qi = 0; qi < qc; qi++)
		{
			var (x, y) = qs[qi];
			Console.WriteLine(GetColor(x));

			if (stars[x])
			{
				orders[x] = qi;
				lazy[x] = qi;
			}
			else
			{
				foreach (var nv in map[x])
				{
					orders[nv] = qi;
				}
			}
		}
		Console.Out.Flush();
	}

	static List<int>[] ToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
