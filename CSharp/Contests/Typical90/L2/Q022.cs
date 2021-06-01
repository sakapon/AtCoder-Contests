using System;
using System.Linq;

class Q022
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = ReadL();

		var g = a.Aggregate(Gcd);
		return a.Sum(x => x / g - 1);
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
