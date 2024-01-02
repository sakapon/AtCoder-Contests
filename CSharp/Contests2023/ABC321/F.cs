using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (qc, k) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine().Split());

		var r = new List<long>();
		var dp = new long[k + 1];
		dp[0] = 1;

		foreach (var q in qs)
		{
			var x = int.Parse(q[1]);

			if (q[0] == "+")
			{
				for (int i = k - 1; i >= 0; i--)
				{
					if (dp[i] == 0) continue;
					var ni = i + x;
					if (ni > k) continue;
					dp[ni] += dp[i];
					dp[ni] %= M;
				}
			}
			else
			{
				for (int i = 0; i < k; i++)
				{
					if (dp[i] == 0) continue;
					var ni = i + x;
					if (ni > k) continue;
					dp[ni] += M - dp[i];
					dp[ni] %= M;
				}
			}

			r.Add(dp[k]);
		}

		return string.Join("\n", r);
	}

	const long M = 998244353;
}
