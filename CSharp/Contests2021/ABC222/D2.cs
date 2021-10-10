using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		var dp = NewArray2<long>(n + 1, 3000 + 1);
		dp[0][0] = 1;

		for (int i = 0; i < n; i++)
		{
			var av = a[i];
			var bv = b[i];

			for (int j = 0; j < bv; j++)
			{
				dp[i][j + 1] += dp[i][j];
			}

			for (int j = av; j <= bv; j++)
			{
				dp[i + 1][j] = dp[i][j] % M;
			}
		}

		return dp[n].Sum() % M;
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
