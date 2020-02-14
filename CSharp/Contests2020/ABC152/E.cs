using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var d = new Dictionary<long, int>();
		foreach (var x in a)
			foreach (var o in Factorize(x).GroupBy(p => p).Select(g => new { p = g.Key, c = g.Count() }))
				if (!d.ContainsKey(o.p) || d[o.p] < o.c) d[o.p] = o.c;

		var lcm = d.Select(p => ((MInt)p.Key).Pow(p.Value)).Aggregate((MInt)1, (x, y) => x * y);
		Console.WriteLine(a.Select(x => lcm / x).Aggregate((x, y) => x + y));
	}

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long rn = (long)Math.Ceiling(Math.Sqrt(n)), x = 2; x <= rn && n > 1; ++x)
			while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}

struct MInt
{
	const int M = 1000000007;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;
	public static MInt operator /(MInt x, MInt y) => x * y.Inv();

	public MInt Pow(int i) => MPow(V, i);
	public MInt Inv() => MPow(V, M - 2);
	public override string ToString() => $"{V}";

	static long MPow(long b, int i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
}
