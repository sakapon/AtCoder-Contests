using System;

class S2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = Console.ReadLine();
		var d = int.Parse(Console.ReadLine());

		// dp0: 一致、dp1: 未満
		var dp0 = new long[d];
		var dp1 = new long[d];
		var dt0 = new long[d];
		var dt1 = new long[d];
		dp0[0] = 1;

		for (int i = 0; i < k.Length; i++)
		{
			var c = k[i] - '0';

			for (int j = 0; j < d; j++)
			{
				for (int x = 0; x < 10; x++)
				{
					var nj = (j + x) % d;
					dt1[nj] += dp1[j];

					if (x < c)
						dt1[nj] += dp0[j];
					else if (x == c)
						dt0[nj] += dp0[j];
				}
			}

			for (int j = 0; j < d; j++)
			{
				dt0[j] %= M;
				dt1[j] %= M;
			}

			(dp0, dt0) = (dt0, dp0);
			(dp1, dt1) = (dt1, dp1);
			Array.Clear(dt0, 0, dt0.Length);
			Array.Clear(dt1, 0, dt1.Length);
		}

		return (dp0[0] + dp1[0] - 1 + M) % M;
	}

	const long M = 1000000007;
}
