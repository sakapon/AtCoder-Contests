using System;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var dp = new long[n + 1];
		var dt = new long[n + 1];
		dp[0] = 1;

		foreach (var c in s)
		{
			if (c == '(')
			{
				for (int j = 0; j < n; j++)
				{
					dt[j + 1] = dp[j];
				}
			}
			else if (c == ')')
			{
				for (int j = 0; j < n; j++)
				{
					dt[j] = dp[j + 1];
				}
			}
			else
			{
				for (int j = 0; j < n; j++)
				{
					dt[j + 1] += dp[j];
					dt[j] += dp[j + 1];
				}
			}

			for (int j = 0; j <= n; j++)
			{
				dt[j] %= M;
			}

			(dp, dt) = (dt, dp);
			Array.Clear(dt, 0, dt.Length);
		}

		return dp[0];
	}

	const long M = 998244353;
}
