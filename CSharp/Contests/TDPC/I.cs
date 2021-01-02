using System;

class I
{
	static void Main()
	{
		var iwi = "iwi";
		var s = Console.ReadLine();
		var n = s.Length;

		var dp = new int[n + 1, n + 1];
		for (int d = 3; d <= n; d++)
			for (int l = 0, r = d; r <= n; l++, r++)
			{
				dp[l, r] = Math.Max(dp[l + 1, r], dp[l, r - 1]);
				if (s[l] != iwi[0] || s[r - 1] != iwi[2]) continue;

				for (int m = l + 1; m < r - 1; m++)
				{
					dp[l, r] = Math.Max(dp[l, r], dp[l, m] + dp[m, r]);
					if (s[m] == iwi[1] && 3 * dp[l + 1, m] == m - l - 1 && 3 * dp[m + 1, r - 1] == r - m - 2)
						dp[l, r] = d / 3;
				}
			}
		Console.WriteLine(dp[0, n]);
	}
}
