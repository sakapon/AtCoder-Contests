using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var c = Array.ConvertAll(new bool[1 << n], _ => Read());

		// i がちょうど j 回勝つ場合の、その範囲の合計の最大値
		var dp = new long[1 << n];

		for (int j = 0; j < n; j++)
		{
			var len = 1 << j;

			for (int l = 0; l < 1 << n; l += len << 1)
			{
				var m = l + len;
				var r = m + len;
				var lmax = dp[l..m].Max();
				var rmax = dp[m..r].Max();

				for (int i = l; i < m; i++)
				{
					dp[i] += c[i][j] - (j > 0 ? c[i][j - 1] : 0) + rmax;
				}
				for (int i = m; i < r; i++)
				{
					dp[i] += c[i][j] - (j > 0 ? c[i][j - 1] : 0) + lmax;
				}
			}
		}
		return dp.Max();
	}
}
