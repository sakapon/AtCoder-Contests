using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var map = Comb2(n).ToDictionary(p => 10000 * p[0] + p[1], p => new List<int>());
		var fores = new HashSet<int>(map.Keys);
		for (var i = 1; i <= n; i++)
		{
			var a = Console.ReadLine().Split().Select(int.Parse).Select(j => i < j ? 10000 * i + j : 10000 * j + i).ToArray();
			var m = n - 2;
			for (int j = 0, k = 1; j < m; j++, k++)
			{
				map[a[j]].Add(a[k]);
				fores.Remove(a[k]);
			}
		}

		int c = 0, M = -1;
		var rs = new HashSet<int>();
		foreach (var id in fores) Find(map, rs, id, ref c, ref M);
		Console.WriteLine(M);
	}

	static IEnumerable<int[]> Comb2(int n)
	{
		for (var i = 1; i <= n; i++)
			for (var j = i + 1; j <= n; j++)
				yield return new[] { i, j };
	}

	static void Find(Dictionary<int, List<int>> map, HashSet<int> rs, int id, ref int c, ref int M)
	{
		if (map[id].Count == 0) { M = Math.Max(M, c + 1); return; }
		if (rs.Contains(id)) { Console.WriteLine(-1); Environment.Exit(0); }
		c++; rs.Add(id);
		foreach (var pid in map[id]) Find(map, rs, pid, ref c, ref M);
		c--; rs.Remove(id);
	}
}
