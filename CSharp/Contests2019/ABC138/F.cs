using System;
using System.Collections;
using System.Linq;
using System.Numerics;

class F
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();
		long L = a[0], R = a[1];

		BigInteger c = 0;
		var pow2 = new long[61];
		var pow3 = new BigInteger[61];
		pow2[0] = 1;
		pow3[0] = 1;
		for (var i = 1; i <= 60; pow2[i] = 2 * pow2[i - 1], pow3[i] = 3 * pow3[i - 1], i++) ;

		for (int i = 0; i < 60; i++)
		{
			var p2l = pow2[i];
			var p2r = pow2[i + 1] - 1;

			if (R < p2l) break;
			if (p2r < L) continue;
			if (L <= p2l && p2r <= R) { c += pow3[i]; continue; }

			if (p2r <= R)
			{
				for (var x = L; x <= p2r; c += pow2[CountBit0(x, i - 1)], x++) ;
			}
			else
			{
				for (var x = Math.Max(L, p2l); x <= R; x++)
					for (var y = x; y <= R; y++)
						if (y - x == (y ^ x)) c++;
			}
		}
		Console.WriteLine(c % 1000000007);
	}

	static int CountBit0(long v, int maxBit)
	{
		var c = 0;
		var b = new BitArray(new[] { (int)v, (int)(v >> 32) });
		for (var i = 0; i <= maxBit; i++) if (!b[i]) c++;
		return c;
	}
}
