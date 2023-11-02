using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int[]>());
		foreach (var e in es)
			map[e[0]].Add(e);

		var dp = new double[n + 1];

		// b-cx の和が 0 以上となるパスが存在するような最後の x を求めます。
		return Last(0, 1 << 14, x =>
		{
			Array.Fill(dp, -1 << 30);
			dp[1] = 0;

			for (int v = 1; v < n; v++)
				foreach (var e in map[v])
					Chmax(ref dp[e[1]], dp[v] + e[2] - e[3] * x);
			return dp[n] >= 0;
		});
	}

	public static double Chmax(ref double x, double v) => x < v ? x = v : x;

	static double Last(double l, double r, Func<double, bool> f, int digits = 9)
	{
		double m;
		while (Math.Round(r - l, digits) > 0) if (f(m = r - (r - l) / 2)) l = m; else r = m;
		return l;
	}
}
