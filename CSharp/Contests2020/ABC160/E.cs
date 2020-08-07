using System;
using System.Linq;

class E
{
	static long[] Read() => Console.ReadLine().Split().Select(long.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int x = (int)h[0], y = (int)h[1];
		var a = Read();
		var b = Read();
		var c = Read();

		var rg = a.OrderBy(v => -v).Take(x).Concat(b.OrderBy(v => -v).Take(y)).OrderBy(v => -v).ToArray();
		var w = c.OrderBy(v => -v).Take(x + y).Concat(new long[Math.Max(0, x + y - h[4])]).Reverse().ToArray();
		Console.WriteLine(Enumerable.Range(0, x + y).Sum(i => Math.Max(rg[i], w[i])));
	}
}
