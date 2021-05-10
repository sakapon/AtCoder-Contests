using System;

class Q020
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (a, b, c) = Read3L();
		return a < Pow(c, b);
	}

	public static long Pow(long b, long i)
	{
		for (long r = 1; ; b *= b)
		{
			if ((i & 1) != 0) r *= b;
			if ((i >>= 1) == 0) return r;
		}
	}
}
