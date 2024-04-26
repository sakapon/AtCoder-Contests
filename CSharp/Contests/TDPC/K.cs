using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLib10.SegTrees.SegTrees111;

class K
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int l, int r) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		ps = Array.ConvertAll(ps, p => (p.l - p.r, p.l + p.r));
		Array.Sort(ps);
		var map = new CompressionHashMap(ps.Select(p => p.l).Concat(ps.Select(p => p.r)).ToArray());

		var m = map.Count;
		var monoid = new Monoid<int>((x, y) => x >= y ? x : y, 0);
		var st = new MergeTree<int>(m, monoid);

		foreach (var p in ps)
		{
			var (l, r) = p;
			(l, r) = (map[l], map[r]);

			var nv = st[r + 1, m] + 1;
			if (st[r] < nv) st[r] = nv;
		}
		return Enumerable.Range(0, m).Max(i => st[i]);
	}
}

class CompressionHashMap
{
	public int[] Raw { get; }
	public int[] ReverseMap { get; }
	public Dictionary<int, int> Map { get; }
	public int this[int v] => Map[v];
	public int Count => ReverseMap.Length;

	int[] c;
	public int[] Compressed => c ??= Array.ConvertAll(Raw, v => Map[v]);

	public CompressionHashMap(int[] a)
	{
		// r = a.Distinct().OrderBy(v => v).ToArray();
		var hs = new HashSet<int>();
		foreach (var v in a) hs.Add(v);
		var r = new int[hs.Count];
		hs.CopyTo(r);
		Array.Sort(r);
		var map = new Dictionary<int, int>();
		for (int i = 0; i < r.Length; ++i) map[r[i]] = i;

		(Raw, ReverseMap, Map) = (a, r, map);
	}
}
