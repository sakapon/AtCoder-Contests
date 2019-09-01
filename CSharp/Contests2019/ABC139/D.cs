using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		Console.WriteLine(Perm(n, n).Max(p => p.Select((pi, i) => i % pi).Sum()));
	}

	static IEnumerable<int[]> Perm(int n, int r) => Perm(new SortedSet<int>(Enumerable.Range(1, n)), r, r);
	static IEnumerable<int[]> Perm(SortedSet<int> set, int r, int k)
	{
		if (r == 0) return new[] { new int[k] };
		if (r == 1) return set.Select(i => { var a = new int[k]; a[k - 1] = i; return a; });

		return set.ToArray().SelectMany(i =>
		{
			set.Remove(i);
			var p = Perm(set, r - 1, k).Select(a => { a[k - r] = i; return a; }).ToArray();
			set.Add(i);
			return p;
		});
	}
}
