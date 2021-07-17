using System;
using System.Linq;

class Q066D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		// i 個のうち j を超える個数の期待値
		var dp = new double[n + 1, 101];
		var e = 0D;

		for (int i = 0; i < n; i++)
		{
			var (l, r) = ps[i];
			var all = r - l + 1;

			for (int j = 0; j <= 100; j++)
			{
				dp[i + 1, j] = dp[i, j];

				if (j < l)
				{
					dp[i + 1, j] += 1;
				}
				else if (j < r)
				{
					dp[i + 1, j] += (double)(r - j) / all;
				}
			}

			e += Enumerable.Range(l, all).Sum(j => dp[i, j]) / all;
		}

		return e;
	}
}
