using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var d = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));

		const int dMax = 100000;
		var dp0 = new long[dMax + 1];
		foreach (var v in d)
		{
			dp0[v]++;
		}

		var dp = NewArray2<long>(4, dMax + 1);
		dp[0] = dp0;

		for (int i = 1; i < 4; i++)
		{
			var cs = CumSumL(dp[i - 1]);
			for (int j = 0; j <= dMax; j++)
			{
				if (dp0[j] == 0) continue;
				dp[i][j] = dp0[j] * cs[(j >> 1) + 1] % M;
			}
		}
		return dp[^1].Sum() % M;
	}

	const long M = 1000000007;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	public static long[] CumSumL(long[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}
}
