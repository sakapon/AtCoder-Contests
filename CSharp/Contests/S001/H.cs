using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var l = new List<int> { int.MaxValue };
		foreach (var x in a)
			if (x > l.Last())
				l.Add(x);
			else
				l[~l.BinarySearch(x)] = x;
		Console.WriteLine(l.Count);
	}
}
