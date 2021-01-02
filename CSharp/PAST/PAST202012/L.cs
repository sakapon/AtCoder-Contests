using System;

class L
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		if (t[0] != t[1] && t[0] == t[2])
		{
			if (!s.Contains(t)) return 0;

			var dp = NewArray2<int>(n + 1, n + 1);

			for (int d = 3; d <= n; d++)
			{
				for (int l = 0, r = d; r <= n; l++, r++)
				{
					dp[l][r] = Math.Max(dp[l + 1][r], dp[l][r - 1]);

					if (s[l] != t[0] || s[r - 1] != t[2]) continue;
					for (int m = l + 1; m < r; m++)
						dp[l][r] = Math.Max(dp[l][r], dp[l][m] + dp[m][r]);

					if (d % 3 != 0) continue;
					for (int m = l + 1; m < r - 1; m++)
					{
						if (s[m] == t[1] && 3 * dp[l + 1][m] == m - l - 1 && 3 * dp[m + 1][r - 1] == r - m - 2)
						{
							dp[l][r] = d / 3;
							break;
						}
					}
				}
			}
			return dp[0][n];
		}
		else
		{
			for (int i; (i = s.IndexOf(t)) != -1;)
				s = s.Remove(i, 3);
			return (n - s.Length) / 3;
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
