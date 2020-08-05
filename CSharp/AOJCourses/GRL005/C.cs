using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		map = new int[n].Select(_ => Read().Skip(1).ToArray()).ToArray();
		var q = int.Parse(Console.ReadLine());
		var qs = new int[q].Select(_ => Read()).ToArray();

		tour = new List<int>();
		order = Array.ConvertAll(new int[n], _ => new List<int>());
		minDepth = new ST_Min(2 * n);
		EulerTourDfs(0, 0);

		Console.WriteLine(string.Join("\n", qs.Select(x => Lca(x[0], x[1]))));
	}

	static int[][] map;
	static List<int> tour;
	static List<int>[] order;
	static ST_Min minDepth;
	static void EulerTourDfs(int v, int depth)
	{
		order[v].Add(tour.Count);
		minDepth.SetMin(tour.Count, depth);
		foreach (var nv in map[v])
		{
			tour.Add(v);
			EulerTourDfs(nv, depth + 1);
			tour.Add(-nv);
			order[v].Add(tour.Count);
			minDepth.SetMin(tour.Count, depth);
		}
	}

	static int Lca(int u, int v)
	{
		if (u == v) return u;
		if (order[u][0] > order[v][0]) { var t = u; u = v; v = t; }
		if (order[u].Last() > order[v][0]) return u;
		return tour[minDepth.ArgMin(order[u].Last(), order[v][0])];
	}
}

class ST_Min
{
	struct KI
	{
		public int k, i;
	}

	int kMax;
	List<long[]> vs = new List<long[]> { new long[1] };
	public ST_Min(int n)
	{
		for (int c = 1; c < n; vs.Add(new long[c <<= 1])) ;
		foreach (var a in vs)
			for (int i = 0; i < a.Length; i++)
				a[i] = long.MaxValue;
		kMax = vs.Count - 1;
	}

	KI[] GetLevels(int i)
	{
		var r = new List<KI>();
		for (int k = kMax; k >= 0; --k, i >>= 1) r.Add(new KI { k = k, i = i });
		return r.ToArray();
	}

	KI[] GetRange(int minIn, int maxEx)
	{
		var r = new List<KI>();
		for (int k = kMax, f = 1; k >= 0 && minIn < maxEx; --k, f <<= 1)
		{
			if ((minIn & f) != 0) r.Add(new KI { k = k, i = (minIn += f) / f - 1 });
			if ((maxEx & f) != 0) r.Add(new KI { k = k, i = (maxEx -= f) / f });
		}
		return r.ToArray();
	}

	public long Get(int i) => vs[kMax][i];

	public void SetMin(int i, long v)
	{
		foreach (var x in GetLevels(i)) vs[x.k][x.i] = Math.Min(vs[x.k][x.i], v);
	}

	public long Submin(int minIn, int maxEx) => GetRange(minIn, maxEx).Select(x => vs[x.k][x.i]).Aggregate(Math.Min);

	public int ArgMin(int minIn, int maxEx)
	{
		var m = Submin(minIn, maxEx);
		var ki = GetRange(minIn, maxEx).First(x => vs[x.k][x.i] == m);

		for (int j; ki.k < kMax; ki = new KI { k = ki.k + 1, i = j })
		{
			j = 2 * ki.i;
			if (vs[ki.k + 1][j] > m) j++;
		}
		return ki.i;
	}
}
