using System;

class L
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main()
	{
		var (q, x) = Read2L();
		var a = Array.ConvertAll(new bool[q], _ => int.Parse(Console.ReadLine()));

		const long M = 1000000007;
		var p10 = PowsL(10, 10);
		var p2 = 1L;

		foreach (var v in a)
		{
			p2 *= 2;
			p2 %= M;

			x += x * (p10[v.ToString().Length] % M);
			x += v * p2;
			x %= M;
		}
		Console.WriteLine(x);
	}

	public static long[] PowsL(long b, int n)
	{
		var p = new long[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b;
		return p;
	}
}
