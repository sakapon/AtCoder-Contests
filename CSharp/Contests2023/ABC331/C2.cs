using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var g = a.GroupBy(v => v).Select(g => (v: g.Key, s: g.Sum())).OrderBy(p => -p.v).ToArray();

		var s = new long[1000001];
		for (int i = 1; i < g.Length; i++)
			s[g[i].v] = s[g[i - 1].v] + g[i - 1].s;
		return string.Join(" ", a.Select(v => s[v]));
	}
}
