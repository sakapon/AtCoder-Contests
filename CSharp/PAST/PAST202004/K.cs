using System;
using System.Linq;
using static System.Math;

class K
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var c = Read();
		var d = Read();

		var dp = new long[n + 1, n + 1];
		for (int i = 0; i <= n; i++)
			for (int j = 0; j <= n; j++)
				dp[i, j] = 1L << 60;
		dp[0, 0] = 0;

		for (int i = 0; i < n; i++)
			for (int j = 0; j <= n; j++)
			{
				if (dp[i, j] == 1L << 60) continue;

				dp[i + 1, j] = Min(dp[i + 1, j], dp[i, j] + d[i]);
				if (s[i] == '(')
				{
					if (j < n) dp[i + 1, j + 1] = Min(dp[i + 1, j + 1], dp[i, j]);
					if (j > 0) dp[i + 1, j - 1] = Min(dp[i + 1, j - 1], dp[i, j] + c[i]);
				}
				else
				{
					if (j < n) dp[i + 1, j + 1] = Min(dp[i + 1, j + 1], dp[i, j] + c[i]);
					if (j > 0) dp[i + 1, j - 1] = Min(dp[i + 1, j - 1], dp[i, j]);
				}
			}
		Console.WriteLine(dp[n, 0]);
	}
}
