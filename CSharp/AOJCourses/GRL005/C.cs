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
		minDepth.InitAllLevels(1 << 30);
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
		minDepth.Set(tour.Count, depth);
		foreach (var nv in map[v])
		{
			tour.Add(v);
			EulerTourDfs(nv, depth + 1);
			tour.Add(-nv);
			order[v].Add(tour.Count);
			minDepth.Set(tour.Count, depth);
		}
	}

	static int Lca(int u, int v)
	{
		if (u == v) return u;
		if (order[u][0] > order[v][0]) { var t = u; u = v; v = t; }
		if (order[u].Last() > order[v][0]) return u;
		return tour[minDepth.FirstArgMin(order[u].Last(), order[v][0])];
	}
}

class ST
{
	public struct Node
	{
		public int k, i;
		public Node(int _k, int _i) { k = _k; i = _i; }

		public Node Parent => new Node(k - 1, i >> 1);
		public Node Child0 => new Node(k + 1, i << 1);
		public Node Child1 => new Node(k + 1, (i << 1) + 1);
	}

	protected int kMax;
	List<long[]> vs = new List<long[]> { new long[1] };

	public ST(int n)
	{
		for (int c = 1; c < n; vs.Add(new long[c <<= 1])) ;
		kMax = vs.Count - 1;
	}

	public virtual long this[int i] => vs[kMax][i];
	public long this[Node n]
	{
		get { return vs[n.k][n.i]; }
		set { vs[n.k][n.i] = value; }
	}

	public void InitAllLevels(long v)
	{
		foreach (var a in vs) for (int i = 0; i < a.Length; ++i) a[i] = v;
	}

	public void Clear()
	{
		for (int k = 0; k <= kMax; ++k) Array.Clear(vs[k], 0, vs[k].Length);
	}

	public void ForLevels(int i, Action<Node> action)
	{
		for (int k = kMax; k >= 0; --k, i >>= 1) action(new Node(k, i));
	}

	public void ForRange(int minIn, int maxEx, Action<Node> action)
	{
		for (int k = kMax, f = 1; k >= 0 && minIn < maxEx; --k, f <<= 1)
		{
			if ((minIn & f) != 0) action(new Node(k, (minIn += f) / f - 1));
			if ((maxEx & f) != 0) action(new Node(k, (maxEx -= f) / f));
		}
	}
}

class ST_Min : ST
{
	public ST_Min(int n) : base(n) { }

	public void Set(int i, long v) => ForLevels(i, n => this[n] = n.k == kMax ? v : Math.Min(this[n.Child0], this[n.Child1]));

	public long Submin(int minIn, int maxEx)
	{
		var r = long.MaxValue;
		ForRange(minIn, maxEx, n => r = Math.Min(r, this[n]));
		return r;
	}

	public int FirstArgMin(int minIn, int maxEx)
	{
		var m = long.MaxValue;
		var mn = new Node();
		ForRange(minIn, maxEx, n =>
		{
			if (this[n] < m)
			{
				m = this[n];
				mn = n;
			}
		});

		while (mn.k < kMax) mn = this[mn.Child0] == m ? mn.Child0 : mn.Child1;
		return mn.i;
	}
}
