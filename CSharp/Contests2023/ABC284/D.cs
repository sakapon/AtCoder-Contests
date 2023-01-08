using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		if (ps == null)
		{
			ps = GetPrimes(3000000);
		}

		foreach (var p in ps)
		{
			var p2 = (long)p * p;
			if (n % p2 == 0)
			{
				var q = n / p2;
				return $"{p} {q}";
			}
		}

		foreach (var q in ps)
		{
			if (n % q == 0)
			{
				var p2 = n / q;
				var p = First(1, 1L << 32, x => x * x >= p2);
				return $"{p} {q}";
			}
		}

		return -1;
	}

	static int[] ps;

	static int[] GetPrimes(int n)
	{
		var b = new bool[n + 1];
		for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
		var r = new List<int>();
		for (int x = 2; x <= n; ++x) if (!b[x]) r.Add(x);
		return r.ToArray();
	}

	public static BigInteger First(BigInteger l, BigInteger r, Func<BigInteger, bool> f)
	{
		BigInteger m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
