using System;
using System.Linq;

class E2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Select(c => c - 'A').ToArray();

		var dp = new long[n, 1 << 10, 10];

		for (int i = 0; i < n; i++)
		{
			dp[i, 1 << s[i], s[i]] = 1;
		}

		for (int i = 0; i < n - 1; i++)
		{
			var f = 1 << s[i + 1];
			var nk = s[i + 1];

			for (int x = 0; x < 1 << 10; x++)
			{
				if ((x & f) == 0)
				{
					for (int k = 0; k < 10; k++)
					{
						if (k != nk)
						{
							dp[i + 1, x | f, nk] += dp[i, x, k];
						}
					}
				}
				else
				{
					dp[i + 1, x, nk] += dp[i, x, nk];
				}
			}

			for (int x = 0; x < 1 << 10; x++)
			{
				for (int k = 0; k < 10; k++)
				{
					dp[i + 1, x, k] += dp[i, x, k];
					dp[i + 1, x, k] %= M;
				}
			}
		}

		var r = 0L;
		for (int x = 0; x < 1 << 10; x++)
		{
			for (int k = 0; k < 10; k++)
			{
				r += dp[n - 1, x, k];
			}
		}
		return r % M;
	}

	const long M = 998244353;
}
