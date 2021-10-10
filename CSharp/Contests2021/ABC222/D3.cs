using System;
using System.Linq;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		var dp = new long[max + 1];
		dp[0] = 1;

		for (int i = 0; i < n; i++)
		{
			var av = a[i];
			var bv = b[i];

			for (int j = 0; j < bv; j++)
			{
				dp[j + 1] += dp[j];
				dp[j + 1] %= M;

				if (j < av) dp[j] = 0;
			}
		}

		return dp.Sum() % M;
	}

	const int max = 3000;
	const long M = 998244353;
}
