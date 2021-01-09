using System;

class L2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var dp = new int[n + 1, n + 1];
		for (int d = 3; d <= n; d++)
			for (int l = 0, r = d; r <= n; l++, r++)
			{
				dp[l, r] = Math.Max(dp[l + 1, r], dp[l, r - 1]);
				if (s[l] != t[0] || s[r - 1] != t[2]) continue;

				for (int m = l + 1; m < r - 1; m++)
				{
					dp[l, r] = Math.Max(dp[l, r], dp[l, m] + dp[m, r]);
					if (s[m] == t[1] && 3 * dp[l + 1, m] == m - l - 1 && 3 * dp[m + 1, r - 1] == r - m - 2)
						dp[l, r] = d / 3;
				}
			}
		Console.WriteLine(dp[0, n]);
	}
}
