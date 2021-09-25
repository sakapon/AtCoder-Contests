using System;
using System.Linq;

class F
{
	const long M = 998244353;
	const int max = 5000;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		(int a, int b)[] ab = a.Zip(b).OrderBy(_ => -_.First).ToArray();

		var dp = new long[max + 1];

		for (int i = 0; i < n; i++)
		{
			var (s, t) = ab[i];

			for (int j = 0; j <= max; j++)
			{
				var nj = j - t;
				if (nj >= 0)
				{
					dp[nj] += dp[j];
					dp[nj] %= M;
				}
			}

			var ns = s - t;
			if (ns >= 0)
			{
				dp[ns]++;
				dp[ns] %= M;
			}
		}

		return dp.Sum() % M;
	}
}
