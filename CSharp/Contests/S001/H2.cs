using System;
using System.Linq;

class H2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var b = Enumerable.Range(n + 1, n).ToArray();
		foreach (var x in a) b[~Array.BinarySearch(b, x)] = x;
		Console.WriteLine(b.Count(x => x <= n));
	}
}
