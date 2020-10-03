using System;
using System.Linq;

class D2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var a = new int[n].Select(_ => int.Parse(Console.ReadLine())).ToArray();

		var kM = 300000;
		var r = 0L;
		var st = new ST_RUQ(kM + 1);

		for (int i = 0; i < n; i++)
			st.Set(Math.Max(0, a[i] - k), Math.Min(kM + 1, a[i] + k + 1), st.Get(a[i]) + 1);
		for (int x = 0; x <= kM; x++)
			Chmax(ref r, st.Get(x));

		Console.WriteLine(r);
	}

	static long Chmax(ref long x, long v) => x < v ? x = v : x;
}

class ST_RUQ
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

	const long e = long.MinValue;

	public ST_RUQ(int n)
	{
		while (n2 < n) n2 <<= 1;
		a = new long[n2 <<= 1];
	}

	public void InitAllLevels(long v) { for (int i = 1; i < a.Length; ++i) a[i] = v; }

	protected Node Actual(int i) => (n2 >> 1) + i;

	public void Set(int i, long v) => Set(1, n2 >> 1, Actual(i), Actual(i + 1), v);
	public void Set(int minIn, int maxEx, long v) => Set(1, n2 >> 1, Actual(minIn), Actual(maxEx), v);
	void Set(Node i, int length, Node l, Node r, long v)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return;

		if (l.i <= nl && nr <= r.i)
		{
			a[i.i] = Math.Max(a[i.i], v);
		}
		else
		{
			if (a[i.i] != e)
			{
				a[i.Child0.i] = Math.Max(a[i.Child0.i], a[i.i]);
				a[i.Child1.i] = Math.Max(a[i.Child1.i], a[i.i]);
				a[i.i] = e;
			}
			Set(i.Child0, length >> 1, l, r, v);
			Set(i.Child1, length >> 1, l, r, v);
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
			if (a[i.i] != e)
			{
				a[i.Child0.i] = Math.Max(a[i.Child0.i], a[i.i]);
				a[i.Child1.i] = Math.Max(a[i.Child1.i], a[i.i]);
				a[i.i] = e;
			}
			return Math.Max(Get(i.Child0, length >> 1, l, r), Get(i.Child1, length >> 1, l, r));
		}
	}
}
