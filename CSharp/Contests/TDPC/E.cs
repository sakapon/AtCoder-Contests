using System;

class E
{
	static void Main()
	{
		var d = int.Parse(Console.ReadLine());
		var n = Console.ReadLine();

		var dp = new long[n.Length + 1, d, 2];
		dp[0, 0, 0] = 1;
		for (int i = 0; i < n.Length; i++)
		{
			var x = n[i] - '0';
			for (int j = 0; j < d; j++)
			{
				dp[i + 1, (j + x) % d, 0] += dp[i, j, 0];
				for (int k = 0; k < 10; k++)
					dp[i + 1, (j + k) % d, 1] += (k < x ? dp[i, j, 0] : 0) + dp[i, j, 1];

			}
			for (int j = 0; j < d; j++)
				dp[i + 1, j, 1] %= 1000000007;
		}
		Console.WriteLine(dp[n.Length, 0, 0] + dp[n.Length, 0, 1] - 1);
	}
}
