using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var dp = NewArray2<int>(h + 1, w + 1);

		for (int i = 0; i <= h; i++)
		{
			for (int j = 0; j <= w; j++)
			{
				if ((i + j) % 2 == 1)
				{
					dp[i][j] = 1 << 30;
				}
				else
				{
					dp[i][j] = -1 << 30;
				}
			}
		}
		dp[h][w] = 0;

		for (int i = h; i > 0; i--)
		{
			for (int j = w; j > 0; j--)
			{
				var v = (((i + j) % 2 == 1) == (s[i - 1][j - 1] == '+')) ? 1 : -1;

				if ((i + j) % 2 == 1)
				{
					dp[i][j - 1] = Math.Max(dp[i][j - 1], dp[i][j] + v);
					dp[i - 1][j] = Math.Max(dp[i - 1][j], dp[i][j] + v);
				}
				else
				{
					dp[i][j - 1] = Math.Min(dp[i][j - 1], dp[i][j] + v);
					dp[i - 1][j] = Math.Min(dp[i - 1][j], dp[i][j] + v);
				}
			}
		}

		return dp[1][1] == 0 ? "Draw" : dp[1][1] > 0 ? "Takahashi" : "Aoki";
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
