using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		map = Enumerable.Range(0, h[0] - 1).Select(i => read()).GroupBy(r => r[0]).ToDictionary(g => g.Key, g => g.Select(r => r[1]).ToArray());
		ops = Enumerable.Range(0, h[1]).Select(i => read()).GroupBy(r => r[0]).ToDictionary(g => g.Key, g => g.Sum(r => r[1]));

		c = new int[h[0]];
		FindPoints(1);
		Console.WriteLine(string.Join(" ", c));
	}

	static Dictionary<int, int[]> map;
	static Dictionary<int, int> ops;
	static int[] c;
	static int sum;

	static void FindPoints(int p)
	{
		if (ops.ContainsKey(p)) sum += ops[p];
		c[p - 1] = sum;
		if (map.ContainsKey(p)) foreach (var r in map[p]) FindPoints(r);
		if (ops.ContainsKey(p)) sum -= ops[p];
	}
}
