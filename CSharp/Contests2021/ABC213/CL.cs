using System;
using System.Collections.Generic;
using System.Linq;

class CL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var xs = Array.ConvertAll(ps, p => p.x);
		var ys = Array.ConvertAll(ps, p => p.y);

		var xmap = new CompressionHashMap(xs);
		var ymap = new CompressionHashMap(ys);

		return string.Join("\n", Enumerable.Range(0, n).Select(i => $"{xmap[xs[i]] + 1} {ymap[ys[i]] + 1}"));
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
