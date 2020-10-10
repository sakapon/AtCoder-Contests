using System;
using System.Linq;

class L
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		var n = h[0];
		var a = Read();

		var st = new ST_RASQ(n);
		for (int i = 0; i < n; i++)
			st.Set(i, a[i]);

		for (int k = 0; k < h[1]; k++)
		{
			var q = Read();
			if (q[0] == 1)
				st.Set(q[1] - 1, q[2], 1);
			else
				Console.WriteLine(st.Get(q[1] - 1, q[2]).inv);
		}
		Console.Out.Flush();
	}
}

class ST_RASQ
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
	public (long c0, long c1, long ord, long inv)[] a2;

	const long e1 = 0;
	static (long c0, long c1, long ord, long inv) e2 = (0, 0, 0, 0);

	public ST_RASQ(int n)
	{
		while (n2 < n) n2 <<= 1;
		n2 <<= 1;
		a1 = new long[n2];
		a2 = new (long, long, long, long)[n2];
		for (int i = 1; i < n2; i++)
			a2[i] = (1, 0, 0, 0);
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
			a1[i.i] += v;
			if (v % 2 == 1)
			{
				var (c0, c1, ord, inv) = a2[i.i];
				a2[i.i] = (c1, c0, inv, ord);
			}
		}
		else
		{
			if (a1[i.i] != e1)
			{
				a1[i.Child0.i] += a1[i.i];
				a1[i.Child1.i] += a1[i.i];
				if (a1[i.i] % 2 == 1)
				{
					var (c0, c1, ord, inv) = a2[i.Child0.i];
					a2[i.Child0.i] = (c1, c0, inv, ord);
					(c0, c1, ord, inv) = a2[i.Child1.i];
					a2[i.Child1.i] = (c1, c0, inv, ord);
				}
				a1[i.i] = e1;
			}
			Set(i.Child0, length >> 1, l, r, v);
			Set(i.Child1, length >> 1, l, r, v);

			var (c00, c10, ord0, inv0) = a2[i.Child0.i];
			var (c01, c11, ord1, inv1) = a2[i.Child1.i];
			a2[i.i] = (c00 + c01, c10 + c11, ord0 + ord1 + c00 * c11, inv0 + inv1 + c10 * c01);
		}
	}

	public (long, long, long, long inv) Get(int minIn, int maxEx) => Get(1, n2 >> 1, Actual(minIn), Actual(maxEx));
	(long, long, long, long) Get(Node i, int length, Node l, Node r)
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
				a1[i.Child0.i] += a1[i.i];
				a1[i.Child1.i] += a1[i.i];
				if (a1[i.i] % 2 == 1)
				{
					var (c0, c1, ord, inv) = a2[i.Child0.i];
					a2[i.Child0.i] = (c1, c0, inv, ord);
					(c0, c1, ord, inv) = a2[i.Child1.i];
					a2[i.Child1.i] = (c1, c0, inv, ord);
				}
				a1[i.i] = e1;
			}

			var (c00, c10, ord0, inv0) = Get(i.Child0, length >> 1, l, r);
			var (c01, c11, ord1, inv1) = Get(i.Child1, length >> 1, l, r);
			return (c00 + c01, c10 + c11, ord0 + ord1 + c00 * c11, inv0 + inv1 + c10 * c01);
		}
	}
}
