using System;
using System.Linq;
using static System.Math;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(long.Parse).Select((x, i) => (x, i)).OrderBy(_ => -_.x).ToArray();

		var dp = new long[n + 1, n + 1];

		for (int i = 0; i < n; i++)
			for (int j = 0; j <= i; j++)
			{
				dp[i + 1, j] = Max(dp[i + 1, j], dp[i, j] + a[i].x * Abs(n - 1 - i + j - a[i].i));
				dp[i + 1, j + 1] = Max(dp[i + 1, j + 1], dp[i, j] + a[i].x * Abs(a[i].i - j));
			}
		Console.WriteLine(Enumerable.Range(0, n + 1).Max(i => dp[n, i]));
	}
}
