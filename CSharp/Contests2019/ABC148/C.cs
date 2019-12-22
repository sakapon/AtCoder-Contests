using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(Lcm(h[0], h[1]));
	}

	static long Gcd(long x, long y) { for (long r; (r = x % y) > 0; x = y, y = r) ; return y; }
	static long Lcm(long x, long y) => x / Gcd(x, y) * y;
}
