using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var c = Array.ConvertAll(new bool[1 << n], _ => Read());

		// i がちょうど j 回勝つ場合の、その範囲の合計の最大値
		var dp = new long[1 << n];
		var t = new long[1 << n];

		for (int j = 0; j < n; j++)
		{
			Array.Clear(t, 0, t.Length);
			var len = 1 << j;

			for (int l = 0; l < 1 << n; l += len << 1)
			{
				var m = l + len;
				var r = m + len;
				var lmax = Enumerable.Range(l, len).Max(i => dp[i]);
				var rmax = Enumerable.Range(m, len).Max(i => dp[i]);

				for (int i = l; i < m; i++)
				{
					t[i] = dp[i] - (j > 0 ? c[i][j - 1] : 0) + c[i][j] + rmax;
				}
				for (int i = m; i < r; i++)
				{
					t[i] = dp[i] - (j > 0 ? c[i][j - 1] : 0) + c[i][j] + lmax;
				}
			}

			(dp, t) = (t, dp);
		}

		return dp.Max();
	}
}
