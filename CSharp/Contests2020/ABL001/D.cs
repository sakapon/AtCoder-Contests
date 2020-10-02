using System;
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
		var st = new ST_RangeMax(kM + 1);

		for (int i = 0; i < n; i++)
			st.Set(Math.Max(0, a[i] - k), Math.Min(kM + 1, a[i] + k + 1), st[a[i]] + 1);

		for (int x = 0; x <= kM; x++)
			ST.Chmax(ref r, st[x]);
		Console.WriteLine(r);
	}
}

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

	public static void Chmax(ref long x, long v) { if (x < v) x = v; }
	public static void Chmin(ref long x, long v) { if (x > v) x = v; }
}

class ST_RangeMax : ST
{
	public ST_RangeMax(int n) : base(n) { }

	public void Set(int minIn, int maxEx, long v) => ForRange(minIn, maxEx, n => this[n] = Math.Max(this[n], v));

	public override long this[int i]
	{
		get
		{
			var r = 0L;
			ForLevels(i, n => Chmax(ref r, this[n]));
			return r;
		}
	}
}
