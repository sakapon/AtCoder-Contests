using System;

class G
{
	static void Main()
	{
		Console.ReadLine();
		var s = Console.ReadLine().Replace(" ", "");
		var n = s.Length;

		var dp = new long[n + 1];
		for (int i = 0; i < n; i++)
			dp[i + 1] = (10 * dp[i] + (s[i] - '0')) % 1000000007;
		Console.WriteLine(dp[n]);
	}
}
