using System;

class Q045
{
	const long max = 1L << 60;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		// グループ内での最大値
		var d = new long[1 << n];
		for (int x = 0; x < 1 << n; x++)
		{
			var m = 0L;

			for (int i = 0; i < n; i++)
			{
				if ((x & (1 << i)) == 0) continue;

				for (int j = i + 1; j < n; j++)
				{
					if ((x & (1 << j)) == 0) continue;

					long dx = ps[i].x - ps[j].x;
					long dy = ps[i].y - ps[j].y;
					m = Math.Max(m, dx * dx + dy * dy);
				}
			}
			d[x] = m;
		}

		var dp = NewArray2(1 << n, k + 1, max);
		dp[0][0] = 0;

		for (int x = 0; x < 1 << n; x++)
		{
			for (int y = x + 1; y < 1 << n; y++)
			{
				if ((x & y) != 0) continue;
				var nx = x | y;

				for (int j = 0; j < k; j++)
				{
					if (dp[x][j] == max) continue;

					dp[nx][j + 1] = Math.Min(dp[nx][j + 1], Math.Max(dp[x][j], d[y]));
				}
			}
		}
		return dp[^1][k];
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
