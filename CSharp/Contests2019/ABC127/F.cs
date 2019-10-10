using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var qs = new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var l = new List<int>();
		var b = 0L;
		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				var i = l.BinarySearch(q[1]);
				if (i < 0) i = ~i;
				l.Insert(i, q[1]);
				b += q[2];
			}
			else
			{
				var x = l[(l.Count - 1) / 2];
				Console.WriteLine($"{x} {l.Sum(a => (long)Math.Abs(x - a)) + b}");
			}
		}
	}
}
