using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		return Last(0, 1 << 30, AverageDP, 3) + "\n" + Last(0, 1 << 30, MedianDP);

		bool AverageDP(double v)
		{
			var dp = new double[n + 1];
			dp[1] = a[0] - v;

			for (int i = 1; i < n; i++)
			{
				dp[i + 1] = Math.Max(dp[i - 1], dp[i]) + a[i] - v;
			}
			return Math.Max(dp[^2], dp[^1]) >= 0;
		}

		bool MedianDP(int v)
		{
			var dp = new int[n + 1];
			dp[1] = a[0] >= v ? 1 : -1;

			for (int i = 1; i < n; i++)
			{
				dp[i + 1] = Math.Max(dp[i - 1], dp[i]) + (a[i] >= v ? 1 : -1);
			}
			return Math.Max(dp[^2], dp[^1]) > 0;
		}
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}

	static double Last(double l, double r, Func<double, bool> f, int digits = 9)
	{
		double m;
		while (Math.Round(r - l, digits) > 0) if (f(m = r - (r - l) / 2)) l = m; else r = m;
		return l;
	}
}
