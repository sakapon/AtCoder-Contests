using System;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var x = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		MInt f = MInt.MFactorial(n - 1);
		var dp = new MInt[n];
		for (int i = 1; i < n; i++)
			dp[i] = dp[i - 1] + f / i;

		MInt r = 0;
		for (int i = 1; i < n; i++)
			r += dp[i] * (x[i] - x[i - 1]);
		Console.WriteLine(r.V);
	}
}

struct MInt
{
	const int M = 1000000007;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;
	public static MInt operator /(MInt x, MInt y) => x * y.Inv();

	public MInt Inv() => MPow(V, M - 2);

	static long MPow(long b, int i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}

	public static long MFactorial(int n) { for (long x = 1, i = 1; ; x = x * ++i % M) if (i >= n) return x; }
}
