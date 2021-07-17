using System;

class Q038
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b) = Read2L();

		var ag = a / Gcd(a, b);

		if (ag > long.MaxValue / b) return "Large";
		var l = ag * b;

		if (l > 1000000000000000000) return "Large";
		return l;
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
