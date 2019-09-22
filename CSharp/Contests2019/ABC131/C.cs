using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(long.Parse).ToArray();
		long a = h[0], b = h[1], c = h[2], d = h[3];

		Func<long, long> count = x => x - x / c - x / d + x / Lcm(c, d);
		Console.WriteLine(count(b) - count(a - 1));
	}

	static long Gcd(long x, long y) { for (long r; (r = x % y) > 0; x = y, y = r) ; return y; }
	static long Lcm(long x, long y) => x / Gcd(x, y) * y;
}
