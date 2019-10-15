using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var h = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var dp = new int[n + 1, 3];
		for (int i = 1; i <= n; i++)
		{
			dp[i, 0] = Math.Max(dp[i - 1, 1] + h[i - 1][1], dp[i - 1, 2] + h[i - 1][2]);
			dp[i, 1] = Math.Max(dp[i - 1, 2] + h[i - 1][2], dp[i - 1, 0] + h[i - 1][0]);
			dp[i, 2] = Math.Max(dp[i - 1, 0] + h[i - 1][0], dp[i - 1, 1] + h[i - 1][1]);
		}
		Console.WriteLine(Math.Max(Math.Max(dp[n, 0], dp[n, 1]), dp[n, 2]));
	}
}
