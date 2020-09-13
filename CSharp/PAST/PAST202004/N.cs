using System;
using System.Collections.Generic;
using System.Linq;

class N
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], q = h[1];
		var rs = new int[n].Select(_ => Read()).ToArray();
		var ps = new int[q].Select(_ => Read()).ToArray();

		var dy = rs.SelectMany(v => new[] { v[1], v[1] + v[2] })
			.Concat(ps.Select(v => v[1]))
			.Distinct()
			.OrderBy(y => y)
			.Select((y, i) => (y, i))
			.ToDictionary(v => v.y, v => v.i);

		var c = new long[q];
		var st = new ST_RangeAdd(dy.Count);
		var qs = rs
			.Select(v => (x: v[0], X: v[0] + v[2], y: dy[v[1]], Y: dy[v[1] + v[2]], c: v[3]))
			.SelectMany(v => new[] { (q: -1, v.x, v.y, v.Y, v.c), (q: 1, x: v.X, v.y, v.Y, c: -v.c) })
			.Concat(ps.Select((v, id) => (q: 0, x: v[0], y: dy[v[1]], Y: 0, c: id)))
			.OrderBy(v => v.x)
			.ThenBy(v => v.q);

		foreach (var v in qs)
			if (v.q == 0)
				c[v.c] = st[v.y];
			else
				st.Add(v.y, v.Y + 1, v.c);
		Console.WriteLine(string.Join("\n", c));
	}
}

class ST
{
	public struct Node
	{
		public int k, i;
		public Node(int _k, int _i) { k = _k; i = _i; }
	}

	int kMax;
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

class ST_RangeAdd : ST
{
	public ST_RangeAdd(int n) : base(n) { }

	public void Add(int minIn, int maxEx, long v) => ForRange(minIn, maxEx, n => this[n] += v);

	public override long this[int i]
	{
		get
		{
			var r = 0L;
			ForLevels(i, n => r += this[n]);
			return r;
		}
	}
}
