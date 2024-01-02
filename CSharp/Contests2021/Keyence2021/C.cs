using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, k) = Read3();
		var ps = Array.ConvertAll(new bool[k], _ => Console.ReadLine().Split());

		var M2_3 = MInv(3) * 2 % M;
		var s = new char[h, w];
		var dp = new long[h + 1, w + 1];
		dp[0, 0] = MPow(3, h * w - k);

		foreach (var p in ps)
		{
			s[int.Parse(p[0]) - 1, int.Parse(p[1]) - 1] = p[2][0];
		}

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (s[i, j] == 0)
				{
					dp[i, j + 1] += dp[i, j] * M2_3;
					dp[i + 1, j] += dp[i, j] * M2_3;
				}
				else if (s[i, j] == 'R')
				{
					dp[i, j + 1] += dp[i, j];
				}
				else if (s[i, j] == 'D')
				{
					dp[i + 1, j] += dp[i, j];
				}
				else
				{
					dp[i, j + 1] += dp[i, j];
					dp[i + 1, j] += dp[i, j];
				}

				dp[i, j + 1] %= M;
				dp[i + 1, j] %= M;
			}
		}
		return dp[h - 1, w - 1];
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);
}
