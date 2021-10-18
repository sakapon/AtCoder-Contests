using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = ((int, long))Read2L();

		// 前の列の横長畳により既に確定している状態を部分集合で表します。
		var combs = new bool[1 << h][];
		var t = 0;

		Power(new[] { false, true }, h, p =>
		{
			combs[t++] = p.ToArray();
		});

		var combCount = combs.Length;
		// j の右の列に i は可能か
		var u = new long[combCount, combCount];

		for (int i = 0; i < combCount; i++)
		{
			var si = combs[i];

			for (int j = 0; j < combCount; j++)
			{
				var sj = combs[j];
				Dfs(0);

				void Dfs(int k)
				{
					if (k == h)
					{
						u[i, j]++;
						return;
					}

					if (sj[k])
					{
						if (si[k])
						{
						}
						else
						{
							Dfs(k + 1);
						}
					}
					else
					{
						if (si[k])
						{
							// 横
							Dfs(k + 1);
						}
						else
						{
							// 半畳
							Dfs(k + 1);

							// 縦
							if (k < h - 1 && !sj[k + 1] && !si[k + 1])
							{
								Dfs(k + 2);
							}
						}
					}
				}
			}
		}

		var v = new long[combCount];
		v[0] = 1;

		var uw = MPow(u, w);
		v = MMul(uw, v);
		return v[0];
	}

	const long M = 998244353;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;

	// 正方行列
	public static long[,] Unit(int n)
	{
		var r = new long[n, n];
		for (var i = 0; i < n; ++i) r[i, i] = 1;
		return r;
	}

	// 正方行列
	public static long[,] MPow(long[,] b, long i)
	{
		var r = Unit(b.GetLength(0));
		for (; i != 0; b = MMul(b, b), i >>= 1) if ((i & 1) != 0) r = MMul(r, b);
		return r;
	}

	public static long[,] MMul(long[,] a, long[,] b)
	{
		if (a.GetLength(1) != b.GetLength(0)) throw new InvalidOperationException();
		var n = a.GetLength(0);
		var m = b.GetLength(1);
		var l = a.GetLength(1);
		var r = new long[n, m];
		for (var i = 0; i < n; ++i)
			for (var j = 0; j < m; ++j)
				for (var k = 0; k < l; ++k)
					r[i, j] = MInt(r[i, j] + a[i, k] * b[k, j]);
		return r;
	}

	public static long[] MMul(long[,] a, long[] v)
	{
		if (a.GetLength(1) != v.Length) throw new InvalidOperationException();
		var n = a.GetLength(0);
		var l = v.Length;
		var r = new long[n];
		for (var i = 0; i < n; ++i)
			for (var k = 0; k < l; ++k)
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
