using System;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var (x, y) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		const int max = 1 << 30;
		var dp = NewArray2(x + 1, y + 1, max);
		dp[0][0] = 0;

		foreach (var (a, b) in ps)
		{
			for (int i = x; i >= 0; i--)
			{
				var ni = Math.Min(x, i + a);

				for (int j = y; j >= 0; j--)
				{
					if (dp[i][j] == max) continue;

					var nj = Math.Min(y, j + b);
					var nv = dp[i][j] + 1;
					dp[ni][nj] = Math.Min(dp[ni][nj], nv);
				}
			}
		}

		if (dp[x][y] == max) return -1;
		return dp[x][y];
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
