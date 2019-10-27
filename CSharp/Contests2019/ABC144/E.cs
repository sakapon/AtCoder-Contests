using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<long[]> read = () => Console.ReadLine().Split().Select(long.Parse).ToArray();
		var h = read();
		var a = read().OrderBy(x => -x).ToArray();
		var f = read().OrderBy(x => x).ToArray();

		long l = 0, r = a[0] * f.Last(), m;
		while (l < r)
		{
			m = (l + r) / 2;
			if (Enumerable.Range(0, (int)h[0]).Sum(i => Math.Max(0, a[i] - m / f[i])) <= h[1]) r = m;
			else l = m + 1;
		}
		Console.WriteLine(r);
	}
}
