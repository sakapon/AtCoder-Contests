using System;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var c = new MInt[n + 1];
		c[0] = 1;
		for (int i = 1; i <= n; i++) c[i] = c[i - 1] * 2 * (2 * i - 1) / (i + 1);

		MInt r = 1;
		for (int t = 0, i = 0; i < n; i++)
		{
			t++;
			if (i == n - 1 || s[i] != s[i + 1])
			{
				r *= c[t];
				t = 0;
			}
		}
		Console.WriteLine(r.V);
	}
}

struct MInt
{
	const int M = 998244353;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
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
