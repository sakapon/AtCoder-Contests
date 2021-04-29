using System;

class Q005A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, b, k) = ((long, int, int))Read3L();
		var cs = Read();

		var dp = NewArray2<long>((int)n + 1, b);
		dp[0][0] = 1;

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < b; j++)
			{
				foreach (var c in cs)
				{
					dp[i + 1][(10 * j + c) % b] += dp[i][j];
				}
			}
			for (int j = 0; j < b; j++)
			{
				dp[i + 1][j] %= M;
			}
		}

		return dp[n][0];
	}

	const long M = 1000000007;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
