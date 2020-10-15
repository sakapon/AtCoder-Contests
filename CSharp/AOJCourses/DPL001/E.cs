using System;

class E
{
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();
		int n = s.Length, m = t.Length;

		var dp = new int[n + 1, m + 1];
		for (int i = 0; i <= n; i++) dp[i, 0] = i;
		for (int j = 0; j <= m; j++) dp[0, j] = j;

		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= m; j++)
			{
				dp[i, j] = dp[i - 1, j - 1] + (s[i - 1] == t[j - 1] ? 0 : 1);
				dp[i, j] = Math.Min(dp[i, j], dp[i - 1, j] + 1);
				dp[i, j] = Math.Min(dp[i, j], dp[i, j - 1] + 1);
			}
		Console.WriteLine(dp[n, m]);
	}
}
