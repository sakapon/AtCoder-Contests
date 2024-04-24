using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, W) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var dp = new long[W + 1];
		Array.Fill(dp, -1);
		dp[0] = 0;

		foreach (var (w, v) in ps)
		{
			for (int i = W - w; i >= 0; i--)
			{
				if (dp[i] == -1) continue;
				Chmax(ref dp[i + w], dp[i] + v);
			}
		}
		return dp.Max();
	}

	public static long Chmax(ref long x, long v) => x < v ? x = v : x;
}
