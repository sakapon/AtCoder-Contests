using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		for (long n = h[0], M = h[1], p = h[2], r = 1; ; n = n * n % M)
		{
			if (p % 2 > 0) r = r * n % M;
			if ((p /= 2) < 1) { Console.WriteLine(r); return; }
		}
	}
}
