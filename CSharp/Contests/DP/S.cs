using System;

class S
{
	static void Main()
	{
		var M = 1000000007;
		var k = Console.ReadLine();
		var d = int.Parse(Console.ReadLine());
		var n = k.Length;

		var dp = new long[n + 1, d, 2];
		dp[0, 0, 0] = 1;

		for (int i = 1; i <= n; i++)
		{
			var c = k[i - 1] - '0';

			for (int x = 0; x <= 9; x++)
				for (int j = 0; j < d; j++)
				{
					dp[i, (j + x) % d, x == c ? 0 : 1] += x <= c ? dp[i - 1, j, 0] : 0;
					dp[i, (j + x) % d, 1] += dp[i - 1, j, 1];
				}

			for (int j = 0; j < d; j++)
				for (int f = 0; f < 2; f++)
					dp[i, j, f] %= M;
		}
		Console.WriteLine((dp[n, 0, 0] + dp[n, 0, 1] + M - 1) % M);
	}
}
