using System;
using System.Linq;

class A
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		Console.ReadLine();
		var s = Read();
		Console.ReadLine();
		var t = Read();
		Console.WriteLine(s.Intersect(t).Count());
	}
}
