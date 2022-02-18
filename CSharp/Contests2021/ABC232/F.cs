using System;
using System.Numerics;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x, y) = ((int, long, long))Read3L();
		var a = ReadL();
		var b = ReadL();

		const long max = 1L << 60;
		var dp = new long[1 << n];
		Array.Fill(dp, max);
		dp[0] = 0;

		for (int i = 0; i < n; i++)
		{
			for (uint z = 0; z < 1 << n; z++)
			{
				if (BitOperations.PopCount(z) != i) continue;

				for (int j = 0; j < n; j++)
				{
					var nz = z | (1U << j);
					if (nz == z) continue;

					// j はフラグ 0 のビットのうち何番目か
					var k = 0;
					for (int l = 0; l < j; l++)
					{
						if ((z & (1 << l)) == 0) k++;
					}

					dp[nz] = Math.Min(dp[nz], dp[z] + y * k + x * Math.Abs(a[j] - b[i]));
				}
			}
		}

		return dp[^1];
	}
}
