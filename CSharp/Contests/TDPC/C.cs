using System;
using System.Linq;

class C
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());
		var n = (int)Math.Pow(2, k);
		var r = new int[n].Select(_ => int.Parse(Console.ReadLine())).ToArray();

		var p = new double[n, n];
		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
				p[i, j] = 1 / (1 + Math.Pow(10, (r[j] - r[i]) / 400.0));

		var dp = new double[k + 1, n];
		for (int j = 0; j < n; j++) dp[0, j] = 1;

		for (int i = 1, i2 = 2; i <= k; i++, i2 *= 2)
			for (int g = 0; g < n / i2; g++)
			{
				int j0 = i2 * g, j1 = j0 + i2 / 2;
				for (int j = j0, l = j1; j < j1; j++, l++)
				{
					dp[i, j] = dp[i - 1, j] * Enumerable.Range(j1, i2 / 2).Sum(x => dp[i - 1, x] * p[j, x]);
					dp[i, l] = dp[i - 1, l] * Enumerable.Range(j0, i2 / 2).Sum(x => dp[i - 1, x] * p[l, x]);
				}
			}
		Console.WriteLine(string.Join("\n", Enumerable.Range(0, n).Select(i => dp[k, i])));
	}
}
