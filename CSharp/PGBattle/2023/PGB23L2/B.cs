using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var r = 0D;

		// 0: なし
		// 1: A
		// 2: AB
		var dp = new double[3];
		var dt = new double[3];
		dp[0] = 1;

		foreach (var c in s)
		{
			if (c == 'A')
			{
				dt[1] += dp[0];
				dt[1] += dp[1];
				dt[1] += dp[2];
			}
			else if (c == 'B')
			{
				dt[0] += dp[0];
				dt[2] += dp[1];
				dt[0] += dp[2];
			}
			else if (c == 'C')
			{
				r += dp[2];
				dt[0] += dp[0];
				dt[0] += dp[1];
				dt[0] += dp[2];
			}
			else
			{
				{
					dt[1] += dp[0] / 3;
					dt[1] += dp[1] / 3;
					dt[1] += dp[2] / 3;
				}
				{
					dt[0] += dp[0] / 3;
					dt[2] += dp[1] / 3;
					dt[0] += dp[2] / 3;
				}
				{
					r += dp[2] / 3;
					dt[0] += dp[0] / 3;
					dt[0] += dp[1] / 3;
					dt[0] += dp[2] / 3;
				}
			}

			(dp, dt) = (dt, dp);
			Array.Clear(dt, 0, dt.Length);
		}

		return r;
	}
}
