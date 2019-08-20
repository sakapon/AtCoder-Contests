using System;
using System.Linq;
using System.Numerics;

class F
{
	static void Main()
	{
		var M = 1000000007;
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();
		long L = a[0], R = a[1];
		var c = 0L;

		var pl = (int)Math.Ceiling(Math.Log(L, 2));
		var pr = (int)Math.Floor(Math.Log(R + 1, 2));
		if (pl < pr)
			c += (long)((BigInteger.Pow(3, pr) - BigInteger.Pow(3, pl)) / 2 % M);

		var max1 = (long)Math.Pow(2, pl) - 1;
		for (var x = L; x <= max1; x++)
		{
			for (var y = x; y <= max1; y++)
				if (y - x == (y ^ x)) c++;
			c %= M;
		}

		var min2 = (long)Math.Pow(2, pr);
		for (var x = min2; x <= R; x++)
		{
			for (var y = x; y <= R; y++)
				if (y - x == (y ^ x)) c++;
			c %= M;
		}
		Console.WriteLine(c);
	}
}
