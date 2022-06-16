using System;
using System.Collections.Generic;
using System.Linq;

class C3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		map = Array.ConvertAll(new bool[n], _ => Read().Skip(1).ToArray());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		lcas = new int[qc];
		qis = Array.ConvertAll(new bool[n], _ => new HashSet<int>());

		for (int qi = 0; qi < qc; qi++)
		{
			var q = qs[qi];
			if (q[0] == q[1])
			{
				lcas[qi] = q[0];
				continue;
			}
			qis[q[0]].Add(qi);
			qis[q[1]].Add(qi);
		}

		Dfs(0);
		return string.Join("\n", lcas);
	}

	static int[][] map;
	static int[] lcas;
	static HashSet<int>[] qis;

	static HashSet<int> Dfs(int v)
	{
		var s = qis[v];
		foreach (var nv in map[v])
			s = Merge(s, Dfs(nv), v);
		return s;
	}

	static HashSet<int> Merge(HashSet<int> s1, HashSet<int> s2, int v)
	{
		if (s1.Count < s2.Count) { var t = s1; s1 = s2; s2 = t; }

		foreach (var qi in s2)
		{
			if (!s1.Add(qi))
			{
				s1.Remove(qi);
				lcas[qi] = v;
			}
		}
		return s1;
	}
}
