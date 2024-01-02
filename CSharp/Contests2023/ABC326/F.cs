using System;
using System.Collections.Generic;
using System.Linq;

class F
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
			if (!dpx[^1].ContainsKey(x) || !dpy[^1].ContainsKey(y)) return "No";

			// 正方向に進んだかどうか
			var pathx = GetPath(dpx, x);
			var pathy = GetPath(dpy, y);

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

			var x1a = dpx1[^1].Keys.Where(z => dpx0[^1].ContainsKey(x - z)).Take(1).ToArray();
			var y1a = dpy1[^1].Keys.Where(z => dpy0[^1].ContainsKey(y - z)).Take(1).ToArray();
			if (x1a.Length == 0 || y1a.Length == 0) return "No";

			var x1 = x1a[0];
			var y1 = y1a[0];

			// 正方向に進んだかどうか
			var pathx0 = GetPath(dpx0, x - x1);
			var pathy0 = GetPath(dpy0, y - y1);
			var pathx1 = GetPath(dpx1, x1);
			var pathy1 = GetPath(dpy1, y1);

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

	static Dictionary<int, int>[] DoDP(int[] a)
	{
		var dp = Array.ConvertAll(new bool[a.Length + 1], _ => new Dictionary<int, int>());
		dp[0][0] = 0;

		for (int i = 0; i < a.Length; i++)
		{
			foreach (var (s, v) in dp[i])
			{
				if (!dp[i + 1].ContainsKey(s + a[i])) dp[i + 1][s + a[i]] = s;
				if (!dp[i + 1].ContainsKey(s - a[i])) dp[i + 1][s - a[i]] = s;
			}
		}
		return dp;
	}

	static bool[] GetPath(Dictionary<int, int>[] dp, int xylast)
	{
		// 正方向に進んだかどうか
		var b = new bool[dp.Length - 1];
		var t = xylast;
		for (int i = b.Length - 1; i >= 0; i--)
		{
			b[i] = t - dp[i + 1][t] > 0;
			t = dp[i + 1][t];
		}
		return b;
	}
}
