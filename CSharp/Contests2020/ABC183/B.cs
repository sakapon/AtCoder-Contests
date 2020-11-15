using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = ReadL();
		long x1 = h[0], y1 = h[1], x2 = h[2], y2 = h[3];
		Console.WriteLine((double)(x1 * y2 + x2 * y1) / (y1 + y2));
	}
}
