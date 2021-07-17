using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		var mv = int.Parse(Console.ReadLine());

		var mw = 1000 * n;
		var dp = Array.ConvertAll(new bool[mw + 2], _ => -1);
		dp[0] = 0;

		foreach (var (v, w) in ps)
		{
			for (int i = mw; i >= w; i--)
			{
				if (dp[i - w] == -1) continue;
				dp[i] = Math.Max(dp[i], dp[i - w] + v);
			}
		}

		var min = mv == 0 ? 1 : Array.IndexOf(dp, mv);
		var max = min;
		while (max <= mw && dp[max + 1] <= mv) max++;

		return $"{min}\n{(max == dp.Length - 1 ? "inf" : max.ToString())}";
	}
}
