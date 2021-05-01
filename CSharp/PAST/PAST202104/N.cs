using System;
using System.Linq;

class N
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, h) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var dp = Array.ConvertAll(new bool[h + 1], _ => -1L);
		dp[h] = 0;

		foreach (var (a, b) in ps.OrderBy(p => -(decimal)p.a / p.b))
		{
			for (long i = 1; i <= h; i++)
			{
				if (dp[i] == -1) continue;

				var ni = Math.Max(0, i - b);
				dp[ni] = Math.Max(dp[ni], dp[i] + a * i);
			}
		}
		return dp.Max();
	}
}
