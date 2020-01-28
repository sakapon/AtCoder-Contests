using System;
using System.Linq;

class L
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var q = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray())
			.Select(p => p[0] < p[1] ? new[] { p[1], p[0] } : p)
			.OrderBy(p => p[0])
			.ThenBy(p => -p[1]);

		var lis = Enumerable.Range(1 << 30, n).ToArray();
		int j;
		foreach (var p in q)
			lis[(j = Array.BinarySearch(lis, p[1])) < 0 ? ~j : j] = p[1];
		Console.WriteLine(~Array.BinarySearch(lis, 1 << 30));
	}
}
