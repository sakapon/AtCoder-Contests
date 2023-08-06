using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var u = NewArray2<bool>(h, w);
		foreach (var (a, b) in ps)
		{
			u[a - 1][b - 1] = true;
		}

		var dp = NewArray2<long>(h, w);

		for (int i = 0; i < h; i++)
		{
			dp[i][0] = u[i][0] ? 0 : 1;
		}
		for (int j = 0; j < w; j++)
		{
			dp[0][j] = u[0][j] ? 0 : 1;
		}

		for (int i = 1; i < h; i++)
		{
			for (int j = 1; j < w; j++)
			{
				if (u[i][j]) continue;
				dp[i][j] = Min(dp[i - 1][j - 1], dp[i][j - 1], dp[i - 1][j]) + 1;
			}
		}
		return dp.Sum(r => r.Sum());
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	static long Min(long x, long y, long z)
	{
		if (x > y) x = y;
		return x > z ? z : x;
	}
}
