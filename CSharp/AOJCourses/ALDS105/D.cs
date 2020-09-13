using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).Select((x, i) => new { x, i }).OrderBy(_ => _.x).Select(_ => _.i).ToArray();

		var st = new ST_Subsum(n);
		var r = 0L;
		for (int i = 0; i < n; i++)
		{
			r += st.Subsum(a[i], n);
			st.Add(a[i], 1);
		}
		Console.WriteLine(r);
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

	public long this[int i] => vs[kMax][i];
	public long this[Node n]
	{
		get { return vs[n.k][n.i]; }
		set { vs[n.k][n.i] = value; }
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
