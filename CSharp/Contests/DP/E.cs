using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, W) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		return DP1(n, W, ps);
	}

	// 配る
	static long DP1(int n, int W, (int, int)[] ps)
	{
		var V = 1000 * n;
		var dp = new long[V + 1];
		Array.Fill(dp, long.MaxValue);
		dp[0] = 0;

		foreach (var (w, v) in ps)
		{
			for (int i = V - v; i >= 0; i--)
			{
				if (dp[i] == long.MaxValue) continue;
				Chmin(ref dp[i + v], dp[i] + w);
			}
		}
		return Enumerable.Range(0, V + 1).Last(i => dp[i] <= W);
	}

	public static long Chmin(ref long x, long v) => x > v ? x = v : x;
}
