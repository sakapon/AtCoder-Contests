using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var a = read();
		var b = read();
		Console.WriteLine(Exchange(Exchange(n, a, b), b, a));
	}

	static long Exchange(long n, int[] a, int[] b)
	{
		var r = a.Zip(b, (p, q) => new[] { p, q }).Where(x => x[0] < x[1]).ToArray();
		var dp = new long[n + 1];
		for (int i = 1; i <= n; i++)
		{
			dp[i] = i;
			foreach (var x in r) if (x[0] <= i) dp[i] = Math.Max(dp[i], dp[i - x[0]] + x[1]);
		}
		return dp[n];
	}
}
