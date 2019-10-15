using System;

class F
{
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var dp = new string[s.Length + 1, t.Length + 1];
		for (int i = 0; i <= s.Length; i++) dp[i, 0] = "";
		for (int j = 0; j <= t.Length; j++) dp[0, j] = "";
		for (int i = 1; i <= s.Length; i++)
			for (int j = 1; j <= t.Length; j++)
				if (s[i - 1] == t[j - 1])
					dp[i, j] = dp[i - 1, j - 1] + s[i - 1];
				else
					dp[i, j] = dp[i, j - 1].Length > dp[i - 1, j].Length ? dp[i, j - 1] : dp[i - 1, j];
		Console.WriteLine(dp[s.Length, t.Length]);
	}
}
