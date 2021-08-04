using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.ReadLine();
		var a = Read();
		Console.ReadLine();
		var b = Read();

		var set = new SortedSet<int>(a.Concat(b));
		Console.WriteLine(string.Join("\n", set));
	}
}
