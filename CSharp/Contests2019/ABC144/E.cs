using System;
using System.Linq;

class E
{
	static long[] Read() => Console.ReadLine().Split().Select(long.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var a = Read().OrderBy(x => x).ToArray();
		var f = Read().OrderBy(x => -x).ToArray();

		Console.WriteLine(First(0, a.Last() * f[0], m => a.Zip(f, (x, y) => Math.Max(0, x - m / y)).Sum() <= h[1]));
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
