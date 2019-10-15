using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int m = h[0], k = h[1], n = 0;
		for (long f = 1, M = 0; M < m; n++, f *= 2 * k + 1) M += k * f;
		Console.WriteLine(n);
	}
}
