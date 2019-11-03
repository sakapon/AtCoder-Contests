using System;
using static System.Math;

class D
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long n = h[0], d = h[1];

		int p = 0, q = 0, r = 0;
		while (d % 2 == 0) { d /= 2; p++; }
		while (d % 3 == 0) { d /= 3; q++; }
		while (d % 5 == 0) { d /= 5; r++; }

		var dp = new double[n + 1, p + 1, q + 1, r + 1];
		dp[0, 0, 0, 0] = 1;
		for (int m = 1; m <= n; m++)
			for (int i = 0; i <= p; i++)
				for (int j = 0; j <= q; j++)
					for (int k = 0; k <= r; k++)
					{
						var v = dp[m - 1, i, j, k] / 6;
						dp[m, i, j, k] += v;
						dp[m, Min(p, i + 1), j, k] += v;
						dp[m, i, Min(q, j + 1), k] += v;
						dp[m, Min(p, i + 2), j, k] += v;
						dp[m, i, j, Min(r, k + 1)] += v;
						dp[m, Min(p, i + 1), Min(q, j + 1), k] += v;
					}
		Console.WriteLine(d == 1 ? dp[n, p, q, r] : 0);
	}
}
