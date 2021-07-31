using System;
using System.Collections.Generic;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new List<int>();
		var set = new SortedSet<int>();

		for (int i = 0; i < n; i++)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				set.Add(q[1]);
				r.Add(set.Count);
			}
			else if (q[0] == 1)
				r.Add(set.Contains(q[1]) ? 1 : 0);
			else if (q[0] == 2)
				set.Remove(q[1]);
			else
				r.AddRange(set.GetViewBetween(q[1], q[2]));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}
