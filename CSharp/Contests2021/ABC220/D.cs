using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var dp = NewArray2<long>(n, 10);
		dp[0][a[0]] = 1;

		for (int i = 1; i < n; i++)
		{
			var y = a[i];

			for (int x = 0; x < 10; x++)
			{
				var nx1 = (x + y) % 10;
				dp[i][nx1] += dp[i - 1][x];
				dp[i][nx1] %= M;

				var nx2 = (x * y) % 10;
				dp[i][nx2] += dp[i - 1][x];
				dp[i][nx2] %= M;
			}
		}

		return string.Join("\n", dp[^1]);
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
