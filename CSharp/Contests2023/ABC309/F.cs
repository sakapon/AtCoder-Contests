using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		Array.ForEach(ps, Array.Sort);
		var map = new WBMap<int, int>
		{
			[0] = 1 << 30,
			[1 << 30] = 0,
		};

		foreach (var p in ps.OrderBy(p => -p[0]).ThenBy(p => p[1]))
		{
			var ge = map.GetFirst(t => t.Key > p[1]);
			if (p[2] < ge.Item.Value) return true;

			var eq = map.Get(p[1]);
			if (eq.TryGetValue(out var v2))
			{
				if (v2 < p[2])
				{
					map[p[1]] = p[2];
				}
			}
			else
			{
				if (ge.Item.Value < p[2])
				{
					eq = map.Add(p[1], p[2]);
				}
				else
				{
					eq = ge;
				}
			}

			var le = eq;
			while ((le = eq.GetPrevious()).Item.Value <= eq.Item.Value)
			{
				map.Remove(le.Item.Key);
			}
		}

		return false;
	}
}
