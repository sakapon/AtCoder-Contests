using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long a = h[0], b = h[1], x = h[2], M = 1000000000;

		if (a * M + b * 10 <= x) { Console.WriteLine(M); return; }

		M--;
		for (int d = 9; d > 0; d--)
		{
			var d10 = (int)Math.Pow(10, d - 1);
			var n = Math.Min(M, (x - b * d) / a);
			if (n >= d10) { Console.WriteLine(n); return; }
			M -= 9 * d10;
		}
		Console.WriteLine(0);
	}
}
