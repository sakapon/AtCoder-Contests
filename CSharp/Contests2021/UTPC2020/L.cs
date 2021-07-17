using System;
using System.Linq;

static class L
{
	const long p = 299993, g = 5;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long x, long y) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		// 4 乗根
		var r4 = MPow(g, (p - 1) / 4);
		//var r4 = (long)Enumerable.Range(0, (int)p).First(x => (long)x * x % p == p - 1);

		var (n, z) = Read2L();
		var ps = Array.ConvertAll(new bool[n], _ =>
		{
			var (x, y) = Read2L();
			return (z: MInt(x + y * r4), z_: MInt(x - y * r4));
		});

		var prod = Enumerable.Range(0, (int)p)
			.Select(s => ps.Select(t => MInt(s - t.z)).Aggregate((x, y) => x * y % p))
			.ToArray()
			.Tally(p - 1);
		var prod_ = Enumerable.Range(0, (int)p)
			.Select(s => ps.Select(t => MInt(s - t.z_)).Aggregate((x, y) => x * y % p))
			.ToArray()
			.Tally(p - 1);

		if (z == 0) return p * p - (p - prod[0]) * (p - prod_[0]);
		return Enumerable.Range(1, (int)p - 1).Sum(x => prod[x] * prod_[z * MInv(x) % p]);
	}

	static long MInt(long x) => (x %= p) < 0 ? x + p : x;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % p, i >>= 1) if ((i & 1) != 0) r = r * b % p;
		return r;
	}
	static long MInv(long x) => MPow(x, p - 2);

	static long[] Tally(this long[] a, long max)
	{
		var r = new long[max + 1];
		foreach (var x in a) ++r[x];
		return r;
	}
}
