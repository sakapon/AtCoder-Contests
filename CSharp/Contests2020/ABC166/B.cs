using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		Console.WriteLine(h[0] - new int[h[1]].SelectMany(_ => { Console.ReadLine(); return Read(); }).Distinct().Count());
	}
}
