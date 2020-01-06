using System;

class F
{
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();
		int l = s.Length, m = t.Length;

		var dp = new int[l + 1, m + 1];
		for (int i = 1; i <= l; i++)
			for (int j = 1; j <= m; j++)
				if (s[i - 1] == t[j - 1])
					dp[i, j] = dp[i - 1, j - 1] + 1;
				else
					dp[i, j] = dp[i, j - 1] > dp[i - 1, j] ? dp[i, j - 1] : dp[i - 1, j];

		var cs = new char[dp[l, m]];
		for (int i = l, j = m; i > 0 && j > 0;)
			if (s[i - 1] == t[j - 1])
			{
				i--; j--;
				cs[dp[i, j]] = s[i];
			}
			else
			{
				if (dp[i, j - 1] > dp[i - 1, j]) j--;
				else i--;
			}
		Console.WriteLine(new string(cs));
	}
}
