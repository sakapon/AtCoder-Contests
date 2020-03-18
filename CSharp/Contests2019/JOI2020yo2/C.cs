using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var dp = new int[n + 1];
		for (int d = 0, i = 1, j; i < n; i++)
		{
			if (i % 10 == 0) d = i.ToString().Sum(c => c - '0');
			else d++;

			if ((j = i + d) <= n) dp[j] += dp[i] + 1;
		}
		Console.WriteLine(dp[n] + 1);
	}
}
