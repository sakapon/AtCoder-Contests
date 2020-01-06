using System;
using System.Collections.Generic;
using System.Linq;

class P
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var map = new int[n + 1].Select(_ => new List<int>()).ToArray();
		foreach (var r in new int[n - 1].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()))
		{
			map[r[0]].Add(r[1]);
			map[r[1]].Add(r[0]);
		}

		var dp = new long[n + 1, 2];
		Find(1, 0, map, dp);
		Console.WriteLine((dp[1, 0] + dp[1, 1]) % M);
	}

	const int M = 1000000007;
	static void Find(int p, int from, List<int>[] map, long[,] dp)
	{
		dp[p, 0] = dp[p, 1] = 1;
		foreach (var np in map[p])
		{
			if (np == from) continue;
			Find(np, p, map, dp);
			dp[p, 0] = dp[p, 0] * (dp[np, 0] + dp[np, 1]) % M;
			dp[p, 1] = dp[p, 1] * dp[np, 0] % M;
		}
	}
}
