using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int r, int c, int a) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		const int max = 1 << 30;
		var rn = Enumerable.Range(0, n).ToArray();

		var dp = new int[n];
		var rmap = Array.ConvertAll(new bool[h + 1], _ => new List<(int a, int v)> { (max, -1) });
		var cmap = Array.ConvertAll(new bool[w + 1], _ => new List<(int a, int v)> { (max, -1) });

		foreach (var id in rn.OrderBy(id => -ps[id].a))
		{
			var (r, c, a) = ps[id];

			var rl = rmap[r];
			var cl = cmap[c];

			var rli = rl[^1].a == a ? 2 : 1;
			var cli = cl[^1].a == a ? 2 : 1;
			dp[id] = Math.Max(rl[^rli].v, cl[^cli].v) + 1;

			if (rli == 2)
			{
				rl[^1] = (a, Math.Max(rl[^1].v, dp[id]));
			}
			else
			{
				rl.Add((a, dp[id]));
			}

			if (cli == 2)
			{
				cl[^1] = (a, Math.Max(cl[^1].v, dp[id]));
			}
			else
			{
				cl.Add((a, dp[id]));
			}
		}

		return string.Join("\n", dp);
	}
}
