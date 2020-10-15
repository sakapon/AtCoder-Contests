using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var c = Read();

		var dp = new int[n + 10000];
		for (int i = 0; i < dp.Length; i++)
			dp[i] = 1 << 30;
		dp[0] = 0;

		foreach (var x in c)
			for (int i = 0; i < n; i++)
				dp[i + x] = Math.Min(dp[i + x], dp[i] + 1);
		Console.WriteLine(dp[n]);
	}
}
