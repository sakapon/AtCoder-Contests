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

		var pa1 = Array.ConvertAll(a, _ => 1L);
		var pa2 = Array.ConvertAll(a, _ => 1L);

		var f = MInt.MFactorials(k);
		var f_ = Array.ConvertAll(f, v => (1 / v).V);

		var p = new long[k + 1];
		for (int x = 0; x <= k; x++)
		{
			var sum = 0L;
			for (int i = 0; i < n; i++)
			{
				sum += pa1[i] * f_[x] % M;
				pa1[i] = pa1[i] * a[i] % M;
			}
			p[x] = sum % M;
		}

		for (int x = 1; x <= k; x++)
		{
			var sum = 0L;
			for (int i = 0; i < n; i++)
			{
				pa2[i] = pa2[i] * (a[i] << 1) % M;
				sum += pa2[i];
			}

			var r = f[x] * Enumerable.Range(0, x + 1).Sum(i => p[i] * p[x - i] % M);
			Console.WriteLine((r - sum) / 2);
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
