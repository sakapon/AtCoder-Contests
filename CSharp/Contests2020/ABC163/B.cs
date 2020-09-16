using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		Console.WriteLine(Math.Max(h[0] - Read().Sum(), -1));
	}
}
