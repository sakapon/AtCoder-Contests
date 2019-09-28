using System;

class E
{
	static void Main()
	{
		var M = 1000000007;
		var l = Console.ReadLine();
		var n = l.Length;

		var dp = new long[n + 1, 2];
		dp[0, 0] = 1;
		for (var i = 0; i < n; i++)
		{
			var f = l[i] == '1';
			dp[i + 1, 0] = (f ? 2 : 1) * dp[i, 0] % M;
			dp[i + 1, 1] = ((f ? dp[i, 0] : 0) + 3 * dp[i, 1]) % M;
		}
		Console.WriteLine((dp[n, 0] + dp[n, 1]) % M);
	}
}
