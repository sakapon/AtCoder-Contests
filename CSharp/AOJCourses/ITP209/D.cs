using System;
using System.Collections.Generic;
using System.Linq;

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
		foreach (var x in b)
			if (!set.Add(x))
				set.Remove(x);
		if (set.Any()) Console.WriteLine(string.Join("\n", set));
	}
}
