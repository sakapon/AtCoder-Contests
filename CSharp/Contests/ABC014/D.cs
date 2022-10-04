using System;
using System.Collections.Generic;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}

		var qimap = Array.ConvertAll(new bool[n + 1], _ => new HashSet<int>());
		for (int qi = 0; qi < qc; qi++)
		{
			var q = qs[qi];
			qimap[q[0]].Add(qi);
			qimap[q[1]].Add(qi);
		}

		var depths = new int[n + 1];
		var r = new int[qc];
		Dfs(1, -1, 0);
		return string.Join("\n", r);

		void Dfs(int v, int pv, int d)
		{
			depths[v] = d;
			var set = qimap[v];
			foreach (var nv in map[v])
			{
				if (nv == pv) continue;

				Dfs(nv, v, d + 1);
				var set2 = qimap[nv];
				if (set.Count < set2.Count) (set, set2) = (set2, set);

				foreach (var qi in set2)
				{
					if (set.Add(qi)) continue;
					set.Remove(qi);
					var q = qs[qi];
					r[qi] = depths[q[0]] + depths[q[1]] - 2 * d + 1;
				}
			}
			qimap[v] = set;
		}
	}
}
