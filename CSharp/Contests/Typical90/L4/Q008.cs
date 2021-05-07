using System;

class Q008
{
	const string AtCoder = "atcoder";
	const long M = 1000000007;
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var dp = new long[AtCoder.Length + 1];
		dp[0] = 1;

		foreach (var c in s)
		{
			var i = AtCoder.IndexOf(c);
			if (i == -1) continue;

			dp[i + 1] += dp[i];
			dp[i + 1] %= M;
		}
		return dp[^1];
	}
}
