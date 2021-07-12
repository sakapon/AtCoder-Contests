using System;

class Q090A
{
	const long M = 998244353;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2L();

		if (k == 1) return CreateSeqWithMod((int)n + 2, M)[n + 2];

		return -1;
	}

	public static long[] CreateSeqWithMod(int nLast, long mod)
	{
		var a = new long[nLast + 1];
		a[1] = 1;
		for (int i = 2; i <= nLast; i++)
			a[i] = (a[i - 1] + a[i - 2]) % mod;
		return a;
	}
}
