using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int v, int k) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var x = Read();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var map = ToMap(n + 1, es, false);
		var vqmap = ToMap(n + 1, Enumerable.Range(0, qc).Select(qi => new[] { qs[qi].v, qi }).ToArray(), true);

		var r = new int[qc];
		Dfs(1, -1);
		return string.Join("\n", r);

		int[] Dfs(int v, int pv)
		{
			var top = new[] { x[v - 1] };

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;

				top = Merge(top, Dfs(nv, v));
			}

			foreach (var qi in vqmap[v])
			{
				r[qi] = top[qs[qi].k - 1];
			}

			return top;
		}

		int[] Merge(int[] a, int[] b)
		{
			return a.Take(20).Concat(b.Take(20)).OrderBy(x => -x).Take(20).ToArray();
		}
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
