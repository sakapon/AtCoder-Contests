using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		if (n % 2 == 0)
		{
			var dp = new long[n + 5, 2];
			for (int i = 0; i <= n; i++)
				for (int j = 0; j < 2; j++)
					dp[i, j] = long.MinValue;
			dp[1, 1] = a[0];
			dp[2, 0] = a[1];

			for (int i = 0; i < n; i++)
				for (int j = 0; j < 2; j++)
				{
					if (dp[i, j] == long.MinValue) continue;
					if (i + 1 < n)
						dp[i + 2, j] = Math.Max(dp[i + 2, j], dp[i, j] + a[i + 1]);
					if (j > 0 && i + 2 < n)
						dp[i + 3, j - 1] = Math.Max(dp[i + 3, j - 1], dp[i, j] + a[i + 2]);
				}
			Console.WriteLine(Math.Max(dp[n - 1, 1], dp[n, 0]));
		}
		else
		{
			var dp = new long[n + 5, 3];
			for (int i = 0; i <= n; i++)
				for (int j = 0; j < 3; j++)
					dp[i, j] = long.MinValue;
			dp[1, 2] = a[0];
			dp[2, 1] = a[1];
			dp[3, 0] = a[2];

			for (int i = 0; i < n; i++)
				for (int j = 0; j < 3; j++)
				{
					if (dp[i, j] == long.MinValue) continue;
					if (i + 1 < n)
						dp[i + 2, j] = Math.Max(dp[i + 2, j], dp[i, j] + a[i + 1]);
					if (j > 0 && i + 2 < n)
						dp[i + 3, j - 1] = Math.Max(dp[i + 3, j - 1], dp[i, j] + a[i + 2]);
					if (j > 1 && i + 3 < n)
						dp[i + 4, j - 2] = Math.Max(dp[i + 4, j - 2], dp[i, j] + a[i + 3]);
				}
			Console.WriteLine(Math.Max(dp[n - 2, 2], Math.Max(dp[n - 1, 1], dp[n, 0])));
		}
	}
}
