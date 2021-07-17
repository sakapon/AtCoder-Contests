using System;
using System.Numerics;

class Q038B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b) = Read2L();

		var ag = a / Gcd(a, b);
		var l = (BigInteger)ag * b;

		if (l > BigInteger.Pow(10, 18)) return "Large";
		return l;
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
