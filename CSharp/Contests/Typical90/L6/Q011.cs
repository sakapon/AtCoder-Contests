using System;
using System.Linq;

class Q011
{
	const int MaxDay = 5000;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int d, int c, int s) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		// i 日目までの報酬
		var dp = NewArray1(MaxDay + 1, -1L);
		dp[0] = 0;

		foreach (var (d, c, s) in ps.OrderBy(_ => _.d))
		{
			for (int i = d - c; i >= 0; i--)
			{
				if (dp[i] == -1) continue;

				dp[i + c] = Math.Max(dp[i + c], dp[i] + s);
			}
		}
		return dp.Max();
	}

	static T[] NewArray1<T>(int n, T v = default) => Array.ConvertAll(new bool[n], _ => v);
}
