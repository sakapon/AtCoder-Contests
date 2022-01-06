using System;

class H2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();
		var n = s.Length;
		var m = t.Length;

		var dp = NewArray2<int>(n + 1, m + 1);

		for (int i = 1; i <= n; i++)
		{
			for (int j = 1; j <= m; j++)
			{
				var nv = dp[i - 1][j - 1];
				if (s[i - 1] != t[j - 1]) nv++;

				dp[i][j] = Math.Max(dp[i - 1][j], dp[i][j - 1]);
				dp[i][j] = Math.Max(dp[i][j], nv);
			}
		}

		return dp[n][m];
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
