using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new List<bool>();
		var set = new HashSet<string>();
		for (int i = 0; i < n; i++)
		{
			var q = Console.ReadLine().Split();
			if (q[0][0] == 'i')
				set.Add(q[1]);
			else
				r.Add(set.Contains(q[1]));
		}
		Console.WriteLine(string.Join("\n", r.Select(x => x ? "yes" : "no")));
	}
}
