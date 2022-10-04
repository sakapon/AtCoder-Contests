using System;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var a = Read();

		var r = 0L;

		for (int c = 1; c <= n; c++)
		{
			// dp[選んだ個数][和 mod 分母]
			var dp = new long[c + 1, c];
			dp[0, 0] = 1;

			for (int ai = 0; ai < n; ai++)
			{
				for (int i = c - 1; i >= 0; i--)
				{
					for (int j = 0; j < c; j++)
					{
						if (dp[i, j] == 0) continue;
						var nj = (j + a[ai]) % c;
						dp[i + 1, nj] += dp[i, j];
						dp[i + 1, nj] %= M;
					}
				}
			}
			r += dp[c, 0];
		}
		return r % M;
	}

	const long M = 998244353;
}
