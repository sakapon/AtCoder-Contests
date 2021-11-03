using System;
using System.Linq;

class C
{
	const long max = 1L << 60;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ => Read());

		var dp = NewArray2(1 << n, n, max);
		dp[1][0] = 0;

		for (int x = 1; x < 1 << n; x++)
		{
			for (int i = 0; i < n; i++)
			{
				if (dp[x][i] == max) continue;

				for (int j = 0; j < n; j++)
				{
					if ((x & (1 << j)) != 0) continue;

					var nx = x | (1 << j);
					var nv = dp[x][i] + a[i][j];
					dp[nx][j] = Math.Min(dp[nx][j], nv);
				}
			}
		}

		return Enumerable.Range(1, n - 1).Min(j => dp[(1 << n) - 1][j] + a[j][0]);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
