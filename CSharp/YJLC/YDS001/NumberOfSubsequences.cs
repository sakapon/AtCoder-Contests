using System;
using System.Collections.Generic;
using System.Linq;

// dp[x]: 最後の項が x である部分列の個数
class NumberOfSubsequences
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var dp = new Dictionary<int, long>();
		dp[-1] = 1;

		foreach (var x in a)
		{
			var all = dp[-1];
			var v = dp.GetValueOrDefault(x);

			dp[x] = all;
			dp[-1] = (all * 2 - v + M) % M;
		}
		return (dp[-1] - 1 + M) % M;
	}

	const long M = 998244353;
}
