using System;
using System.Linq;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var b = ReadL();

		var m = Math.Max(GetMax(false), GetMax(true));
		return a.Sum() + b.Sum() - m;

		long GetMax(bool same01)
		{
			var dp = NewArray2(n + 1, 2, long.MinValue);

			if (same01)
				dp[1][0] = 0;
			else
				dp[1][1] = a[0];

			for (int i = 2; i <= n; i++)
			{
				dp[i][0] = Math.Max(dp[i - 1][0], dp[i - 1][1] + b[i - 2]);
				dp[i][1] = Math.Max(dp[i - 1][0] + b[i - 2], dp[i - 1][1]);
				dp[i][1] += a[i - 1];
			}

			if (same01)
				dp[n][1] += b[^1];
			else
				dp[n][0] += b[^1];

			return dp[n].Max();
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
