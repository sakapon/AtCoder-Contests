using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	const string chokudai = "chokudai";
	const long M = 1000000007;
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var dp = new long[chokudai.Length + 1];
		dp[0] = 1;

		foreach (var c in s)
		{
			var i = chokudai.IndexOf(c);
			if (i == -1) continue;

			dp[i + 1] += dp[i];
			dp[i + 1] %= M;
		}
		return dp[^1];
	}
}
