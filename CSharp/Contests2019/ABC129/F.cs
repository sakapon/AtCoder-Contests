using System;

class F
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long l = h[0], a = h[1], b = h[2];
		MInt.M = (int)h[3];

		MInt r = 0, td = 1;
		for (int d = (a + (l - 1) * b).ToString().Length; d >= a.ToString().Length; d--)
		{
			var d10 = 1L;
			for (int i = 0; i < d; i++) d10 *= 10;
			var n_max = Math.Min(l - 1, (d10 - 1 - a) / b);
			var t_min = d10 / 10 - 1 - a;
			var n_min_ex = t_min < 0 ? -1 : t_min / b;

			long c = a + n_max * b, n = n_max - n_min_ex;
			var d10n = ((MInt)d10).Pow(n);

			var s = (d10n - 1) / (d10 - 1) * c - ((n - 1) * d10n * d10 - n * d10n + d10) / ((MInt)(d10 - 1)).Pow(2) * b;
			r += td * s;
			td *= d10n;
		}
		Console.WriteLine(r.V);
	}
}

struct MInt
{
	public static int M = 1000000007;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;
	public static MInt operator /(MInt x, MInt y) => x * y.Inv();

	public MInt Pow(long i) => MPow(V, i);
	public MInt Inv() => MPow(V, M - 2);

	static long MPow(long b, long i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
}
