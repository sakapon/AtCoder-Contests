using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var r = new List<int>();
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var qc = int.Parse(Console.ReadLine());

		var st = new ST_Bit(n);
		for (int i = 0; i < n; i++)
			st.Set(i, 1 << (s[i] - 'a'));

		for (int k = 0; k < qc; k++)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "1")
				st.Set(int.Parse(q[1]) - 1, 1 << (q[2][0] - 'a'));
			else
			{
				var f = st.Subor(int.Parse(q[1]) - 1, int.Parse(q[2]));
				r.Add(Enumerable.Range(0, 26).Count(i => (f & (1 << i)) != 0));
			}
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

	public void InitAllLevels(long v)
	{
		foreach (var a in vs) for (int i = 0; i < a.Length; ++i) a[i] = v;
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

class ST_Bit : ST
{
	public ST_Bit(int n) : base(n) { }

	public void Set(int i, long v) => ForLevels(i, n => this[n] = n.k == kMax ? v : this[n.Child0] | this[n.Child1]);

	public long Subor(int minIn, int maxEx)
	{
		var r = 0L;
		ForRange(minIn, maxEx, n => r |= this[n]);
		return r;
	}
}
