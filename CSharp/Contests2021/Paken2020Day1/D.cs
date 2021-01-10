using System;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2L();

		if (k <= n) return k * k * k;
		if (k <= 2 * n)
		{
			var d = k - n;
			return k * k * k - 3 * d * d * d;
		}
		if (k <= 3 * n)
		{
			var d = 3 * n - k;
			return 6 * n * n * n - d * d * d;
		}
		return 6 * n * n * n;
	}
}
