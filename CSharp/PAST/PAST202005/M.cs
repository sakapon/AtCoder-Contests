using System;
using System.Collections.Generic;
using System.Linq;

class M
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var rs = new int[m].Select(_ => Read()).ToArray();
		var s = int.Parse(Console.ReadLine());
		var k = int.Parse(Console.ReadLine());
		var t = Read();

		var map = UndirectedMap(n, rs);

		int[] Paths(int sp)
		{
			var u = Enumerable.Repeat(-1, n + 1).ToArray();
			var q = new Queue<int>();
			u[sp] = 0;
			q.Enqueue(sp);

			while (q.TryDequeue(out var p))
			{
				foreach (var np in map[p])
				{
					if (u[np] >= 0) continue;
					u[np] = u[p] + 1;
					q.Enqueue(np);
				}
			}
			return u;
		}
		var pathMap = t.Append(s).ToDictionary(x => x, Paths);

		var k2 = 1 << k;
		var dp = new int[k2, k];
		for (int j = 0; j < k; j++)
		{
			for (int i = 0; i < k2; i++)
				dp[i, j] = 1 << 30;
			dp[1 << j, j] = pathMap[s][t[j]];
		}

		for (int x = 0; x < k2; x++)
		{
			for (int j = 0; j < k; j++)
			{
				if (dp[x, j] == 1 << 30) continue;
				for (int i = 0; i < k; i++)
					dp[x | (1 << i), i] = Math.Min(dp[x | (1 << i), i], dp[x, j] + pathMap[t[i]][t[j]]);
			}
		}
		Console.WriteLine(Enumerable.Range(0, k).Min(j => dp[k2 - 1, j]));
	}

	static List<int>[] UndirectedMap(int n, int[][] rs)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<int>());
		foreach (var r in rs)
		{
			map[r[0]].Add(r[1]);
			map[r[1]].Add(r[0]);
		}
		return map;
	}
}
