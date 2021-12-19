using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var dp = NewArray2<int>(h, w);
		dp[0][0] = 1;

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == '#') continue;
				if (dp[i][j] == 0) continue;

				if (i < h - 1 && s[i + 1][j] == '.')
					dp[i + 1][j] = Math.Max(dp[i + 1][j], dp[i][j] + 1);
				if (j < w - 1 && s[i][j + 1] == '.')
					dp[i][j + 1] = Math.Max(dp[i][j + 1], dp[i][j] + 1);
			}
		}

		return dp.Max(r => r.Max());
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
