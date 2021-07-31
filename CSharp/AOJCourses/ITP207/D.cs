using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new List<int>();
		var set = new SortedSet<int>();
		var d = new Dictionary<int, int>();
		var count = 0;

		for (int i = 0; i < n; i++)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				set.Add(q[1]);
				if (d.ContainsKey(q[1])) d[q[1]]++;
				else d[q[1]] = 1;
				r.Add(++count);
			}
			else if (q[0] == 1)
				r.Add(d.ContainsKey(q[1]) ? d[q[1]] : 0);
			else if (q[0] == 2)
			{
				if (d.ContainsKey(q[1]))
				{
					count -= d[q[1]];
					d.Remove(q[1]);
					set.Remove(q[1]);
				}
			}
			else
				r.AddRange(set.GetViewBetween(q[1], q[2]).SelectMany(x => Enumerable.Repeat(x, d[x])));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
