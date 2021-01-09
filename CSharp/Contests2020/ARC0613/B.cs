using System;

class B
{
	static long[] Read() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var av = Read();
		long a = av[0], v = av[1];
		var bw = Read();
		long b = bw[0], w = bw[1];
		var t = long.Parse(Console.ReadLine());

		Console.WriteLine(Math.Abs(a - b) + w * t <= v * t ? "YES" : "NO");
	}
}
