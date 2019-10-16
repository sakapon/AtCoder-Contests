using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var h = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var dp = new int[n + 1, 3];
		for (int i = 0; i < n; i++)
			for (int j = 0; j < 3; j++)
				dp[i + 1, (j + 2) % 3] = Math.Max(dp[i, j] + h[i][j], dp[i, (j + 1) % 3] + h[i][(j + 1) % 3]);
		Console.WriteLine(Enumerable.Range(0, 3).Max(i => dp[n, i]));
	}
}
