using System;
using System.Collections.Generic;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var prev = new int[n + 1];
		prev[0] = -1;
		var s = 0L;
		var imap = new Dictionary<long, int>();
		imap[0] = 0;

		for (int i = 1; i <= n; i++)
		{
			s += a[i - 1];
			prev[i] = imap.ContainsKey(s) ? imap[s] : -1;
			imap[s] = i;
		}

		var dp = new long[n + 1];
		dp[1] = 1;

		for (int i = 2; i <= n; i++)
		{
			var pi = prev[i - 1];
			var dup = pi < 0 ? 0 : dp[pi];
			dp[i] = (dp[i - 1] * 2 - dup + M) % M;
		}

		return dp[n];
	}

	const long M = 998244353;
}
