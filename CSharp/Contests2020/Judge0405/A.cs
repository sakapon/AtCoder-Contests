using System;
using System.Linq;

class A
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		Console.WriteLine(Math.Min(Math.Max(h[0], h[1]), h[2]));
	}
}
