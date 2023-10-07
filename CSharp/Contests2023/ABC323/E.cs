using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x) = Read2();
		var t = Read();

		MInt r = 0;
		var dp = new MInt[x + 1];
		dp[0] = 1;
		var ninv = new MInt(1) / n;

		for (int i = 0; i <= x; i++)
		{
			var v = dp[i] * ninv;
			if (v.V == 0) continue;

			if (x < i + t[0]) r += v;

			for (int k = 0; k < n; k++)
			{
				var ni = i + t[k];
				if (ni <= x) dp[ni] += v;
			}
		}
		return r;
	}
}

struct MInt
{
	//const long M = 1000000007;
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
