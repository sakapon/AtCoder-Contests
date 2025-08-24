using System;
using System.Linq;

class L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var f = Array.ConvertAll(new bool[n], _ => Read());

		var dp = new long[n];
		var dt = new long[n];

		for (int i = 1; i < n; i++)
		{
			var max = long.MinValue;
			var sum = f[i][0..i].Sum();

			for (int j = 0; j <= i; j++)
			{
				if (max < dp[j]) max = dp[j];
				dt[j] = max + sum;
				sum -= f[i][j];
			}

			(dp, dt) = (dt, dp);
		}
		return dp.Max() << 1;
	}
}
