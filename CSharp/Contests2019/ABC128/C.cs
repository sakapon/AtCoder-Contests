using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var n = h[0];
		var ls = Enumerable.Range(0, h[1]).Select(i => new { i, ss = Console.ReadLine().Split().Select(x => int.Parse(x) - 1).Skip(1).ToArray() }).ToArray();
		var p = Console.ReadLine().Split().Select(x => int.Parse(x) == 0).ToArray();

		var f = Enumerable.Range(0, n + 1).Select(i => (int)Math.Pow(2, i)).ToArray();
		Console.WriteLine(Enumerable.Range(0, f[n]).Count(b => ls.All(l => l.ss.Select(s => (b & f[s]) != 0).Aggregate((x, y) => x ^ y) ^ p[l.i])));
	}
}
