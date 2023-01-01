using System;
using System.Collections.Generic;
using System.Linq;

class K
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var r = (Subsequence(s) + Subsequence(t) - CommonSubsequence(s, t) + M) % M;
		r = (r - 1 + M) % M;
		return r;
	}

	const long M = 998244353;

	// Number of Subsequence
	// 空文字列を含みます。
	public static long Subsequence(string s)
	{
		var all = 1L;
		// 最後が c である部分文字列の数
		var dp = new long[1 << 7];
		foreach (var c in s) (all, dp[c]) = ((all * 2 - dp[c] + M) % M, all);
		return all;
	}

	// Number of Common Subsequence
	// 空文字列を含みます。
	public static long CommonSubsequence(string s, string t, char c0 = 'a')
	{
		const int CharsCount = 26;
		var n = s.Length;
		var m = t.Length;

		var dp = new long[n + 1, m + 1, CharsCount + 1];
		for (int i = 0; i <= n; ++i) dp[i, 0, CharsCount] = 1;
		for (int j = 0; j <= m; ++j) dp[0, j, CharsCount] = 1;

		for (int i = 0; i < n; ++i)
			for (int j = 0; j < m; ++j)
			{
				if (s[i] == t[j])
				{
					for (int k = 0; k <= CharsCount; ++k)
						dp[i + 1, j + 1, k] = dp[i, j, k];

					var c = s[i] - c0;
					dp[i + 1, j + 1, CharsCount] = (dp[i, j, CharsCount] * 2 - dp[i, j, c] + M) % M;
					dp[i + 1, j + 1, c] = dp[i, j, CharsCount];
				}
				else
				{
					for (int k = 0; k <= CharsCount; ++k)
						dp[i + 1, j + 1, k] = (dp[i + 1, j, k] + dp[i, j + 1, k] - dp[i, j, k] + M) % M;
				}
			}
		return dp[n, m, CharsCount];
	}
}
