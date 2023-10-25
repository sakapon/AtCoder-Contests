using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class D3
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var d = new Dictionary<long, long>();

		foreach (var (s, c) in ps)
		{
			var f = -s & s;
			var ns = s / f;
			if (d.TryGetValue(ns, out var v)) d[ns] = v + c * f;
			else d[ns] = c * f;
		}
		return d.Values.Sum(v => BitOperations.PopCount((ulong)v));
	}
}
