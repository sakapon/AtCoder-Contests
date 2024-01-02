using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = Read();

		if (k == 0) return 1;

		MInt r = 1;
		var mc = new MCombination(n);

		var gs = Enumerable.Range(0, n).GroupBy(i => i % k);
		foreach (var g in gs)
		{
			var b = g.Select(i => a[i]).ToArray();
			r *= mc.MFactorial(b.Length);

			foreach (var h in b.GroupBy(x => x))
			{
				r *= mc.MInvFactorial(h.Count());
			}
		}
		return r;
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

public class MCombination
{
	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);

	static long[] MFactorials(int n)
	{
		var f = new long[n + 1];
		f[0] = 1;
		for (int i = 1; i <= n; ++i) f[i] = f[i - 1] * i % M;
		return f;
	}

	// nPr, nCr を O(1) で求めるため、階乗を O(n) で求めておきます。
	long[] f, f_;
	public MCombination(int nMax)
	{
		f = MFactorials(nMax);
		f_ = Array.ConvertAll(f, MInv);
	}

	public long MFactorial(int n) => f[n];
	public long MInvFactorial(int n) => f_[n];
	public long MNpr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M;
	public long MNcr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M * f_[r] % M;

	// nMax >= 2n としておく必要があります。
	public long MCatalan(int n) => f[2 * n] * f_[n] % M * f_[n + 1] % M;
}
