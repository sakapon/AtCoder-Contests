using System;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var dp = NewArray2(h + 1, w + 1, -1 << 30);

		for (int i = h - 1; i >= 0; i--)
		{
			for (int j = w - 1; j >= 0; j--)
			{
				if (s[i][j] == '#') continue;

				dp[i][j] = 1;
				dp[i][j] = Math.Max(dp[i][j], dp[i][j + 1] + 1);
				dp[i][j] = Math.Max(dp[i][j], dp[i + 1][j] + 1);
			}
		}

		return dp[0][0];
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
