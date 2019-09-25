using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var cg_map = Enumerable.Range(0, h[0] + 1).ToArray();
		var gc_map = cg_map.ToDictionary(i => i, i => new List<int> { i });

		foreach (var r in new int[h[1]].Select(_ => read()))
		{
			int g0 = cg_map[r[0]], g1 = cg_map[r[1]];
			if (g0 == g1) continue;
			foreach (var c in gc_map[g1]) cg_map[c] = g0;
			gc_map[g0].AddRange(gc_map[g1]);
			gc_map.Remove(g1);
		}
		Console.WriteLine(gc_map.Count - 1);
	}
}
