using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new bool[n]
			.Select(_ => Read())
			.Select((a, id) => (id, x: a[0], y: a[1], z: a[2]))
			.GroupBy(p => p.x)
			.OrderBy(g => g.Key);

		var r = new bool[n];
		// (y, z)
		var map = new WBMap<int, int>();

		foreach (var g in ps)
		{
			var yzs = g.ToArray();

			foreach (var (id, _, y, z) in yzs)
			{
				var node = map.GetLast(p => p.Key < y);
				if (node == null) continue;
				var z0 = node.GetValueOrDefault(int.MaxValue);
				if (z0 < z) r[id] = true;
			}

			foreach (var (_, _, y, z) in yzs)
			{
				if (map.ContainsKey(y))
				{
					map[y] = Math.Min(map[y], z);
					var node = map.Get(y);
					var nn = node;
					while ((nn = node.GetNext()) != null && nn.Item.Value >= z)
					{
						map.Remove(nn.Item.Key);
					}
				}
				else
				{
					var node = map.GetLast(p => p.Key < y);
					if (node.GetValueOrDefault(int.MaxValue) <= z) continue;
					node = map.Add(y, z);
					var nn = node;
					while ((nn = node.GetNext()) != null && nn.Item.Value >= z)
					{
						map.Remove(nn.Item.Key);
					}
				}
			}
		}

		return string.Join("\n", r.Select(x => x ? "Yes" : "No"));
	}
}
