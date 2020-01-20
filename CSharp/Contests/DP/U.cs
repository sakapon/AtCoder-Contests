using System;
using System.Linq;

class U
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();
		var p2 = Enumerable.Range(0, n + 1).Select(i => 1 << i).ToArray();

		var dp = new long[n, p2[n]];
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				for (int f = 0; f < p2[n]; f++)
					if ((f & p2[i]) > 0 && (f & p2[j]) > 0)
						dp[0, f] += a[i][j];

		for (int i = 0, i2 = 1; i2 < n; i++, i2++)
			for (int f = 0; f < p2[n]; f++)
			{
				dp[i2, f] = long.MinValue;
				for (int g = 0; g <= f; g++)
					if ((f | g) == f)
						dp[i2, f] = Math.Max(dp[i2, f], dp[i, g] + dp[0, f - g]);
			}
		Console.WriteLine(dp[n - 1, p2[n] - 1]);
	}
}
