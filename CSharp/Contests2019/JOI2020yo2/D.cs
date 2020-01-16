using System;
using System.Linq;

class D
{
	static void Main()
	{
		var d = new[]
		{
			"1234345456",
			"2123234345",
			"3212323434",
			"4321432543",
			"3234123234",
			"4323212323",
			"5432321432",
			"4345234123",
			"5434323212",
			"6543432321",
		};

		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int m = h[0], r = h[1];

		var max = 30;
		var dp = new int[max, 10, m];
		for (int i = 0; i < max; i++)
			for (int j = 0; j < 10; j++)
				for (int k = 0; k < m; k++)
					dp[i, j, k] = max;
		dp[0, 0, 0] = 0;

		var min = 30;
		for (int i = 0; i < max - 1; i++)
			for (int j = 0; j < 10; j++)
				for (int k = 0; k < m; k++)
				{
					if (dp[i, j, k] >= min) continue;
					for (int nj = 0; nj < 10; nj++)
					{
						var nk = (10 * k + nj) % m;
						var v = dp[i, j, k] + (d[j][nj] - '0');
						dp[i + 1, nj, nk] = Math.Min(dp[i + 1, nj, nk], v);
						if (nk == r) min = Math.Min(min, v);
					}
				}
		Console.WriteLine(min);
	}
}
