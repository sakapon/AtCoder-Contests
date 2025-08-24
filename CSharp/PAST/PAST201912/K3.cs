using System;
using System.Collections.Generic;
using System.Linq;

class K3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));
		var qc = int.Parse(Console.ReadLine());
		var r = new bool[qc];
		var qs = Array.ConvertAll(r, _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		var root = 0;

		for (int i = 0; i < n; i++)
		{
			if (p[i] == -1)
				root = i + 1;
			else
				map[p[i]].Add(i + 1);
		}

		var qimap = Array.ConvertAll(map, _ => new List<int>());
		for (int qi = 0; qi < qc; qi++)
			qimap[qs[qi].b].Add(qi);

		var sets = Array.ConvertAll(map, _ => new HashSet<int>());
		DFS(root);
		return string.Join("\n", r.Select(b => b ? "Yes" : "No"));

		void DFS(int v)
		{
			foreach (var qi in sets[v])
				r[qi] = true;

			foreach (var qi in qimap[v])
				sets[qs[qi].a].Add(qi);

			foreach (var nv in map[v])
				DFS(nv);

			foreach (var qi in qimap[v])
				sets[qs[qi].a].Remove(qi);
		}
	}
}
