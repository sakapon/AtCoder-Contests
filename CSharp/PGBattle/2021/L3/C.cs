using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var r = Read();

		var dp = new MInt[n + (1 << 18)];
		dp[1] = 1;
		MInt t = 0, d = 0;
		var limit = new MInt[n + (1 << 18)];

		for (int i = 1; i < dp.Length; i++)
		{
			if (i > 1)
			{
				dp[i] = t;
				t -= d;
				d -= limit[i];
			}

			if (i <= n)
			{
				var ri = r[i - 1];
				var sum = Sum1(ri);

				var unit = dp[i] / sum;
				t += unit * ri;
				d += unit;
				limit[i + ri] += unit;
			}
		}

		MInt e = 0;
		for (int i = n + 1; i < dp.Length; i++)
		{
			e += i * dp[i];
		}
		return e;
	}

	static MInt Sum1(int k)
	{
		return (MInt)k * (k + 1) / 2;
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
