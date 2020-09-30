using System;

// 外見上は 0-indexed, 0 <= i < n
// 内部では 1-indexed, 1 <= i < n2
class ST
{
	public struct Node
	{
		public int i;
		public static implicit operator Node(int i) => new Node { i = i };

		public Node Parent => i >> 1;
		public Node Child0 => i << 1;
		public Node Child1 => (i << 1) + 1;
	}

	// Power of 2
	protected int n2 = 1;
	public long[] a;

	public ST(int n)
	{
		while (n2 < n) n2 <<= 1;
		a = new long[n2 <<= 1];
	}

	public void InitAllLevels(long v) { for (int i = 1; i < a.Length; ++i) a[i] = v; }

	protected Node Actual(int i) => (n2 >> 1) + i;
	public long this[Node n]
	{
		get { return a[n.i]; }
		set { a[n.i] = value; }
	}
	public virtual long this[int i] => this[Actual(i)];

	// 階層の降順
	public void ForLevels(int i, Action<Node> action)
	{
		for (i += n2 >> 1; i > 0; i >>= 1) action(i);
	}

	// 範囲の昇順
	public void ForRange(int minIn, int maxEx, Action<Node> action)
	{
		int l = (n2 >> 1) + minIn, r = (n2 >> 1) + maxEx;
		while (l < r)
		{
			var length = l & -l;
			while (l + length > r) length >>= 1;
			action(l / length);
			l += length;
		}
	}

	// 範囲の降順 (階層の降順)
	public void ForRange(int maxEx, Action<Node> action)
	{
		var i = (n2 >> 1) + maxEx;
		if (i == n2) { action(1); return; }
		for (; i > 1; i >>= 1) if ((i & 1) != 0) action(i - 1);
	}

	// 範囲の昇順 (再帰)
	public void ForRangeR(int minIn, int maxEx, Action<Node> action) => ForRangeR(1, n2 >> 1, Actual(minIn), Actual(maxEx), action);
	protected void ForRangeR(Node i, int length, Node l, Node r, Action<Node> action)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return;

		if (l.i <= nl && nr <= r.i)
			action(i);
		else
		{
			ForRangeR(i.Child0, length >> 1, l, r, action);
			ForRangeR(i.Child1, length >> 1, l, r, action);
		}
	}

	public static long Chmax(ref long x, long v) => x < v ? x = v : x;
	public static long Chmin(ref long x, long v) => x > v ? x = v : x;
}

// 範囲の最小値を求める場合。
class ST_Min : ST
{
	public ST_Min(int n) : base(n) { }

	public void Set(int i, long v)
	{
		ForLevels(i, n => this[n] = n.i >= n2 >> 1 ? v : Math.Min(this[n.Child0], this[n.Child1]));
	}

	public long Submin(int minIn, int maxEx)
	{
		var r = long.MaxValue;
		ForRange(minIn, maxEx, n => Chmin(ref r, this[n]));
		return r;
	}
}

// 範囲の和を求める場合。
class ST_Subsum : ST
{
	public ST_Subsum(int n) : base(n) { }

	public void Set(int i, long v) => Add(i, v - this[i]);
	public void Add(int i, long v) => ForLevels(i, n => this[n] += v);

	public long Subsum(int minIn, int maxEx)
	{
		var r = 0L;
		ForRange(minIn, maxEx, n => r += this[n]);
		return r;
	}
}

// 範囲に設定する場合。
class ST_RangeSet : ST
{
	public ST_RangeSet(int n) : base(n) { }

	long c = 0;
	public void Set(int minIn, int maxEx, long v)
	{
		v |= ++c << 32;
		ForRange(minIn, maxEx, n => this[n] = v);
	}

	public override long this[int i]
	{
		get
		{
			var r = 0L;
			ForLevels(i, n => Chmax(ref r, this[n]));
			return (int)r;
		}
	}
}

// 範囲に加算する場合。
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
