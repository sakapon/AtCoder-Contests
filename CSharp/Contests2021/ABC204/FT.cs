using System;
using System.Collections.Generic;
using System.Linq;

class FT
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = ((int, long))Read2L();

		// TLE のコードです O(combCount^3 log w)。
		// combCount = 200

		var combs = new List<int[]>();

		// 0: 半畳
		// 1: 縦
		// 2: 横
		Power(new[] { 0, 1, 2 }, h, p =>
		{
			for (int i = 0; i < h - 1; i++)
			{
				if (p[i] == 1 && p[i + 1] != 0)
				{
					return;
				}
			}
			if (p[h - 1] == 1) return;

			combs.Add(p.ToArray());
		});

		var combCount = combs.Count;
		// j の右の列に i は可能か
		var u = new long[combCount, combCount];

		for (int i = 0; i < combCount; i++)
		{
			if (combs[i] == null) continue;

			for (int j = 0; j < combCount; j++)
			{
				if (combs[j] == null) continue;

				var ok = true;
				for (int k = 0; k < h; k++)
				{
					if (combs[j][k] == 2)
					{
						if (combs[i][k] != 0 ||
							k != 0 && combs[i][k - 1] == 1)
						{
							ok = false;
							break;
						}
					}
				}
				if (ok) u[i, j] = 1;
			}
		}

		var v = new long[combCount];
		v[0] = 1;

		var uw = MPow(u, w);
		v = MMul(uw, v);

		return Enumerable.Range(0, combCount)
			.Where(i => combs[i] != null && !combs[i].Contains(2))
			.Sum(i => v[i]) % M;
	}

	const long M = 998244353;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;

	public static long[,] Unit(int n)
	{
		var r = new long[n, n];
		for (var i = 0; i < n; ++i) r[i, i] = 1;
		return r;
	}

	public static long[,] MPow(long[,] b, long i)
	{
		var r = Unit(b.GetLength(0));
		for (; i != 0; b = MMul(b, b), i >>= 1) if ((i & 1) != 0) r = MMul(r, b);
		return r;
	}

	public static long[,] MMul(long[,] a, long[,] b)
	{
		var n = a.GetLength(0);
		var r = new long[n, n];
		for (var i = 0; i < n; ++i)
			for (var j = 0; j < n; ++j)
				for (var k = 0; k < n; ++k)
					r[i, j] = MInt(r[i, j] + a[i, k] * b[k, j]);
		return r;
	}

	public static long[] MMul(long[,] a, long[] v)
	{
		var n = v.Length;
		var r = new long[n];
		for (var i = 0; i < n; ++i)
			for (var k = 0; k < n; ++k)
				r[i] = MInt(r[i] + a[i, k] * v[k]);
		return r;
	}

	public static void Power<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2);
				else action(p);
			}
		}
	}
}
