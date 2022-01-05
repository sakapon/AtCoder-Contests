using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.Spp;

class I
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2L();
		var es = Array.ConvertAll(new bool[m], _ => ReadL());

		var xs = es.Select(e => e[0])
			.Concat(es.Select(e => e[1]))
			.Append(1).Append(n)
			.ToArray();
		var hmap = new CompressionHashMap(xs);
		var n2 = hmap.Count;

		var nes = new List<long[]>();
		foreach (var e in es)
		{
			nes.Add(new[] { hmap[e[0]], hmap[e[1]], e[2] });
		}
		for (int i = 1; i < n2; i++)
		{
			nes.Add(new[] { i - 1, i, hmap.ReverseMap[i] - hmap.ReverseMap[i - 1] });
		}

		var map = ToMap(n2, nes.ToArray(), false);
		var r = Dijkstra(n2, v => map[v].ToArray(), 0, n2 - 1);
		return r[^1];
	}

	static List<long[]>[] ToMap(int n, long[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<long[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}

	public static long[] Dijkstra(int n, Func<int, long[][]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = PriorityQueue<int>.CreateWithKey(v => costs[v]);
		costs[sv] = 0;
		q.Push(sv);

		while (q.Any)
		{
			var (v, c) = q.Pop();
			if (v == ev) break;
			if (costs[v] < c) continue;

			foreach (var e in nexts(v))
			{
				var (nv, nc) = ((int)e[1], c + e[2]);
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				q.Push(nv);
			}
		}
		return costs;
	}
}

class CompressionHashMap
{
	public long[] Raw { get; }
	public long[] ReverseMap { get; }
	public Dictionary<long, int> Map { get; }
	public int this[long v] => Map[v];
	public int Count => ReverseMap.Length;

	int[] c;
	public int[] Compressed => c ??= Array.ConvertAll(Raw, v => Map[v]);

	public CompressionHashMap(long[] a)
	{
		// r = a.Distinct().OrderBy(v => v).ToArray();
		var hs = new HashSet<long>();
		foreach (var v in a) hs.Add(v);
		var r = new long[hs.Count];
		hs.CopyTo(r);
		Array.Sort(r);
		var map = new Dictionary<long, int>();
		for (int i = 0; i < r.Length; ++i) map[r[i]] = i;

		(Raw, ReverseMap, Map) = (a, r, map);
	}
}
