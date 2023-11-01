using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x, y) = Read3();
		var a = Read();

		if (n <= 42)
		{
			var ax = a.Where((v, i) => i % 2 == 1).ToArray();
			var ay = a.Where((v, i) => i % 2 == 0).ToArray();

			var dpx = DoDP(ax);
			var dpy = DoDP(ay);
			if (!dpx.ContainsKey(x) || !dpy.ContainsKey(y)) return "No";

			// 正方向に進んだかどうか
			var pathx = dpx[x].ToBitArray(ax.Length);
			var pathy = dpy[y].ToBitArray(ay.Length);

			var b = Enumerable.Range(0, n)
				.Select(i => i % 2 == 0 ? pathy[i / 2] : pathx[i / 2])
				.Prepend(true)
				.ToArray();

			return "Yes\n" + string.Join("", Enumerable.Range(1, n).Select(i => ((i % 2 == 1) ^ b[i] ^ b[i - 1]) ? 'L' : 'R'));
		}
		else
		{
			var ax0 = a[..40].Where((v, i) => i % 2 == 1).ToArray();
			var ay0 = a[..40].Where((v, i) => i % 2 == 0).ToArray();
			var ax1 = a[40..].Where((v, i) => i % 2 == 1).ToArray();
			var ay1 = a[40..].Where((v, i) => i % 2 == 0).ToArray();

			var dpx0 = DoDP(ax0);
			var dpy0 = DoDP(ay0);
			var dpx1 = DoDP(ax1);
			var dpy1 = DoDP(ay1);

			var x1a = dpx1.Keys.Where(z => dpx0.ContainsKey(x - z)).Take(1).ToArray();
			var y1a = dpy1.Keys.Where(z => dpy0.ContainsKey(y - z)).Take(1).ToArray();
			if (x1a.Length == 0 || y1a.Length == 0) return "No";

			var x1 = x1a[0];
			var y1 = y1a[0];

			// 正方向に進んだかどうか
			var pathx0 = dpx0[x - x1].ToBitArray(ax0.Length);
			var pathy0 = dpy0[y - y1].ToBitArray(ay0.Length);
			var pathx1 = dpx1[x1].ToBitArray(ax1.Length);
			var pathy1 = dpy1[y1].ToBitArray(ay1.Length);

			var b0 = Enumerable.Range(0, 40)
				.Select(i => i % 2 == 0 ? pathy0[i / 2] : pathx0[i / 2]);
			var b1 = Enumerable.Range(0, n - 40)
				.Select(i => i % 2 == 0 ? pathy1[i / 2] : pathx1[i / 2]);
			var b = b0.Concat(b1)
				.Prepend(true)
				.ToArray();

			return "Yes\n" + string.Join("", Enumerable.Range(1, n).Select(i => ((i % 2 == 1) ^ b[i] ^ b[i - 1]) ? 'L' : 'R'));
		}
	}

	static Dictionary<int, uint> DoDP(int[] a)
	{
		var dp = new Dictionary<int, uint>();
		var dt = new Dictionary<int, uint>();
		dp[0] = 0;

		for (int i = 0; i < a.Length; i++)
		{
			foreach (var (s, v) in dp)
			{
				if (!dt.ContainsKey(s + a[i])) dt[s + a[i]] = v | (1U << i);
				if (!dt.ContainsKey(s - a[i])) dt[s - a[i]] = v;
			}

			(dp, dt) = (dt, dp);
			dt.Clear();
		}
		return dp;
	}
}
