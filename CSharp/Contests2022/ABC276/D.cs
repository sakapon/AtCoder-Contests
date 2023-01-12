using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var g = a.Aggregate(Gcd);

		var r = 0;
		foreach (var x in a)
		{
			var d = x / g;

			while (d % 2 == 0)
			{
				d /= 2;
				r++;
			}
			while (d % 3 == 0)
			{
				d /= 3;
				r++;
			}

			if (d != 1) return -1;
		}
		return r;
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
