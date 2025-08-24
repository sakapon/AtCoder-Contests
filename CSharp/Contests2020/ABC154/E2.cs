using System;

class E2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var k = int.Parse(Console.ReadLine());

		// dp0: 一致、dp1: 未満
		var dp0 = new long[k + 1];
		var dp1 = new long[k + 1];
		var dt0 = new long[k + 1];
		var dt1 = new long[k + 1];
		dp0[0] = 1;

		foreach (var c in s)
		{
			var v = c - '0';

			for (int j = 0; j <= k; j++)
			{
				dt1[j] += dp1[j];
				if (v == 0) dt0[j] += dp0[j];
				else dt1[j] += dp0[j];

				if (j == k) continue;

				dt1[j + 1] += dp1[j] * 9;
				if (v > 0)
				{
					dt0[j + 1] += dp0[j];
					dt1[j + 1] += dp0[j] * (v - 1);
				}
			}

			(dp0, dt0) = (dt0, dp0);
			(dp1, dt1) = (dt1, dp1);
			Array.Clear(dt0, 0, dt0.Length);
			Array.Clear(dt1, 0, dt1.Length);
		}

		return dp0[k] + dp1[k];
	}
}
