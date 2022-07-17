using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var nm = n * m;

		// i までの和が j である個数
		var dp = NewArray2<long>(n + 1, nm + 1);
		dp[0][0] = 1;

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < nm; j++)
			{
				if (dp[i][j] == 0) continue;

				for (int d = 1; d <= m; d++)
				{
					dp[i + 1][j + d] += dp[i][j];
					dp[i + 1][j + d] %= M;
				}
			}
		}

		return dp[n][..(k + 1)].Sum() % M;
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
