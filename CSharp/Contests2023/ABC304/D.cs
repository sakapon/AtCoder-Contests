using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections.Statics.Typed;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		var A = long.Parse(Console.ReadLine());
		var a = Read();
		var B = long.Parse(Console.ReadLine());
		var b = Read();

		var aset = new ArrayItemSet<int>(a);
		var bset = new ArrayItemSet<int>(b);

		var map = new HashMap<(int, int), int>();

		foreach (var (p, q) in ps)
		{
			map[(aset.GetFirstIndexGeq(p), bset.GetFirstIndexGeq(q))]++;
		}

		var M = map.Values.Max();
		var m = map.Values.Min();

		if (map.Count < (A + 1) * (B + 1)) m = 0;
		return $"{m} {M}";
	}
}

class HashMap<TK, TV> : Dictionary<TK, TV>
{
	TV _v0;
	public HashMap(TV v0 = default(TV)) { _v0 = v0; }

	public new TV this[TK key]
	{
		get { return ContainsKey(key) ? base[key] : _v0; }
		set { base[key] = value; }
	}
}
