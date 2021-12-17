using System;
using System.Linq;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Select(c => c - 'A').ToArray();

		var dp = NewArray3<long>(n, 1 << 10, 10);

		for (int i = 0; i < n; i++)
		{
			dp[i][1 << s[i]][s[i]] = 1;
		}

		for (int i = 0; i < n - 1; i++)
		{
			var f = 1 << s[i + 1];
			var nk = s[i + 1];

			for (int x = 0; x < 1 << 10; x++)
			{
				for (int k = 0; k < 10; k++)
				{
					dp[i + 1][x][k] += dp[i][x][k];

					if (k == nk)
					{
						dp[i + 1][x][k] += dp[i][x][k];
					}
					else
					{
						if ((x & f) == 0)
						{
							dp[i + 1][x | f][nk] += dp[i][x][k];
						}
					}
				}
			}

			for (int x = 0; x < 1 << 10; x++)
			{
				for (int k = 0; k < 10; k++)
				{
					dp[i + 1][x][k] %= M;
				}
			}
		}

		return dp[^1].Sum(a => a.Sum()) % M;
	}

	const long M = 998244353;
	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
