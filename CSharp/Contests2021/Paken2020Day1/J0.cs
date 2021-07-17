using System;
using System.IO;

class J0
{
	static void Main()
	{
		using var writer = File.CreateText("J0.txt");

		int count = 0;
		for (int p = 1; ; p++)
			for (int q = 1; q < p; q++)
			{
				var (a, b, c) = (2 * p * q, p * p - q * q, p * p + q * q);
				if (Gcd(a, b) != 1) continue;
				if (a > b) (a, b) = (b, a);
				writer.WriteLine($"{a} {b} {c}");
				if (++count == 100000) return;
			}
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
