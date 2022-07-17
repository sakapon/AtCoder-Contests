using System;
using System.Collections.Generic;

class M2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int[]>());
		for (int ei = 0; ei < n - 1; ei++)
		{
			var e = es[ei];
			map[e[0]].Add(new[] { e[0], e[1], ei });
			map[e[1]].Add(new[] { e[1], e[0], ei });
		}

		var qis = Array.ConvertAll(new bool[n + 1], _ => new SortedSet<int>());
		for (int qi = 0; qi < qc; qi++)
		{
			var q = qs[qi];
			qis[q[0]].Add(qi);
			qis[q[1]].Add(qi);
		}

		var colors = new int[n - 1];
		Dfs(1, -1);
		return string.Join("\n", colors);

		SortedSet<int> Dfs(int v, int pv)
		{
			var r = qis[v];

			foreach (var e in map[v])
			{
				if (e[1] == pv) continue;
				var s = Dfs(e[1], v);
				if (s.Count > 0) colors[e[2]] = qs[s.Max][2];
				r = Merge(r, s);
			}
			return r;
		}
	}

	public static SortedSet<int> Merge(SortedSet<int> s1, SortedSet<int> s2)
	{
		if (s1.Count < s2.Count) (s1, s2) = (s2, s1);
		foreach (var v in s2)
			if (!s1.Add(v)) s1.Remove(v);
		return s1;
	}
}
