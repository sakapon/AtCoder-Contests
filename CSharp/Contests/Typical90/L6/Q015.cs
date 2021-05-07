using System;

class Q015
{
	const long M = 1000000007;
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var mc = new MCombination(n);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int k = 1; k <= n; k++)
		{
			var r = 0L;
			for (int a = 1; a <= n; a++)
			{
				var p = n - (k - 1) * (a - 1);
				if (p < a) break;
				r += mc.MNcr(p, a);
			}
			Console.WriteLine(r % M);
		}
		Console.Out.Flush();
	}
}

public class MCombination
{
	const long M = 1000000007;
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
}
