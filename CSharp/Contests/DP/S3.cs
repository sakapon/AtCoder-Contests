using System;

class S3
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = Console.ReadLine();
		var d = int.Parse(Console.ReadLine());

		// s0: 一致、dp: 未満
		var s0 = 0;
		var dp = new long[d];
		var dt = new long[d];

		foreach (var t in k)
		{
			var c = t - '0';

			for (int j = 0; j < d; j++)
			{
				for (int x = 0; x < 10; x++)
				{
					var nj = (j + x) % d;
					dt[nj] += dp[j];
					if (s0 == j && x < c) dt[nj]++;
				}
			}

			for (int j = 0; j < d; j++)
			{
				dt[j] %= M;
			}

			s0 += c;
			s0 %= d;

			(dp, dt) = (dt, dp);
			Array.Clear(dt, 0, dt.Length);
		}

		var r = dp[0] - 1;
		if (s0 == 0) r++;
		return (r + M) % M;
	}

	const long M = 1000000007;
}
