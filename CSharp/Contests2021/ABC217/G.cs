using System;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();

		var dp = NewArray2<long>(n + 1, n + 1);
		dp[0][0] = 1;

		for (int i = 1; i <= n; i++)
		{
			for (int j = 1; j <= n; j++)
			{
				dp[i][j] += dp[i - 1][j - 1];
				dp[i][j] += dp[i - 1][j] * (j - (i - 1) / m);
				dp[i][j] %= M;
			}
		}

		return string.Join("\n", dp[^1][1..]);
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
