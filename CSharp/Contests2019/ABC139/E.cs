using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var map = new List<int>[1001000];
		for (var i = 1; i <= n; i++)
			for (var j = i + 1; j <= n; j++)
				map[1000 * j + i] = new List<int>();
		for (var i = 1; i <= n; i++)
		{
			var a = Console.ReadLine().Split().Select(int.Parse).Select(j => i > j ? 1000 * i + j : 1000 * j + i).ToArray();
			var m = n - 2;
			for (int j = 0, k = 1; j < m; j++, k++) map[a[j]].Add(a[k]);
		}

		var cs = new int[1001000];
		var rs = new bool[1001000];
		for (var id = 0; id < map.Length; id++) if (map[id] != null) Find(map, id, cs, rs);
		Console.WriteLine(cs.Max());
	}

	static void Find(List<int>[] map, int id, int[] cs, bool[] rs)
	{
		if (cs[id] > 0) return;
		if (map[id].Count == 0) { cs[id] = 1; return; }
		if (rs[id]) { Console.WriteLine(-1); Environment.Exit(0); }
		rs[id] = true;
		foreach (var pid in map[id]) Find(map, pid, cs, rs);
		cs[id] = map[id].Max(i => cs[i]) + 1;
		rs[id] = false;
	}
}
