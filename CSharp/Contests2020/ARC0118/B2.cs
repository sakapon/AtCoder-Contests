using System;
using System.Linq;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n]
			.Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray())
			.Select(x => new { l = x[0] - x[1], r = x[0] + x[1] })
			.OrderBy(x => x.l);

		int M = 2000000000, j;
		var r = Enumerable.Range(M, n + 1).ToArray();
		r[0] = int.MinValue;
		foreach (var x in a)
		{
			if ((j = Array.BinarySearch(r, x.l + 1)) < 0) j = ~j;
			r[j] = Math.Min(r[j], x.r);
		}
		Console.WriteLine(r.Count(x => x <= M) - 1);
	}
}
