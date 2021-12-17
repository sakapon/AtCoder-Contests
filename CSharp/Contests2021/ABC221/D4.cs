using System;
using System.Collections.Generic;

class D4
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var l = new List<(int x, int d)>();
		foreach (var ab in ps)
		{
			l.Add((ab[0], 1));
			l.Add((ab[0] + ab[1], -1));
		}

		var qs = l.ToArray();
		Array.Sort(Array.ConvertAll(qs, q => q.x), qs);

		var r = new int[n + 1];
		var (tx, td) = (0, 0);

		foreach (var (x, d) in qs)
		{
			r[td] += x - tx;
			tx = x;
			td += d;
		}
		return string.Join(" ", r[1..]);
	}
}
