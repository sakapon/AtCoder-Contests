using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());

		foreach (var (u, v) in es)
		{
			map[u].Add(v);
			map[v].Add(u);
		}
		var degs = Array.ConvertAll(map, l => l.Count);

		var l = new List<int>();
		for (int v = 1; v <= n; v++)
		{
			if (degs[v] <= 2) continue;

			l.Add(degs[v]);

			foreach (var u in map[v])
			{
				if (degs[u] == 2)
				{
					foreach (var w in map[u])
					{
						degs[u]--;
						degs[w]--;
					}
				}
				else
				{
					degs[u]--;
					degs[v]--;
				}
			}
		}

		// 連結成分の個数
		var c = degs.Count(d => d == 1) / 2;
		// レベル 2 の個数
		c += (degs.Count(d => d == 2) - c) / 3;

		while (c-- > 0)
			l.Add(2);
		l.Sort();

		return string.Join(" ", l);
	}
}
