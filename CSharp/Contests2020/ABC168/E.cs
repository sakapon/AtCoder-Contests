using System;
using System.Collections.Generic;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var d = new Map<(long, long), (int c1, int c2)>();
		var c00 = 0;

		foreach (var p in ps)
		{
			var (a, b) = p;

			if ((a, b) == (0, 0))
			{
				c00++;
			}
			else if (b == 0)
			{
				var (c1, c2) = d[(1, 0)];
				d[(1, 0)] = (c1 + 1, c2);
			}
			else if (a == 0)
			{
				var (c1, c2) = d[(1, 0)];
				d[(1, 0)] = (c1, c2 + 1);
			}
			else
			{
				if (b < 0) (a, b) = (-a, -b);
				var gcd = Gcd(Math.Abs(a), Math.Abs(b));
				a /= gcd;
				b /= gcd;

				var isQ1 = a > 0;
				var key = isQ1 ? (a, b) : (b, -a);
				var (c1, c2) = d[key];
				d[key] = isQ1 ? (c1 + 1, c2) : (c1, c2 + 1);
			}
		}

		MInt r = 1, two = 2;
		foreach (var (c1, c2) in d.Values)
			r *= two.Pow(c1) + two.Pow(c2) - 1;
		Console.WriteLine(r - 1 + c00);
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
}

class Map<TK, TV> : Dictionary<TK, TV>
{
	TV _v0;
	public Map(TV v0 = default(TV)) { _v0 = v0; }

	public new TV this[TK key]
	{
		get { return ContainsKey(key) ? base[key] : _v0; }
		set { base[key] = value; }
	}
}

struct MInt
{
	//const long M = 998244353;
	const long M = 1000000007;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }
	public override string ToString() => $"{V}";
	public static implicit operator MInt(long v) => new MInt(v);

	public static MInt operator -(MInt x) => -x.V;
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;
	public static MInt operator /(MInt x, MInt y) => x.V * y.Inv().V;

	public static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	public MInt Pow(long i) => MPow(V, i);
	public MInt Inv() => MPow(V, M - 2);
}
