﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections.Dynamics.Int;

class O
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ =>
		{
			var q = Console.ReadLine().Split();
			return (add: q[0][0] == '+', x: int.Parse(q[1]), y: int.Parse(q[2]));
		});

		var ps = new List<(double angle, int qi)>();
		var pmap = new Dictionary<(int, int), int>();

		for (int qi = 0; qi < qc; qi++)
		{
			var (add, x, y) = qs[qi];
			if (!add) continue;
			if (pmap.ContainsKey((x, y))) continue;

			var angle = Math.Atan2(y, x);
			ps.Add((angle, qi));
			pmap[(x, y)] = -1;
		}

		var n = ps.Count;
		ps.Sort();
		for (int pi = 0; pi < n; pi++)
		{
			var qi = ps[pi].qi;
			var (_, x, y) = qs[qi];
			pmap[(x, y)] = pi;
		}

		var r = new long[qc];
		var s = 0L;
		var rsx = new IntSegmentRangeSum(n);
		var rsy = new IntSegmentRangeSum(n);

		for (int qi = 0; qi < qc; qi++)
		{
			var (add, x, y) = qs[qi];
			var pi = pmap[(x, y)];
			var angle = Math.Atan2(y, x);

			if (add)
			{
				s += GetDelta();
				rsx[pi] += x;
				rsy[pi] += y;
			}
			else
			{
				rsx[pi] -= x;
				rsy[pi] -= y;
				s -= GetDelta();
			}
			s %= M;
			if (s < 0) s += M;
			r[qi] = s;

			long GetDelta()
			{
				if (y >= 0)
				{
					// 前
					var pi1 = First(0, n, i => ps[i].angle >= angle - Math.PI);
					var sx1 = rsx[pi1, pi];
					var sy1 = rsy[pi1, pi];
					var sx2 = rsx.Sum - sx1;
					var sy2 = rsy.Sum - sy1;
					return (sx1 - sx2) % M * y - (sy1 - sy2) % M * x;
				}
				else
				{
					// 後
					var pi2 = First(0, n, i => ps[i].angle >= angle + Math.PI);
					var sx2 = rsx[pi, pi2];
					var sy2 = rsy[pi, pi2];
					var sx1 = rsx.Sum - sx2;
					var sy1 = rsy.Sum - sy2;
					return (sx1 - sx2) % M * y - (sy1 - sy2) % M * x;
				}
			}
		}
		return string.Join("\n", r);
	}

	const long M = 998244353;

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
