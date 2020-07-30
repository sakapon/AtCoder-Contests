using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var x = h[0] * h[2] - Read().Sum();
		Console.WriteLine(x <= h[1] ? Math.Max(x, 0) : -1);
	}
}
