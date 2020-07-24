using System;
using System.Collections.Generic;
using System.Linq;

class J
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var a = Read();

		var c = new int[n + 1];
		c[0] = 1 << 30;

		var r = new List<int>();
		foreach (var x in a)
		{
			var id = First(1, n + 1, i => c[i] < x);
			r.Add(id <= n ? id : -1);
			if (id <= n) c[id] = x;
		}
		Console.WriteLine(string.Join("\n", r));
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
