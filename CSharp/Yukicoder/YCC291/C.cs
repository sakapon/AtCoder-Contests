using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var b = Read();

		var dp = new double[m];
		dp[^1] = n - b[^1];
		var sum = m + dp[^1];

		for (int i = m - 2; i >= 0; i--)
		{
			dp[i] = Math.Min(dp[i + 1] + b[i + 1] - b[i], sum / (m - i - 1));
			sum += dp[i];
		}
		Console.WriteLine(dp[0] + b[0] - 1);
	}
}
