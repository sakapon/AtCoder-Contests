using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new int[5].Select(_ => Console.ReadLine()).ToArray();

		var d = new int[n + 1, 3];
		for (int i = 0; i < n; i++)
		{
			d[i + 1, 0] = Enumerable.Range(0, 5).Count(j => s[j][i] != 'B');
			d[i + 1, 1] = Enumerable.Range(0, 5).Count(j => s[j][i] != 'W');
			d[i + 1, 2] = Enumerable.Range(0, 5).Count(j => s[j][i] != 'R');
		}

		var dp = new int[n + 1, 3];
		for (int i = 1; i <= n; i++)
			for (int k = 0; k < 3; k++)
				dp[i, k] = Math.Min(dp[i - 1, (k + 1) % 3], dp[i - 1, (k + 2) % 3]) + d[i, k];
		Console.WriteLine(Enumerable.Range(0, 3).Min(k => dp[n, k]));
	}
}
