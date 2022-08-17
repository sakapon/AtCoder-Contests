using System;
using System.Collections.Generic;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, ec) = Read3();
		var es = Array.ConvertAll(new bool[ec], _ => Read());
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

		Array.ForEach(es, e =>
		{
			if (e[0] > n) e[0] = 0;
			if (e[1] > n) e[1] = 0;
		});

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());

		var qe = new bool[ec + 1];
		foreach (var ei in qs)
		{
			qe[ei] = true;
		}
		for (int ei = 1; ei <= ec; ei++)
		{
			if (qe[ei]) continue;
			var e = es[ei - 1];
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}

		var u = new bool[n + 1];
		var count = -1;
		Dfs(0);

		var r = new int[qc];
		for (int qi = qc - 1; qi >= 0; qi--)
		{
			r[qi] = count;

			var e = es[qs[qi] - 1];
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);

			if (u[e[0]] ^ u[e[1]])
			{
				if (u[e[0]])
				{
					Dfs(e[1]);
				}
				else
				{
					Dfs(e[0]);
				}
			}
		}
		return string.Join("\n", r);

		void Dfs(int v)
		{
			if (u[v]) return;
			u[v] = true;
			count++;
			foreach (var nv in map[v])
			{
				Dfs(nv);
			}
		}
	}
}
