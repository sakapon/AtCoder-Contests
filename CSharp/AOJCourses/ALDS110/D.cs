using System;

class D
{
	static double[] ReadD() => Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = ReadD();
		var q = ReadD();

		var sp = new StaticRSQ1(p);
		var sq = new StaticRSQ1(q);

		var dp = new double[n + 2, n + 2];
		for (int i = 0; i <= n; i++)
			dp[i, i + 1] = q[i];

		for (int len = 2; len <= n + 1; len++)
		{
			for (int l = 0, r; (r = l + len) <= n + 1; l++)
			{
				var min = double.MaxValue;

				for (int c = 1; c < len; c++)
				{
					min = Math.Min(min, dp[l, l + c] + dp[l + c, r]);
				}
				dp[l, r] = min + sp.GetSum(l, r - 1) + sq.GetSum(l, r);
			}
		}
		return dp[0, n + 1];
	}
}

public class StaticRSQ1
{
	int n;
	double[] s;
	public double[] Raw => s;
	public StaticRSQ1(double[] a)
	{
		n = a.Length;
		s = new double[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
	}

	// [l, r)
	// 範囲外のインデックスも可。
	public double GetSum(int l, int r)
	{
		if (r < 0 || n < l) return 0;
		if (l < 0) l = 0;
		if (n < r) r = n;
		return s[r] - s[l];
	}
}
