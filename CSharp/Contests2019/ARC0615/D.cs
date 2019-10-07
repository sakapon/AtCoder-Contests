using System;

class D
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		Func<int[]> read = () => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		var a = read();
		var b = read();
		Console.WriteLine(Exchange(Exchange(n, a, b), b, a));
	}

	static long Exchange(long n, int[] a, int[] b)
	{
		var dp = new long[n + 1];
		for (int i = 1; i <= n; i++)
		{
			dp[i] = i;
			for (int j = 0; j < 3; j++) if (a[j] <= i) dp[i] = Math.Max(dp[i], dp[i - a[j]] + b[j]);
		}
		return dp[n];
	}
}
