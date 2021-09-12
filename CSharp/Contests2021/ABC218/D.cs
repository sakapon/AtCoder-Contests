using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var map = new Map<(int y1, int y2), int>();

		foreach (var g in ps.GroupBy(p => p.x))
		{
			var a = g.ToArray();
			Array.Sort(a);

			for (int i = 0; i < a.Length; i++)
			{
				for (int j = i + 1; j < a.Length; j++)
				{
					map[(a[i].y, a[j].y)]++;
				}
			}
		}

		return map.Values.Sum(v => (long)v * (v - 1) / 2);
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
