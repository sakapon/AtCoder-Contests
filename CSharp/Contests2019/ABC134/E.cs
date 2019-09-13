using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int> read = () => int.Parse(Console.ReadLine());
		var a = new int[read()].Select(_ => read()).ToArray();

		var l = new List<int>();
		for (int i = a.Length - 1; i >= 0; i--)
		{
			var v = a[i];
			var j = SearchForInsert(l, v);
			if (j < l.Count) l[j] = v;
			else l.Add(v);
		}
		Console.WriteLine(l.Count);
	}

	static int SearchForInsert(IList<int> l, int v) => l.Count > 0 ? SearchForInsert(l, v, 0, l.Count) : 0;
	static int SearchForInsert(IList<int> l, int v, int s, int c)
	{
		if (c == 1) return v < l[s] ? s : s + 1;
		int c2 = c / 2, s2 = s + c2;
		return v < l[s2] ? SearchForInsert(l, v, s, c2) : SearchForInsert(l, v, s2, c - c2);
	}
}
