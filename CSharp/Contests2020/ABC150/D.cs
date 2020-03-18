using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var m = h[1];
		var a = Array.ConvertAll(Read(), x => x / 2);

		long l = a[0];
		for (int i = 1; i < a.Length; i++)
			if ((l = Lcm(l, a[i])) > m) { Console.WriteLine(0); return; }

		Console.WriteLine(Array.Exists(a, x => l / x % 2 == 0) ? 0 : (m / l + 1) / 2);
	}

	static long Gcd(long x, long y) { for (long r; (r = x % y) > 0; x = y, y = r) ; return y; }
	static long Lcm(long x, long y) => x / Gcd(x, y) * y;
}
