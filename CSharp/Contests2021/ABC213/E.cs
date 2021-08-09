using System;
using System.Collections.Generic;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		int ToId(int i, int j) => i * w + j;
		(int i, int j) FromId(int id) => (id / w, id % w);
		int[] Nexts(int id) => new[] { id + 1, id - 1, id + w, id - w };

		var sv = 0;
		var ev = ToId(h - 1, w - 1);

		var r = BfsMod(2, h * w, p =>
		{
			var (i, j) = FromId(p);

			var nes = new List<int[]>();
			if (i != 0 && s[i - 1][j] == '.') nes.Add(new[] { p, p - w, 0 });
			if (i != h - 1 && s[i + 1][j] == '.') nes.Add(new[] { p, p + w, 0 });
			if (j != 0 && s[i][j - 1] == '.') nes.Add(new[] { p, p - 1, 0 });
			if (j != w - 1 && s[i][j + 1] == '.') nes.Add(new[] { p, p + 1, 0 });

			for (int di = -2; di <= 2; di++)
			{
				var ni = i + di;
				if (ni < 0 || h <= ni) continue;

				for (int dj = -2; dj <= 2; dj++)
				{
					var nj = j + dj;
					if (nj < 0 || w <= nj) continue;

					if ((di, dj) == (0, 0)) continue;
					if (Math.Abs(di) + Math.Abs(dj) == 4) continue;

					nes.Add(new[] { p, ToId(ni, nj), 1 });
				}
			}

			return nes.ToArray();
		},
		sv, ev);

		return r[ev];
	}

	public static long[] BfsMod(int mod, int n, Func<int, int[][]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var qs = Array.ConvertAll(new bool[mod], _ => new Queue<int>());
		costs[sv] = 0;
		qs[0].Enqueue(sv);

		for (long c = 0; Array.Exists(qs, q => q.Count > 0); ++c)
		{
			var q = qs[c % mod];
			while (q.Count > 0)
			{
				var v = q.Dequeue();
				if (v == ev) return costs;
				if (costs[v] < c) continue;

				foreach (var e in nexts(v))
				{
					var (nv, nc) = (e[1], c + e[2]);
					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					qs[nc % mod].Enqueue(nv);
				}
			}
		}
		return costs;
	}
}
