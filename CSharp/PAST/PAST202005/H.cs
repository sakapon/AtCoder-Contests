using System;
using System.Linq;

class H
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], l = h[1];
		var x = Read().ToHashSet();
		var t = Read();

		var dp = Enumerable.Repeat(1 << 30, l).Prepend(0).ToArray();
		for (int i = 0; i < l; i++)
		{
			if (dp[i] == 1 << 30) continue;
			if (x.Contains(i)) dp[i] += t[2];

			dp[i + 1] = Math.Min(dp[i + 1], dp[i] + t[0]);

			if (i + 2 <= l)
				dp[i + 2] = Math.Min(dp[i + 2], dp[i] + t[0] + t[1]);
			else
				dp[l] = Math.Min(dp[l], dp[i] + (t[0] + t[1]) / 2);

			if (i + 4 <= l)
				dp[i + 4] = Math.Min(dp[i + 4], dp[i] + t[0] + 3 * t[1]);
			else
				dp[l] = Math.Min(dp[l], dp[i] + (t[0] + t[1]) / 2 + (l - i - 1) * t[1]);
		}
		Console.WriteLine(dp[l]);
	}
}
