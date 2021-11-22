using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		if (dp == null) dp = Init();
		return dp[n];
	}

	static long[] dp;
	static long[] Init()
	{
		var n = 1000000;

		// 回文
		var p = new long[n + 1];
		p[0] = 1;
		p[1] = 26;
		for (int i = 2; i <= n; i++)
		{
			p[i] = p[i - 2] * 26 % M;
		}

		var dp = new long[n + 1];
		dp[2] = 25 * 26;

		for (int i = 3; i <= n; i++)
		{
			dp[i] = dp[i - 2] * 26 + p[i - 3] * 25 * 26 * 2;
			if (i % 2 == 0) dp[i] += M - 25 * 26;
			dp[i] %= M;
		}
		return dp;
	}

	const long M = 998244353;
}
