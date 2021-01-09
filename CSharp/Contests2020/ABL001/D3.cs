using System;
using System.Linq;

class D3
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var a = new int[n].Select(_ => int.Parse(Console.ReadLine())).ToArray();

		var kM = 300000;
		var st = new ST_RUMQ(kM + 1);

		for (int i = 0; i < n; i++)
			st.Set(Math.Max(0, a[i] - k), Math.Min(kM + 1, a[i] + k + 1), st.Get(a[i]) + 1);
		Console.WriteLine(st.Get(0, kM + 1));
	}
}

class ST_RUMQ
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

	const long e1 = long.MinValue;
	const long e2 = long.MinValue;

	public ST_RUMQ(int n)
	{
		while (n2 < n) n2 <<= 1;
		n2 <<= 1;
		a1 = new long[n2];
		a2 = new long[n2];
	}

	protected Node Actual(int i) => (n2 >> 1) + i;

	public void Set(int i, long v) => Set(1, n2 >> 1, Actual(i), Actual(i + 1), v);
	public void Set(int minIn, int maxEx, long v) => Set(1, n2 >> 1, Actual(minIn), Actual(maxEx), v);
	void Set(Node i, int length, Node l, Node r, long v)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return;

		if (l.i <= nl && nr <= r.i)
		{
			a1[i.i] = Math.Max(a1[i.i], v);
			a2[i.i] = Math.Max(a2[i.i], v);
		}
		else
		{
			if (a1[i.i] != e1)
			{
				a1[i.Child0.i] = Math.Max(a1[i.Child0.i], a1[i.i]);
				a1[i.Child1.i] = Math.Max(a1[i.Child1.i], a1[i.i]);
				a2[i.Child0.i] = Math.Max(a2[i.Child0.i], a1[i.i]);
				a2[i.Child1.i] = Math.Max(a2[i.Child1.i], a1[i.i]);
				a1[i.i] = e1;
			}
			Set(i.Child0, length >> 1, l, r, v);
			Set(i.Child1, length >> 1, l, r, v);
			a2[i.i] = Math.Max(a2[i.Child0.i], a2[i.Child1.i]);
		}
	}

	public long Get(int i) => Get(1, n2 >> 1, Actual(i), Actual(i + 1));
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
				a1[i.Child0.i] = Math.Max(a1[i.Child0.i], a1[i.i]);
				a1[i.Child1.i] = Math.Max(a1[i.Child1.i], a1[i.i]);
				a2[i.Child0.i] = Math.Max(a2[i.Child0.i], a1[i.i]);
				a2[i.Child1.i] = Math.Max(a2[i.Child1.i], a1[i.i]);
				a1[i.i] = e1;
			}
			return Math.Max(Get(i.Child0, length >> 1, l, r), Get(i.Child1, length >> 1, l, r));
		}
	}
}
