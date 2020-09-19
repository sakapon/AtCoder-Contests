using System;
using System.Collections.Generic;
using System.Linq;

class DD
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];

		var map = Array.ConvertAll(new int[n], _ => new List<int>());
		foreach (var e in new int[h[1]].Select(_ => Read()))
		{
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}

		var u = new bool[n];
		var g = new int[n];
		var gi = 0;

		Action<int> Dfs = null;
		Dfs = v =>
		{
			u[v] = true;
			g[v] = gi;
			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				Dfs(nv);
			}
		};

		for (int i = 0; i < n; i++)
		{
			if (u[i]) continue;
			++gi;
			Dfs(i);
		}

		var k = int.Parse(Console.ReadLine());
		Console.WriteLine(string.Join("\n", new int[k].Select(_ => Read()).Select(r => g[r[0]] == g[r[1]] ? "yes" : "no")));
	}
}
