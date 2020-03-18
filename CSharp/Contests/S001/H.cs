using System;
using System.Linq;

class H
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var l = new int[0].ToList();
		int i;
		foreach (var x in a)
			if ((i = ~l.BinarySearch(x)) < l.Count) l[i] = x;
			else l.Add(x);
		Console.WriteLine(l.Count);
	}
}
