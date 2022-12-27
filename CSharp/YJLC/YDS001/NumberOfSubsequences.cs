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

		var all = 1L;
		var dp = new Dictionary<int, long>();

		foreach (var x in a)
		{
			dp.TryGetValue(x, out var v);
			dp[x] = all;
			all = (all * 2 - v + M) % M;
		}
		return (all - 1 + M) % M;
	}

	const long M = 998244353;
}
