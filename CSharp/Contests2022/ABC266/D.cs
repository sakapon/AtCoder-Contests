using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int tmax = 100000;
		var n = int.Parse(Console.ReadLine());
		var ps = new (int, int)[tmax + 1];
		while (n-- > 0)
		{
			var (t, x, a) = Read3();
			ps[t] = (x + 1, a);
		}

		var dp = new long[7];
		var dt = new long[7];
		Array.Fill(dp, -1);
		dp[1] = 0;

		for (int t = 1; t <= tmax; t++)
		{
			dt[0] = dt[^1] = -1;

			for (int i = 1; i <= 5; i++)
			{
				dt[i] = dp[i];
				if (dt[i] < dp[i - 1]) dt[i] = dp[i - 1];
				if (dt[i] < dp[i + 1]) dt[i] = dp[i + 1];
			}

			var (x, a) = ps[t];
			if (dt[x] != -1) dt[x] += a;

			(dp, dt) = (dt, dp);
		}

		return dp.Max();
	}
}
