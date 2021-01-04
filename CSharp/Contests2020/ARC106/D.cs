using System;
using System.Linq;

class D
{
	const long M = 998244353;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, k) = Read2();
		var a = Read();

		long Sum(long[] vs)
		{
			long r = 0;
			foreach (var v in vs) r += v;
			return r;
		}

		var f = MInt.MFactorials(k);
		var f_ = Array.ConvertAll(f, v => 1 / v);
		var p = new long[k + 1];
		for (int x = 0; x <= k; x++)
			p[x] = Sum(Array.ConvertAll(a, v => MInt.MPow(v, x) * f_[x].V % M)) % M;

		for (int x = 1; x <= k; x++)
		{
			var r = f[x] * Sum(Enumerable.Range(0, x + 1).Select(i => p[i] * p[x - i] % M).ToArray());
			r -= Sum(Array.ConvertAll(a, v => MInt.MPow(2 * v, x)));
			Console.WriteLine(r / 2);
		}
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

	public static MInt[] MFactorials(int n)
	{
		var f = new MInt[n + 1];
		f[0] = 1;
		for (int i = 0; i < n; ++i) f[i + 1] = f[i] * (i + 1);
		return f;
	}
}
