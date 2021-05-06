using System;

class Q005
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, b, k) = ((long, int, int))Read3L();
		var cs = Read();

		const int ln = 60;

		// 10^(2^i) mod b
		var p10 = new long[ln + 1];
		p10[0] = 10 % b;
		for (int i = 0; i < ln; ++i) p10[i + 1] = p10[i] * p10[i] % b;

		// 2^i 桁で j mod b
		var dp = NewArray2<long>(ln + 1, b);
		foreach (var c in cs)
		{
			dp[0][c % b]++;
		}

		for (int i = 0; i < ln; i++)
		{
			for (int j1 = 0; j1 < b; j1++)
			{
				for (int j2 = 0; j2 < b; j2++)
				{
					var nj = (p10[i] * j1 + j2) % b;
					dp[i + 1][nj] += dp[i][j1] * dp[i][j2];
					dp[i + 1][nj] %= M;
				}
			}
		}

		var r = new long[b];
		r[0] = 1;
		for (int i = 0; i < ln && n != 0; i++, n >>= 1)
		{
			if ((n & 1) != 0)
			{
				var t = new long[b];
				for (int j1 = 0; j1 < b; j1++)
				{
					for (int j2 = 0; j2 < b; j2++)
					{
						var nj = (p10[i] * j1 + j2) % b;
						t[nj] += r[j1] * dp[i][j2];
						t[nj] %= M;
					}
				}
				r = t;
			}
		}

		return r[0];
	}

	const long M = 1000000007;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	public static long[] PowsL(long b, int n)
	{
		var p = new long[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b;
		return p;
	}
}
