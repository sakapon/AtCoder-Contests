using System;
using System.Linq;

class I
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);

		var dp = new double[n + 1, n + 1];
		dp[0, 0] = 1;
		for (int i = 0; i < n; i++) dp[i + 1, 0] = dp[i, 0] * (1 - p[i]);

		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
				dp[i + 1, j + 1] = dp[i, j] * p[i] + dp[i, j + 1] * (1 - p[i]);
		Console.WriteLine(Enumerable.Range((n + 1) / 2, (n + 1) / 2).Sum(i => dp[n, i]));
	}
}
