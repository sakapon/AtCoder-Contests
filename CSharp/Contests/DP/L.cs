using System;

class L
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		var dp = new long[n, n];
		for (int i = 0; i < n; i++) dp[i, i] = a[i];

		for (int i = 1; i < n; i++)
			for (int l = 0; l + i < n; l++)
				dp[l, l + i] = Math.Max(a[l] - dp[l + 1, l + i], a[l + i] - dp[l, l + i - 1]);
		Console.WriteLine(dp[0, n - 1]);
	}
}
