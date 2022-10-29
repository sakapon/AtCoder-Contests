using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();

		var dp = new MInt[n + 1];
		var dt = new MInt[n + 1];
		dp[0] = 1;

		var m_ = new MInt(m).Inv();

		while (k-- > 0)
		{
			for (int i = 0; i < n; i++)
			{
				var nv = dp[i] * m_;
				if (nv.V == 0) continue;

				for (int j = 1; j <= m; j++)
				{
					var ni = i + j;
					if (ni > n) ni = 2 * n - ni;
					dt[ni] += nv;
				}
			}

			dt[n] += dp[n];
			(dp, dt) = (dt, dp);
			Array.Clear(dt, 0, dt.Length);
		}

		return dp[n];
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
