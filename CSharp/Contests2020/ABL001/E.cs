using System;
using System.Linq;

class E
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		var n = h[0];

		var st = new ST_RUSQ(n);
		st.Set(0, n, 1);

		for (int k = 0; k < h[1]; k++)
		{
			var q = Read();
			st.Set(q[0] - 1, q[1], q[2]);
			Console.WriteLine(st.Get(0, n));
		}
		Console.Out.Flush();
	}
}

class ST_RSQ
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

	const long e = 0;

	public ST_RSQ(int n)
	{
		while (n2 < n) n2 <<= 1;
		a = new long[n2 <<= 1];
	}

	public void InitAllLevels(long v) { for (int i = 1; i < a.Length; ++i) a[i] = v; }

	protected Node Actual(int i) => (n2 >> 1) + i;
	public virtual long this[int i] => a[Actual(i).i];

	public void Set(int i, long v) => Set(1, n2 >> 1, Actual(i), Actual(i + 1), v);
	public void Set(int minIn, int maxEx, long v) => Set(1, n2 >> 1, Actual(minIn), Actual(maxEx), v);
	void Set(Node i, int length, Node l, Node r, long v)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return;

		if (l.i <= nl && nr <= r.i)
		{
			a[i.i] += v;
		}
		else
		{
			Set(i.Child0, length >> 1, l, r, v);
			Set(i.Child1, length >> 1, l, r, v);
			a[i.i] = a[i.Child0.i] + a[i.Child1.i];
		}
	}

	public long Get(int i) => Get(1, n2 >> 1, Actual(i), Actual(i + 1));
	public long Get(int minIn, int maxEx) => Get(1, n2 >> 1, Actual(minIn), Actual(maxEx));
	long Get(Node i, int length, Node l, Node r)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return e;

		if (l.i <= nl && nr <= r.i)
		{
			return a[i.i];
		}
		else
		{
			return Get(i.Child0, length >> 1, l, r) + Get(i.Child1, length >> 1, l, r);
		}
	}
}

class ST_RUSQ
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
	// original: 通常の更新
	public long[] a1;
	// shadow: 自身を含む子孫の集計
	public long[] a2;

	const long e1 = long.MaxValue;
	const long e2 = 0;

	const long M = 998244353;
	ST_RSQ rsq;

	public ST_RUSQ(int n)
	{
		while (n2 < n) n2 <<= 1;
		n2 <<= 1;
		a1 = new long[n2];
		a2 = new long[n2];

		rsq = new ST_RSQ(n);
		rsq.Set(n - 1, 1);
		for (int i = n - 1; i > 0; i--)
			rsq.Set(i - 1, rsq[i] * 10 % M);
	}

	protected Node Actual(int i) => (n2 >> 1) + i;

	public void Set(int minIn, int maxEx, long v) => Set(1, n2 >> 1, Actual(minIn), Actual(maxEx), v);
	void Set(Node i, int length, Node l, Node r, long v)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return;

		if (l.i <= nl && nr <= r.i)
		{
			a1[i.i] = v;
			a2[i.i] = v * (rsq.a[i.i] % M) % M;
		}
		else
		{
			if (a1[i.i] != e1)
			{
				a1[i.Child0.i] = a1[i.i];
				a1[i.Child1.i] = a1[i.i];
				a2[i.Child0.i] = a1[i.i] * (rsq.a[i.Child0.i] % M) % M;
				a2[i.Child1.i] = a1[i.i] * (rsq.a[i.Child1.i] % M) % M;
				a1[i.i] = e1;
			}
			Set(i.Child0, length >> 1, l, r, v);
			Set(i.Child1, length >> 1, l, r, v);
			a2[i.i] = (a2[i.Child0.i] + a2[i.Child1.i]) % M;
		}
	}

	public long Get(int minIn, int maxEx) => Get(1, n2 >> 1, Actual(minIn), Actual(maxEx));
	long Get(Node i, int length, Node l, Node r)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return e2;

		if (l.i <= nl && nr <= r.i)
		{
			return a2[i.i];
		}
		else
		{
			if (a1[i.i] != e1)
			{
				a1[i.Child0.i] = a1[i.i];
				a1[i.Child1.i] = a1[i.i];
				a2[i.Child0.i] = a1[i.i] * (rsq.a[i.Child0.i] % M) % M;
				a2[i.Child1.i] = a1[i.i] * (rsq.a[i.Child1.i] % M) % M;
				a1[i.i] = e1;
			}
			return (Get(i.Child0, length >> 1, l, r) + Get(i.Child1, length >> 1, l, r)) % M;
		}
	}
}
