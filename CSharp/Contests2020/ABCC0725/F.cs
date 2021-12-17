using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => { var a = Console.ReadLine().Split(); return (x: int.Parse(a[0]), y: int.Parse(a[1]), d: a[2][0]); });

		var qs = Array.ConvertAll(ps, p => (u: p.x + p.y, v: p.x - p.y, p.d));

		var r = max;

		foreach (var g in ps.Where(p => p.d == 'R' || p.d == 'L').GroupBy(p => p.y))
		{
			var xys = g.OrderBy(p => p.x).ToArray();

			for (int i = 1; i < xys.Length; i++)
			{
				if (xys[i - 1].d == 'R' && xys[i].d == 'L')
				{
					r = Math.Min(r, (xys[i].x - xys[i - 1].x) * 5);
				}
			}
		}
		foreach (var g in ps.Where(p => p.d == 'U' || p.d == 'D').GroupBy(p => p.x))
		{
			var xys = g.OrderBy(p => p.y).ToArray();

			for (int i = 1; i < xys.Length; i++)
			{
				if (xys[i - 1].d == 'U' && xys[i].d == 'D')
				{
					r = Math.Min(r, (xys[i].y - xys[i - 1].y) * 5);
				}
			}
		}

		foreach (var g in qs.Where(q => q.d == 'R' || q.d == 'U').GroupBy(q => q.u))
		{
			var uvs = g.OrderBy(q => q.v).ToArray();

			for (int i = 1; i < uvs.Length; i++)
			{
				if (uvs[i - 1].d == 'R' && uvs[i].d == 'U')
				{
					r = Math.Min(r, (uvs[i].v - uvs[i - 1].v) * 5);
				}
			}
		}
		foreach (var g in qs.Where(q => q.d == 'D' || q.d == 'L').GroupBy(q => q.u))
		{
			var uvs = g.OrderBy(q => q.v).ToArray();

			for (int i = 1; i < uvs.Length; i++)
			{
				if (uvs[i - 1].d == 'D' && uvs[i].d == 'L')
				{
					r = Math.Min(r, (uvs[i].v - uvs[i - 1].v) * 5);
				}
			}
		}

		foreach (var g in qs.Where(q => q.d == 'R' || q.d == 'D').GroupBy(q => q.v))
		{
			var uvs = g.OrderBy(q => q.u).ToArray();

			for (int i = 1; i < uvs.Length; i++)
			{
				if (uvs[i - 1].d == 'R' && uvs[i].d == 'D')
				{
					r = Math.Min(r, (uvs[i].u - uvs[i - 1].u) * 5);
				}
			}
		}
		foreach (var g in qs.Where(q => q.d == 'U' || q.d == 'L').GroupBy(q => q.v))
		{
			var uvs = g.OrderBy(q => q.u).ToArray();

			for (int i = 1; i < uvs.Length; i++)
			{
				if (uvs[i - 1].d == 'U' && uvs[i].d == 'L')
				{
					r = Math.Min(r, (uvs[i].u - uvs[i - 1].u) * 5);
				}
			}
		}

		if (r == max) return "SAFE";
		return r;
	}
}
