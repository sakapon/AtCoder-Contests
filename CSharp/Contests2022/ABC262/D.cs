using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		// dp[分母][選んだ個数][和 mod 分母]
		var dp = new long[n + 1, n + 1, n];

		for (int i = 1; i <= n; i++)
		{
			dp[i, 0, 0] = 1;
		}

		for (int ai = 0; ai < n; ai++)
		{
			for (int i = 1; i <= n; i++)
			{
				for (int j = i - 1; j >= 0; j--)
				{
					for (int k = 0; k < i; k++)
					{
						var nk = (k + a[ai]) % i;
						dp[i, j + 1, nk] += dp[i, j, k];
						dp[i, j + 1, nk] %= M;
					}
				}
			}
		}
		return Enumerable.Range(1, n).Sum(i => dp[i, i, 0]) % M;
	}

	const long M = 998244353;
}
