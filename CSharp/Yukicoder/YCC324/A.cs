using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		if (dp == null) dp = Init();
		return dp[n][0];
	}

	static long[][] dp;
	static long[][] Init()
	{
		var n = 100;

		var dp = NewArray2<long>(n + 1, 3);
		dp[0][0] = 1;

		for (int i = 0; i < n; i++)
		{
			dp[i + 1][0] += dp[i][0] * 2;
			dp[i + 1][1] += dp[i][0];
			dp[i + 1][2] += dp[i][0];

			dp[i + 1][0] += dp[i][1];
			dp[i + 1][1] += dp[i][1];

			dp[i + 1][0] += dp[i][2];
			dp[i + 1][2] += dp[i][2];

			dp[i + 1][0] %= M;
			dp[i + 1][1] %= M;
			dp[i + 1][2] %= M;
		}
		return dp;
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
