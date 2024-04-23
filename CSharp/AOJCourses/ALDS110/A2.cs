using System;

class A2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		dp = new long[n + 1];
		Array.Fill(dp, -1);
		dp[1] = dp[0] = 1;
		Console.WriteLine(Rec(n));
	}

	// メモ化再帰
	static long[] dp;
	static long Rec(int n)
	{
		if (dp[n] != -1) return dp[n];
		return dp[n] = Rec(n - 1) + Rec(n - 2);
	}
}
