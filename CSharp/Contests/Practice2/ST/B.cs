using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];
		var a = Read();

		var st = new ST_Subsum(n);
		for (int i = 0; i < n; i++)
			st.Add(i, a[i]);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Add(q[1], q[2]);
			else
				r.Add(st.Subsum(q[1], q[2]));
		}
		Console.WriteLine(string.Join("\n", r));
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
