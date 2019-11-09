using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var d = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		if (d[0] > 0 || d.Skip(1).Any(x => x == 0)) { Console.WriteLine(0); return; }
		var c = d.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		var max = c.Keys.Max();
		if (c.Count <= max) { Console.WriteLine(0); return; }

		var r = 1L;
		for (int i = 0; i < max; i++) r = MMul(r, MPow(c[i], c[i + 1]));
		Console.WriteLine(r);
	}

	const int M = 998244353;
	static long MPow(long b, int i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
	static long MMul(long x, long y) => x * y % M;
}
