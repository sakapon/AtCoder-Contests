using System;
using System.Collections.Generic;
using System.Linq;

class CD
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];

		var map = Array.ConvertAll(new int[n + 1], _ => new List<int>());
		foreach (var e in new int[h[1]].Select(_ => Read()))
		{
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}

		var u = new bool[n + 1];
		var g = new int[n + 1];
		var gi = 0;

		void Dfs(int v)
		{
			u[v] = true;
			g[v] = gi;
			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				Dfs(nv);
			}
		}

		for (int i = 1; i <= n; i++)
		{
			if (u[i]) continue;
			++gi;
			Dfs(i);
		}
		Console.WriteLine(g.Distinct().Count() - 2);
	}
}
