using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		Console.WriteLine(MPow(h[0], h[2], h[1]));
	}

	static long MPow(long b, long i, long M)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
}
