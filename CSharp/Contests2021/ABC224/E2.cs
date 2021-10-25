using System;
using System.Linq;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int r, int c, int a) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		var dp = new int[n];
		var rmap = Array.ConvertAll(new bool[h + 1], _ => -1);
		var cmap = Array.ConvertAll(new bool[w + 1], _ => -1);

		var q = Enumerable.Range(0, n).GroupBy(id => ps[id].a).OrderBy(g => -g.Key);
		foreach (var g in q)
		{
			var ids = g.ToArray();

			foreach (var id in ids)
			{
				var (r, c, _) = ps[id];
				dp[id] = Math.Max(rmap[r], cmap[c]) + 1;
			}

			foreach (var id in ids)
			{
				var (r, c, _) = ps[id];
				rmap[r] = Math.Max(rmap[r], dp[id]);
				cmap[c] = Math.Max(cmap[c], dp[id]);
			}
		}

		return string.Join("\n", dp);
	}
}
