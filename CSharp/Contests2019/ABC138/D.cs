using System;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var map = Enumerable.Range(0, h[0] - 1).Select(i => read()).OrderBy(a => a[0]).ToArray();
		var ops = Enumerable.Range(0, h[1]).Select(i => read()).GroupBy(a => a[0]).ToDictionary(g => g.Key, g => g.Sum(a => a[1]));

		var c = new int[h[0]];
		if (ops.ContainsKey(1)) c[0] = ops[1];
		foreach (var r in map) c[r[1] - 1] = c[r[0] - 1] + (ops.ContainsKey(r[1]) ? ops[r[1]] : 0);
		Console.WriteLine(string.Join(" ", c));
	}
}
