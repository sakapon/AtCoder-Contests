using System;
using System.Linq;

class L
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var q = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray())
			.GroupBy(p => p.Max(), p => p.Min())
			.OrderBy(g => g.Key)
			.SelectMany(g => g.OrderBy(y => -y));

		var lis = Enumerable.Range(1 << 30, n).ToArray();
		int j;
		foreach (var y in q)
			lis[(j = Array.BinarySearch(lis, y)) < 0 ? ~j : j] = y;
		Console.WriteLine(~Array.BinarySearch(lis, 1 << 30));
	}
}
