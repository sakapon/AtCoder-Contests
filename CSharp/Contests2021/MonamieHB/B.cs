using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = ((long, int))Read2L();

		var a = Convert(n - 1, k);
		Array.Reverse(a);

		// 0: <, 1: =
		var dp = NewArray2<long>(a.Length + 1, 2);
		dp[0][1] = 1;

		for (int i = 0; i < a.Length; i++)
		{
			dp[i + 1][0] += dp[i][0] * (k - 1);
			dp[i + 1][0] += dp[i][1] * a[i];
			if (a[i] < k - 1) dp[i + 1][1] += dp[i][1];
		}
		return dp[^1].Sum();
	}

	static int[] Convert(long x, int b)
	{
		var r = new List<int>();
		for (; x > 0; x /= b) r.Add((int)(x % b));
		return r.ToArray();
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
