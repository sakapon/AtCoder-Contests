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
			if (d[c].Count == 0) { Console.WriteLine(-1); return; }

			var ci = Search(d[c], si);
			if (ci != d[c].Count)
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

	static int Search(IList<int> l, int v) => l.Count > 0 ? Search(l, v, 0, l.Count) : 0;
	static int Search(IList<int> l, int v, int start, int count)
	{
		if (count == 1) return start + (v < l[start] ? 0 : 1);
		var c = count / 2;
		var s = start + c;
		return v < l[s] ? Search(l, v, start, c) : Search(l, v, s, count - c);
	}
}
