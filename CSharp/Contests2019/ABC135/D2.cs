using System;

class D2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var dp = new long[D];
		var dt = new long[D];
		dp[0] = 1;

		foreach (var c in s)
		{
			if (c == '?')
			{
				for (int j = 0; j < D; j++)
				{
					for (int x = 0; x < 10; x++)
					{
						var nj = (j * 10 + x) % D;
						dt[nj] += dp[j];
					}
				}
			}
			else
			{
				var v = c - '0';

				for (int j = 0; j < D; j++)
				{
					var nj = (j * 10 + v) % D;
					dt[nj] += dp[j];
				}
			}

			for (int j = 0; j < D; j++)
			{
				dt[j] %= M;
			}

			(dp, dt) = (dt, dp);
			Array.Clear(dt, 0, dt.Length);
		}

		return dp[5];
	}

	const int D = 13;
	const long M = 1000000007;
}
