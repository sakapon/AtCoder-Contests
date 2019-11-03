using System;

class E
{
	static void Main()
	{
		var d = int.Parse(Console.ReadLine());
		var n = Console.ReadLine();
		var l = n.Length;

		var dp = new long[l + 1, d, 2];
		dp[0, 0, 0] = 1;
		for (int i = 0; i < l; i++)
		{
			var x = n[i] - '0';
			for (int j = 0; j < d; j++)
			{
				dp[i + 1, (j + x) % d, 0] = dp[i, j, 0];
				for (int k = 0; k < 10; k++)
					dp[i + 1, (j + k) % d, 1] = (dp[i + 1, (j + k) % d, 1] + (k < x ? dp[i, j, 0] : 0) + dp[i, j, 1]) % 1000000007;
			}
		}
		Console.WriteLine(dp[l, 0, 0] + dp[l, 0, 1] - 1);
	}
}
