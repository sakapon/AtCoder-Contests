using System;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var dp = new int[n + 1, n + 1];
		var M = 0;
		for (var j = n - 1; j >= 0; j--)
			for (var i = j - 1; i >= 0; i--)
				if (s[i] == s[j]) M = Math.Max(M, dp[i, j] = Math.Min(dp[i + 1, j + 1] + 1, j - i));
		Console.WriteLine(M);
	}
}
