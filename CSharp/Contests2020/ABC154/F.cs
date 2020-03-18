using System;
using System.Linq;

class F
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int r1 = h[0] - 1, c1 = h[1] - 1, r2 = h[2], c2 = h[3];
		invs = Enumerable.Range(0, c2 + 2).Select(i => ((MInt)i).Inv()).ToArray();
		Console.WriteLine(Sum(r2, c2) + Sum(r1, c1) - Sum(r1, c2) - Sum(r2, c1));
	}

	static MInt[] invs;
	static MInt Sum(int r, int c)
	{
		MInt t = 1, v = 0;
		for (int i = 1; i <= c + 1; i++)
		{
			t *= (r + i) * invs[i];
			v += t;
		}
		return v;
	}
}

struct MInt
{
	const int M = 1000000007;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
	public static MInt operator +(MInt x) => x;
	public static MInt operator -(MInt x) => -x.V;
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
