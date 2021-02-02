using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, m, k) = Read3();

		var ncrs = MNcrs(n - 1);

		MInt r = 0;
		for (int i = n - 1 - k; i < n; i++)
			r += ncrs[i] * m * MInt.MPow(m - 1, i);
		Console.WriteLine(r);
	}

	static MInt[] MNcrs(int n)
	{
		var c = new MInt[n + 1];
		c[0] = 1;
		for (int i = 0; i < n; ++i) c[i + 1] = c[i] * (n - i) / (i + 1);
		return c;
	}
}

struct MInt
{
	const long M = 998244353;
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
