using System;
using System.Linq;

class A
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var st = Console.ReadLine().Split();
		var ab = Read();
		var u = Console.ReadLine();

		ab[u == st[0] ? 0 : 1]--;
		Console.WriteLine($"{ab[0]} {ab[1]}");
	}
}
