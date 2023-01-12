using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class P066
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		// x * x - d * y * y = 1
		const int dmax = 1000;
		var sqset = Enumerable.Range(1, 100).Select(i => (long)i * i).ToHashSet();

		var r = 0;
		BigInteger xmax = 0;

		for (int d = 1; d <= dmax; d++)
		{
			if (sqset.Contains(d)) continue;

			var (x, _) = Pell(d);
			if (xmax < x)
			{
				r = d;
				xmax = x;
			}
		}
		return r;
	}

	static (BigInteger x, BigInteger y) Pell(int n)
	{
		var cont = ContinuedSqrt(n);

		if (cont.Length % 2 == 1)
		{
			return ConvergentBig(cont[..^1]);
		}
		else
		{
			var (x, y) = ConvergentBig(cont[..^1]);
			return (x * x + n * y * y, 2 * x * y);
		}
	}

	static long[] ContinuedSqrt(int n)
	{
		var r = new List<long>();
		var set = new HashSet<Irrational>();
		var x = new Irrational(n, 0, 1);

		while (true)
		{
			var (i, t) = Next(x);
			r.Add(i);
			x = t;
			if (set.Contains(x)) return r.ToArray();
			set.Add(x);
		}
	}

	// (整数部分, 小数部分の逆数)
	static (long, Irrational) Next(Irrational x)
	{
		long i = (long)Math.Floor(x.Value);
		return (i, new Irrational(x.a, x.b - i * x.c, x.c).Inverse);
	}

	// a: 連分数の標準形
	static (BigInteger num, BigInteger denom) ConvergentBig(long[] a)
	{
		BigInteger u = a[^1], v = 1;

		for (int i = a.Length - 2; i >= 0; i--)
		{
			(u, v) = (v + u * a[i], u);
		}
		return (u, v);
	}
}
