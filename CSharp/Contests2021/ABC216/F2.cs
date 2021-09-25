using System;
using System.Linq;

class F2
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

		var r = 0L;
		var dp = new long[max + 1];
		dp[0] = 1;

		foreach (var (av, bv) in a.Zip(b).OrderBy(_ => _.First))
		{
			for (int s = max; s >= 0; s--)
			{
				var ns = s + bv;
				if (ns <= max)
				{
					dp[ns] += dp[s];
					dp[ns] %= M;
				}

				if (av >= ns)
				{
					r += dp[s];
					r %= M;
				}
			}
		}

		return r;
	}
}
