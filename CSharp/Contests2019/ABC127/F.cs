using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var qs = new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray());

		long m = 0, a = 0, b = 0;
		var l = new List<long>();
		var r = new List<string>();
		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				var toLeft = l.Count % 2 == 1 && q[1] < m;
				var i = l.BinarySearch(q[1]);
				if (i < 0) i = ~i;
				l.Insert(i, q[1]);

				var m2 = l[(l.Count - 1) / 2];
				if (toLeft) a += m - m2;
				a += Math.Abs(m2 - q[1]);
				m = m2;

				b += q[2];
			}
			else r.Add($"{m} {a + b}");
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
