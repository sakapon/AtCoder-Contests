using System;
using System.Linq;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Select(c => c - 'A').ToArray();

		var dp = NewArray3<long>(n + 1, 1 << 10, 10);

		for (int i = 0; i < n; i++)
		{
			dp[i + 1][1 << s[i]][s[i]] = 1;
		}

		for (int i = 1; i < n; i++)
		{
			for (int j = 0; j < 1 << 10; j++)
			{
				for (int k = 0; k < 10; k++)
				{
					dp[i + 1][j][k] += dp[i][j][k];
				}
			}

			for (int j = 0; j < 1 << 10; j++)
			{
				for (int k = 0; k < 10; k++)
				{
					if (k == s[i])
					{
						dp[i + 1][j][k] += dp[i][j][k];
					}
					else
					{
						if ((j & (1 << s[i])) == 0)
						{
							dp[i + 1][j | (1 << s[i])][s[i]] += dp[i][j][k];
						}
					}
				}
			}

			for (int j = 0; j < 1 << 10; j++)
			{
				for (int k = 0; k < 10; k++)
				{
					dp[i + 1][j][k] %= M;
				}
			}
		}

		return dp[n].Sum(a => a.Sum()) % M;
	}

	const long M = 998244353;
	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
