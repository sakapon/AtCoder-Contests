using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new SortedSet<int>();
		var d = new Dictionary<int, int>();
		var count = 0;

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (n-- > 0)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				set.Add(q[1]);
				if (d.ContainsKey(q[1])) d[q[1]]++;
				else d[q[1]] = 1;
				Console.WriteLine(++count);
			}
			else if (q[0] == 1)
				Console.WriteLine(d.ContainsKey(q[1]) ? d[q[1]] : 0);
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
				foreach (var x in set.GetViewBetween(q[1], q[2]).SelectMany(x => Enumerable.Repeat(x, d[x])))
					Console.WriteLine(x);
		}
		Console.Out.Flush();
	}
}
