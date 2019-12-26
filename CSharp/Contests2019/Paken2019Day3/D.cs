using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new int[5].Select(_ => Console.ReadLine()).ToArray();

		var d = Enumerable.Range(0, n).Select(j => "BWR".Select(c => Enumerable.Range(0, 5).Count(i => s[i][j] != c)).ToArray()).ToArray();

		var dp = new int[n + 1, 3];
		for (int j = 0; j < n; j++)
			for (int k = 0; k < 3; k++)
				dp[j + 1, k] = Math.Min(dp[j, (k + 1) % 3], dp[j, (k + 2) % 3]) + d[j][k];
		Console.WriteLine(Enumerable.Range(0, 3).Min(k => dp[n, k]));
	}
}
