﻿using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var map = ToMap(n + 1, es, false);
		var set = new HashSet<int>();

		return string.Join("\n", qs.Select(q =>
		{
			// BFS。
			// DFS で枝狩りをする場合は注意。
			set.Clear();
			set.Add(q[0]);

			while (q[1]-- > 0)
			{
				foreach (var v in set.ToArray())
				{
					foreach (var nv in map[v])
					{
						set.Add(nv);
					}
				}
			}
			return set.Sum();
		}));
	}

	public static int[][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
	public static List<int>[] ToMapList(int n, int[][] es, bool directed)
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
