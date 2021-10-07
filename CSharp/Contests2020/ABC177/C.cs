using System;
using System.Linq;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var s = a.Sum() % M;

		var r = s * s % M;
		r -= a.Sum(x => x * x % M);
		r = MInt(r);
		r = r * MHalf % M;

		return r;
	}

	const long M = 1000000007;
	const long MHalf = (M + 1) / 2;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
