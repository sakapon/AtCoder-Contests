using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long n = h[0], k = h[1];
		n %= k;
		Console.WriteLine(Math.Min(n, k - n));
	}
}
