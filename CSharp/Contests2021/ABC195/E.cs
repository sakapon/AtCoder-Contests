using System;

class E
{
	static void Main() => Console.WriteLine(Solve() ? "Takahashi" : "Aoki");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var x = Console.ReadLine();

		var dp = new bool[n + 1, 7];
		dp[n, 0] = true;

		for (int i = n - 1; i >= 0; i--)
		{
			if (x[i] == 'T')
			{
				for (int j = 0; j < 7; j++)
				{
					dp[i, j] = dp[i + 1, 10 * j % 7] || dp[i + 1, (10 * j + s[i] - '0') % 7];
				}
			}
			else
			{
				for (int j = 0; j < 7; j++)
				{
					dp[i, j] = dp[i + 1, 10 * j % 7] && dp[i + 1, (10 * j + s[i] - '0') % 7];
				}
			}
		}
		return dp[0, 0];
	}
}
