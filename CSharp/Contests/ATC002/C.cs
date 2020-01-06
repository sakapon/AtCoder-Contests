using System;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var w = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		var s = new int[n + 1];
		for (int i = 0; i < n; i++)
			s[i + 1] = s[i] + w[i];

		var dp = new long[n, n];
		for (int k = 1; k < n; k++)
			for (int i = 0, j = k; j < n; i++, j++)
			{
				var m = long.MaxValue;
				for (int t = 0; t < k; t++)
					m = Math.Min(m, dp[i, i + t] + dp[i + 1 + t, j]);
				dp[i, j] = m + s[j + 1] - s[i];
			}
		Console.WriteLine(dp[0, n - 1]);
	}
}
