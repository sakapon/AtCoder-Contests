using System;
using System.Linq;

class B
{
	static long[] Read() => Console.ReadLine().Split().Select(long.Parse).ToArray();
	static void Main()
	{
		var av = Read();
		long a = av[0], v = av[1];
		var bw = Read();
		long b = bw[0], w = bw[1];
		var t = long.Parse(Console.ReadLine());

		if (a < b)
			Console.WriteLine(b + w * t <= a + v * t ? "YES" : "NO");
		else
			Console.WriteLine(a - v * t <= b - w * t ? "YES" : "NO");
	}
}
