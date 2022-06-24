using System;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var w = Read();

		var rsq = new StaticRSQ1(w);
		var dp = new long[n + 1, n + 1];
		var center = new int[n + 1, n + 1];

		// d == 2
		for (int l = 0; l + 2 <= n; l++)
		{
			dp[l, l + 2] = w[l] + w[l + 1];
			center[l, l + 2] = l + 1;
		}

		for (int d = 3; d <= n; d++)
		{
			for (int l = 0; l + d <= n; l++)
			{
				dp[l, l + d] = 1L << 60;

				for (int c = center[l, l + d - 1]; c <= center[l + 1, l + d]; c++)
				{
					var nv = dp[l, c] + dp[c, l + d];
					if (dp[l, l + d] > nv)
					{
						dp[l, l + d] = nv;
						center[l, l + d] = c;
					}
				}
				dp[l, l + d] += rsq.GetSum(l, l + d);
			}
		}
		return dp[0, n];
	}
}

public class StaticRSQ1
{
	int n;
	long[] s;
	public long[] Raw => s;
	public StaticRSQ1(int[] a)
	{
		n = a.Length;
		s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
	}

	// [l, r)
	// 範囲外のインデックスも可。
	public long GetSum(int l, int r)
	{
		if (r < 0 || n < l) return 0;
		if (l < 0) l = 0;
		if (n < r) r = n;
		return s[r] - s[l];
	}
}
