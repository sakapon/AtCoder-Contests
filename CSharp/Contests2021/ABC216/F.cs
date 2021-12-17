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

		var dp = new long[max + 1];

		foreach (var (av, bv) in a.Zip(b).OrderBy(_ => -_.First))
		{
			for (int d = 0; d <= max; d++)
			{
				var nd = d - bv;
				if (nd >= 0)
				{
					dp[nd] += dp[d];
					dp[nd] %= M;
				}
			}

			var di = av - bv;
			if (di >= 0)
			{
				dp[di]++;
				dp[di] %= M;
			}
		}

		return dp.Sum() % M;
	}
}
