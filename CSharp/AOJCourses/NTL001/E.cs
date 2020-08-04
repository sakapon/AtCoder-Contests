using System;
using System.Linq;

class E
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		int g = Gcd(h[0], h[1]), a = h[0] / g, b = h[1] / g;
		var s0 = ExtendedEuclid(a, b);
		var s = new[] { s0, new[] { s0[0] + b, s0[1] - a }, new[] { s0[0] - b, s0[1] + a } }
			.OrderBy(r => Math.Abs(r[0]) + Math.Abs(r[1]))
			.ThenBy(r => r[0] - r[1])
			.First();
		Console.WriteLine(string.Join(" ", s));
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }

	static long[] ExtendedEuclid(long a, long b)
	{
		if (b == 1) return new[] { 1, 1 - a };
		long r;
		var q = Math.DivRem(a, b, out r);
		var t = ExtendedEuclid(b, r);
		return new[] { t[1], t[0] - q * t[1] };
	}
}
