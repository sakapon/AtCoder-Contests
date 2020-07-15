using System;

class D
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(((MInt)2).Pow(h[0]) - MInt.MNcr(h[0], h[1]) - MInt.MNcr(h[0], h[2]) - 1);
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

	public static long MFactorial(int n) { for (long x = 1, i = 1; ; x = x * ++i % M) if (i >= n) return x; }
	public static long MNpr(int n, int r)
	{
		if (n < r) return 0;
		for (long x = 1, i = n - r; ; x = x * ++i % M) if (i >= n) return x;
	}
	public static MInt MNcr(int n, int r) => n < r ? 0 : n - r < r ? MNcr(n, n - r) : (MInt)MNpr(n, r) / MFactorial(r);
}
