using System;
using System.Linq;

class C
{
	static void Main()
	{
		var A = 1000000;
		Console.ReadLine();
		var a = new int[A + 1];
		foreach (var x in Console.ReadLine().Split().Select(int.Parse)) a[x]++;

		var c = new MInt[A + 1];
		for (var d = 1; d <= A; d++) c[d] = new MInt(d).Inv();
		for (var d = 1; d <= A / 2; d++)
			for (var m = 2 * d; m <= A; m += d) c[m] -= c[d];

		var h = new MInt(2).Inv();
		MInt s = 0;
		for (var d = 1; d <= A; d++)
		{
			MInt si = 0, si2 = 0;
			for (var m = d; m <= A; m += d)
			{
				if (a[m] == 0) continue;
				si += ((MInt)a[m]) * m;
				si2 += ((MInt)a[m]) * m * m;
			}
			s += (si * si - si2) * h * c[d];
		}
		Console.WriteLine(s.V);
	}
}

struct MInt
{
	const int M = 998244353;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;
	public static MInt operator /(MInt x, MInt y) => x * y.Inv();

	public MInt Pow(int i) => MPow(V, i);
	public MInt Inv() => MPow(V, M - 2);

	static long MPow(long b, int i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
}
