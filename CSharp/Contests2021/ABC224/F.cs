using System;
using System.Linq;

class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c - '0').ToArray();
		var n = s.Length;

		var dp = NewArray2<long>(n, 2);
		dp[0][0] = s[0];

		var p2 = 1L;

		for (int i = 1; i < n; i++)
		{
			p2 *= 2;
			p2 %= M;

			dp[i][0] = dp[i - 1][0] * 10;
			dp[i][0] += s[i] * p2;
			dp[i][0] %= M;

			dp[i][1] = dp[i - 1][0] + dp[i - 1][1] * 2;
			dp[i][1] %= M;
		}
		return dp[^1].Sum() % M;
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
