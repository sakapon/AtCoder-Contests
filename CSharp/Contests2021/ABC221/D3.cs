using System;
using System.Collections.Generic;
using System.Linq;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var map = new Map<int, int>();

		foreach (var (a, b) in ps)
		{
			map[a]++;
			map[a + b]--;
		}
		var xs = map.OrderBy(p => p.Key).ToArray();

		var r = new long[n + 1];
		var t = 0;

		for (int i = 0; i < xs.Length - 1; i++)
		{
			var (x, d) = xs[i];
			r[t += d] += xs[i + 1].Key - x;
		}
		return string.Join(" ", r[1..]);
	}
}

class Map<TK, TV> : Dictionary<TK, TV>
{
	TV _v0;
	public Map(TV v0 = default(TV)) { _v0 = v0; }

	public new TV this[TK key]
	{
		get { return ContainsKey(key) ? base[key] : _v0; }
		set { base[key] = value; }
	}
}
