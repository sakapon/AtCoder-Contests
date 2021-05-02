using System;
using System.Collections.Generic;
using System.Linq;

class Q026
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var tree = new Tree(n + 1, 1, es);
		var gs = Enumerable.Range(1, n).ToLookup(v => tree.Depths[v] % 2);

		var r = gs[0].Count() >= n / 2 ? gs[0] : gs[1];
		return string.Join(" ", r.Take(n / 2));
	}
}
