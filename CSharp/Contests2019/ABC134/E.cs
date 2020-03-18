using System;
using System.Collections.Generic;

class E
{
	static int Read() => int.Parse(Console.ReadLine());
	static void Main()
	{
		var a = Array.ConvertAll(new int[Read()], _ => Read());

		var l = new List<int>();
		int j;
		foreach (var v in a)
			if ((j = First(0, l.Count, i => l[i] < v)) < l.Count) l[j] = v;
			else l.Add(v);
		Console.WriteLine(l.Count);
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
