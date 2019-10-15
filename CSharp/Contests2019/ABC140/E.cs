using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var q = new int[n + 1];
		for (int i = 0; i < n; i++) q[p[i]] = i;

		var bigs = new List<int> { q[n] };
		var s = 0L;
		for (int i = n - 1; i > 0; i--)
		{
			var bi = ~bigs.BinarySearch(q[i]);
			if (bi > 0)
			{
				var li = bigs[bi - 1];
				var li2 = bi - 2 >= 0 ? bigs[bi - 2] : -1;
				var ri2 = bi < bigs.Count ? bigs[bi] : n;
				s += (long)i * (li - li2) * (ri2 - q[i]);
			}
			if (bi < bigs.Count)
			{
				var ri = bigs[bi];
				var ri2 = bi + 1 < bigs.Count ? bigs[bi + 1] : n;
				var li2 = bi - 1 >= 0 ? bigs[bi - 1] : -1;
				s += (long)i * (q[i] - li2) * (ri2 - ri);
			}
			bigs.Insert(bi, q[i]);
		}
		Console.WriteLine(s);
	}
}
