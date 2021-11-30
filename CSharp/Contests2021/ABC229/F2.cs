using System;
using System.Linq;

class F2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var b = ReadL();

		return Math.Min(GetMin(false), GetMin(true));

		long GetMin(bool same01)
		{
			var dp = NewArray2(n + 1, 2, 1L << 60);

			if (same01)
				dp[1][0] = a[0];
			else
				dp[1][1] = 0;

			for (int i = 2; i <= n; i++)
			{
				dp[i][0] = Math.Min(dp[i - 1][0] + b[i - 2], dp[i - 1][1]);
				dp[i][0] += a[i - 1];
				dp[i][1] = Math.Min(dp[i - 1][0], dp[i - 1][1] + b[i - 2]);
			}

			if (same01)
				dp[n][0] += b[^1];
			else
				dp[n][1] += b[^1];

			return dp[n].Min();
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
