using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var a = read();
		var h = read();

		var dp = Enumerable.Repeat(int.MaxValue, a[0] + 1).ToArray();
		dp[1] = 0;
		for (int i = 2; i <= a[0]; i++)
			for (int j = Math.Min(a[1], i - 1); j > 0; j--)
				dp[i] = Math.Min(dp[i], dp[i - j] + Math.Abs(h[i - j - 1] - h[i - 1]));
		Console.WriteLine(dp[a[0]]);
	}
}
