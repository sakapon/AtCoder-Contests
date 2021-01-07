using System;
using System.Collections.Generic;
using Bang.Graphs.Spp;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var s = GraphConsole.ReadEnclosedGrid(ref h, ref w);

		var sv = (h - 2, w - 1);
		var ev = (1, 1);

		var grid = ShortestPath.ForGrid(h, w);
		var dp = grid.CreateMap(int.MaxValue);
		dp[sv] = 0;

		var r = grid.ForUnweightedMap(v =>
		{
			if (v.i == 0 || v.j == 0) return new Point[0];

			var lv = v - (0, 1);
			var uv = v - (1, 0);

			if (v.j == w - 1)
			{
				var nlv = dp[v] + (s.GetValue(lv) == 'E' ? 0 : 1);
				dp[lv] = Math.Min(dp[lv], nlv);

				return new[] { lv };
			}
			else
			{
				var nlv = dp[v] + (s.GetValue(lv) == 'E' ? 0 : 1);
				dp[lv] = Math.Min(dp[lv], nlv);
				var nuv = dp[v] + (s.GetValue(uv) == 'S' ? 0 : 1);
				dp[uv] = Math.Min(dp[uv], nuv);

				return new[] { lv, uv };
			}
		}).Bfs(sv, (-1, -1));

		Console.WriteLine(dp[ev]);
	}
}
