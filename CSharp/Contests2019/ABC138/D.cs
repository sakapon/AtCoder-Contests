using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var rs = new int[h[0] - 1].Select(_ => read()).ToArray();
		map = new int[h[0] + 1].Select(_ => new List<int>()).ToArray();
		foreach (var r in rs)
		{
			map[r[0]].Add(r[1]);
			map[r[1]].Add(r[0]);
		}
		ops = new int[h[1]].Select(_ => read()).GroupBy(a => a[0]).ToDictionary(g => g.Key, g => g.Sum(a => a[1]));

		c = new int[h[0] + 1];
		FindPoints(1, 0);
		Console.WriteLine(string.Join(" ", c.Skip(1)));
	}

	static List<int>[] map;
	static Dictionary<int, int> ops;
	static int[] c;

	static void FindPoints(int p, int p0)
	{
		c[p] = c[p0] + (ops.ContainsKey(p) ? ops[p] : 0);
		foreach (var q in map[p]) if (q != p0) FindPoints(q, p);
	}
}
