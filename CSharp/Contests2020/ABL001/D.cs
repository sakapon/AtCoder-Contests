using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var a = new int[n].Select(_ => int.Parse(Console.ReadLine())).ToArray();

		var kM = 300000;

		var r = 0L;
		var st = new ST_RangeSet(kM + 1);

		for (int i = 0; i < n; i++)
			st.Set(Math.Max(0, a[i] - k), Math.Min(kM + 1, a[i] + k + 1), st[a[i]] + 1);

		for (int x = 0; x <= kM; x++)
			r = Math.Max(r, st[x]);
		Console.WriteLine(r);
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
	public List<long[]> vs = new List<long[]> { new long[1] };

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

	// インデックスの昇順ではなく、階層の降順です。
	public void ForRange(int minIn, int maxEx, Action<Node> action)
	{
		for (int k = kMax, f = 1; k >= 0 && minIn < maxEx; --k, f <<= 1)
		{
			if ((minIn & f) != 0) action(new Node(k, (minIn += f) / f - 1));
			if ((maxEx & f) != 0) action(new Node(k, (maxEx -= f) / f));
		}
	}
}

class ST_RangeSet : ST
{
	public ST_RangeSet(int n) : base(n) { }

	public void Set(int minIn, int maxEx, long v) => ForRange(minIn, maxEx, n => this[n] = Math.Max(this[n], v));

	public override long this[int i]
	{
		get
		{
			var r = 0L;
			ForLevels(i, n => r = Math.Max(r, this[n]));
			return r;
		}
	}
}
