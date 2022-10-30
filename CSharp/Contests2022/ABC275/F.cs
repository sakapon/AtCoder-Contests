using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		const int max = 1 << 30;

		// [sum][continuous]: 削除回数
		var dp = NewArray2(m + 1, 2, max);
		var dt = NewArray2(m + 1, 2, max);

		dp[0][1] = 0;

		for (int i = 0; i < n; i++)
		{
			for (int x = 0; x <= m; x++)
			{
				dt[x][0] = Math.Min(dt[x][0], dp[x][0]);
				dt[x][0] = Math.Min(dt[x][0], dp[x][1] + 1);

				var nx = x + a[i];
				if (nx > m) continue;

				dt[nx][1] = Math.Min(dt[nx][1], dp[x][0]);
				dt[nx][1] = Math.Min(dt[nx][1], dp[x][1]);
			}

			(dp, dt) = (dt, dp);
			dt = NewArray2(m + 1, 2, max);
		}

		var rn = Enumerable.Range(1, m).ToArray();
		return string.Join("\n", rn.Select(x => dp[x].Min()).Select(x => x == max ? -1 : x));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
