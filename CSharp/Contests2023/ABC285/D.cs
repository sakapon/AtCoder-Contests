using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());

		var map = ps.ToDictionary(p => p[0], p => p[1]);
		var c = 0;

		var svs = map.Keys.Except(map.Values);
		foreach (var sv in svs)
		{
			for (var s = sv; map.ContainsKey(s); s = map[s])
			{
				c++;
			}
		}
		return c == n;
	}
}
