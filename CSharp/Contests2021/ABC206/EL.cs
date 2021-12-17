using System;
using System.Collections.Generic;
using System.Numerics;

class EL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (L, R) = Read2();
		if (L == 1) L++;

		var pts = GetPrimeTypes(R);
		var c = 0L;

		for (int x = L; x < R; x++)
		{
			c += R - x + 1;

			// x と互いに素
			c -= InclusionExclusion(pts[x].Length, b =>
			{
				var d = 1;
				for (int i = 0; i < pts[x].Length; i++)
				{
					if (b[i])
					{
						d *= pts[x][i];
					}
				}

				// d の倍数の個数
				return R / d - (x - 1) / d;
			});

			// x の倍数の個数
			c -= R / x;
		}
		return c * 2;
	}

	static int[][] GetPrimeTypes(int n)
	{
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int p = 2; p <= n; ++p)
			if (map[p].Count == 0)
				for (int x = p; x <= n; x += p)
					map[x].Add(p);
		return Array.ConvertAll(map, l => l.ToArray());
	}

	public static long InclusionExclusion(int n, Func<bool[], long> getCount)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		var r = 0L;
		for (uint x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;

			var sign = BitOperations.PopCount(x) % 2 == 0 ? 1 : -1;
			r += sign * getCount(b);
		}
		return r;
	}
}
