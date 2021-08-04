using System;
using System.Collections.Generic;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.ReadLine();
		var a = Read();
		Console.ReadLine();
		var b = Read();

		var set = new SortedSet<int>(a);
		set.SymmetricExceptWith(b);
		if (set.Count > 0) Console.WriteLine(string.Join("\n", set));
	}
}
