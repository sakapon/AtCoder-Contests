using System;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		var dp = new int[n + 1];
		dp[2] = Math.Abs(h[0] - h[1]);
		for (int i = 3; i <= n; i++)
			dp[i] = Math.Min(dp[i - 2] + Math.Abs(h[i - 3] - h[i - 1]), dp[i - 1] + Math.Abs(h[i - 2] - h[i - 1]));
		Console.WriteLine(dp[n]);
	}
}
