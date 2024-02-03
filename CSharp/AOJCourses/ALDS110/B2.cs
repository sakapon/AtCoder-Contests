using System;

class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int r, int c) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var dp = new long[n + 1, n + 1];
		for (int len = 2; len <= n; len++)
		{
			for (int l = 0, r = len; r <= n; l++, r++)
			{
				dp[l, r] = long.MaxValue;
				for (int c = l + 1; c < r; c++)
				{
					var nv = dp[l, c] + dp[c, r] + ps[l].r * ps[c].r * ps[r - 1].c;
					if (dp[l, r] > nv) dp[l, r] = nv;
				}
			}
		}
		return dp[0, n];
	}
}
