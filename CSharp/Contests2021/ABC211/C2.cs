using System;

class C2
{
	const string chokudai = "chokudai";
	const long M = 1000000007;
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		var m = chokudai.Length;

		var dp = new long[n + 1, m + 1];
		dp[0, 0] = 1;

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j <= m; j++)
			{
				dp[i + 1, j] = dp[i, j];
			}

			var cj = chokudai.IndexOf(s[i]);
			if (cj == -1) continue;

			dp[i + 1, cj + 1] += dp[i, cj];
			dp[i + 1, cj + 1] %= M;
		}
		return dp[n, m];
	}
}
