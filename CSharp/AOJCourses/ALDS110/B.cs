using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var m = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var dp = new long[n, n + 1];
		for (int c = 2; c <= n; c++)
			for (int l = 0; l < n - c + 1; l++)
				for (int k = 1; k < c; k++)
				{
					var v = dp[l, k] + dp[l + k, c - k] + m[l][0] * m[l + k][0] * m[l + c - 1][1];
					dp[l, c] = dp[l, c] == 0 ? v : Math.Min(dp[l, c], v);
				}
		Console.WriteLine(dp[0, n]);
	}
}
