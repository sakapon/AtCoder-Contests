using System;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var a = Read().Distinct().Select(x => x / 2).ToArray();

		long lcm = a[0];
		for (int i = 1; i < a.Length; i++)
		{
			lcm = Lcm(lcm, a[i]);
			if (lcm > m) { Console.WriteLine(0); return; }
		}

		if (a.Any(x => lcm / x % 2 == 0)) { Console.WriteLine(0); return; }
		Console.WriteLine((int)((m / lcm + 1) / 2));
	}

	static long Gcd(long x, long y) { for (long r; (r = x % y) > 0; x = y, y = r) ; return y; }
	static long Lcm(long x, long y) => x / Gcd(x, y) * y;
}
