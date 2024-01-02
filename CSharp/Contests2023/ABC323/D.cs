using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var r = 0L;
		var map = new WBMap<long, long>();
		map.Initialize(ps);

		while (map.Count > 0)
		{
			var (s, c) = map.RemoveFirst().Item;
			if (c % 2 == 1) r++;
			s <<= 1;
			c >>= 1;
			if (c == 0) continue;
			if (map.ContainsKey(s))
				map[s] += c;
			else
				map[s] = c;
		}
		return r;
	}
}
