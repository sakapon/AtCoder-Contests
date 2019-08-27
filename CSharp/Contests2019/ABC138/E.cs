using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var d = Enumerable.Range('a', 26).ToDictionary(x => (char)x, x => new List<int>());
		for (var i = 0; i < s.Length; i++) d[s[i]].Add(i);

		int turn = 0, si = -1;
		foreach (var c in t)
		{
			if (!d[c].Any()) { Console.WriteLine(-1); return; }

			var ci = d[c].BinarySearch(si + 1);
			if (ci < 0) ci = ~ci;
			if (ci < d[c].Count)
			{
				si = d[c][ci];
			}
			else
			{
				turn++;
				si = d[c][0];
			}
		}
		Console.WriteLine((long)s.Length * turn + si + 1);
	}
}
