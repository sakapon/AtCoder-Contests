using System;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		var h = read();
		var s = read();
		var t = read();
		int n = h[0], m = h[1];

		var dp = new int[n + 1, m + 1];
		for (int i = 0; i <= n; i++) dp[i, 0] = 1;
		for (int j = 0; j <= m; j++) dp[0, j] = 1;
		for (int i = 0; i < n; i++)
			for (int j = 0; j < m; j++)
				dp[i + 1, j + 1] = (dp[i, j + 1] + dp[i + 1, j] - (s[i] == t[j] ? 0 : dp[i, j])) % 1000000007;
		Console.WriteLine(dp[n, m]);
	}
}
