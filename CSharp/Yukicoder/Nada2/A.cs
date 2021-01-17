using System;

class A
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (a, b, k) = Read3L();
		var lcm = Lcm(a, b);
		Console.WriteLine(First(1, 1L << 60, x => x / a + x / b - x / lcm >= k));
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
	static long Lcm(long a, long b) => a / Gcd(a, b) * b;

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
