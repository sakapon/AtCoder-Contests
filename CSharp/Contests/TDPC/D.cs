using System;
using static System.Math;

class D
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long n = h[0], d = h[1];

		int p2 = 0, p3 = 0, p5 = 0;
		while (d % 2 == 0) { d /= 2; p2++; }
		while (d % 3 == 0) { d /= 3; p3++; }
		while (d % 5 == 0) { d /= 5; p5++; }
		if (d != 1) { Console.WriteLine(0); return; }

		var dp = new double[n + 1, p2 + 1, p3 + 1, p5 + 1];
		dp[0, 0, 0, 0] = 1;
		for (int m = 0; m < n; m++)
			for (int i = 0; i <= p2; i++)
				for (int j = 0; j <= p3; j++)
					for (int k = 0; k <= p5; k++)
					{
						var v = dp[m, i, j, k] / 6;
						dp[m + 1, i, j, k] += v;
						dp[m + 1, Min(p2, i + 1), j, k] += v;
						dp[m + 1, i, Min(p3, j + 1), k] += v;
						dp[m + 1, Min(p2, i + 2), j, k] += v;
						dp[m + 1, i, j, Min(p5, k + 1)] += v;
						dp[m + 1, Min(p2, i + 1), Min(p3, j + 1), k] += v;
					}
		Console.WriteLine(dp[n, p2, p3, p5]);
	}
}
