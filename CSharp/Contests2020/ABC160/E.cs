using System;
using System.Linq;

class E
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int x = h[0], y = h[1];
		var a = Read();
		var b = Read();
		var c = Read();
		Console.WriteLine(a.OrderBy(v => -v).Take(x).Concat(b.OrderBy(v => -v).Take(y)).Concat(c).OrderBy(v => -v).Take(x + y).Sum(v => (long)v));
	}
}
