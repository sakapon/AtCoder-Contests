using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var d = Enumerable.Range('a', 26).ToDictionary(x => (char)x, x => new HashSet<int>());
		for (var i = 0; i < s.Length; i++) d[s[i]].Add(i);

		int turn = 0, si = -1;
		foreach (var c in t)
		{
			if (d[c].Count == 0) { Console.WriteLine(-1); return; }

			var found = false;
			foreach (var ci in d[c])
			{
				if (si < ci)
				{
					si = ci;
					found = true;
					break;
				}
			}
			if (!found)
			{
				turn++;
				si = d[c].First();
			}
		}
		Console.WriteLine((long)s.Length * turn + si + 1);
	}
}
