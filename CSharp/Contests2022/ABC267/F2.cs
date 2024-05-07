using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var rn = Enumerable.Range(1, n).ToArray();

		var map = ToMap(n + 1, es, true);
		var depths = new int[n + 1];

		var qmap = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int qi = 0; qi < qc; qi++)
			qmap[qs[qi].Item1].Add(qi);

		var dqmap = Array.ConvertAll(new bool[n], _ => new List<int>());

		var r = new int[qc];
		Array.Fill(r, -1);

		Search0(1);
		var rv1 = rn.MaxBy(v => depths[v]);
		Search(rv1);
		var rv2 = rn.MaxBy(v => depths[v]);
		Search(rv2);
		return string.Join("\n", r);

		void Search0(int root)
		{
			Array.Fill(depths, -1);
			depths[root] = 0;
			DFS0(root);

			void DFS0(int v)
			{
				foreach (var nv in map[v])
				{
					if (depths[nv] != -1) continue;
					depths[nv] = depths[v] + 1;
					DFS0(nv);
				}
			}
		}

		void Search(int root)
		{
			Array.Fill(depths, -1);
			depths[root] = 0;
			DFS(root);

			void DFS(int v)
			{
				foreach (var nv in map[v])
				{
					if (depths[nv] != -1) continue;
					depths[nv] = depths[v] + 1;
					DFS(nv);
				}

				foreach (var qi in qmap[v])
				{
					var d = depths[v] - qs[qi].Item2;
					if (d >= 0) dqmap[d].Add(qi);
				}

				foreach (var qi in dqmap[depths[v]])
				{
					r[qi] = v;
				}
				dqmap[depths[v]].Clear();
			}
		}
	}

	public static int[][] ToMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (twoWay) map[e[1]].Add(e[0]);
		}
		return Array.ConvertAll(map, l => l.ToArray());
	}
}
