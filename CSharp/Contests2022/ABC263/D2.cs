using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, l, r) = Read3();
		var a = Read();

		// 0: L, 1: Ai, 2: R
		var dp = new long[3];

		for (int i = 0; i < n; i++)
		{
			dp[2] = dp.Min() + r;
			dp[1] = dp[..2].Min() + a[i];
			dp[0] += l;
		}
		return dp.Min();
	}
}
