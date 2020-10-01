using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];

		var st = new ST_RangeSet2(n);
		st.Set(0, n, int.MaxValue);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Set(q[1], q[2] + 1, q[3]);
			else
				r.Add(st.Get(q[1]));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}

class ST_RangeSet2
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

	public ST_RangeSet2(int n)
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

	public void Set(int minIn, int maxEx, long v) => Set(1, n2 >> 1, Actual(minIn), Actual(maxEx), v);
	void Set(Node i, int length, Node l, Node r, long v)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return;

		if (l.i <= nl && nr <= r.i)
		{
			this[i] = v;
		}
		else
		{
			if (this[i] != -1)
			{
				this[i.Child0] = this[i.Child1] = this[i];
				this[i] = -1;
			}
			Set(i.Child0, length >> 1, l, r, v);
			Set(i.Child1, length >> 1, l, r, v);
		}
	}

	public long Get(int i)
	{
		var r = 0L;
		ForLevels(i, n => { if (this[n] != -1) r = this[n]; });
		return r;
	}
}
