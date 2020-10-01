using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];

		var st = new ST_RASQ(n + 1);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Set(q[1], q[2] + 1, q[3]);
			else
				r.Add(st.Get(q[1], q[2] + 1));
		}
		Console.WriteLine(string.Join("\n", r));
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

	const long NaN = long.MinValue;

	// Power of 2
	protected int n2 = 1;
	// original: 通常の更新
	public long[] a1;
	// shadow: 自身を含む子孫の集計
	public long[] a2;

	public ST_RASQ(int n)
	{
		while (n2 < n) n2 <<= 1;
		n2 <<= 1;
		a1 = new long[n2];
		a2 = new long[n2];
	}

	protected Node Actual(int i) => (n2 >> 1) + i;

	public void Set(int minIn, int maxEx, long v) => Set(1, n2 >> 1, Actual(minIn), Actual(maxEx), v);
	void Set(Node i, int length, Node l, Node r, long v)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return;

		if (l.i <= nl && nr <= r.i)
		{
			a1[i.i] += v;
			a2[i.i] += v * length;
		}
		else
		{
			a1[i.Child0.i] += a1[i.i];
			a1[i.Child1.i] += a1[i.i];
			a2[i.Child0.i] += a1[i.i] * (length >> 1);
			a2[i.Child1.i] += a1[i.i] * (length >> 1);
			a1[i.i] = 0;

			Set(i.Child0, length >> 1, l, r, v);
			Set(i.Child1, length >> 1, l, r, v);
			a2[i.i] = a2[i.Child0.i] + a2[i.Child1.i];
		}
	}

	public long Get(int minIn, int maxEx) => Get(1, n2 >> 1, Actual(minIn), Actual(maxEx));
	long Get(Node i, int length, Node l, Node r)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return 0;

		if (l.i <= nl && nr <= r.i)
		{
			return a2[i.i];
		}
		else
		{
			a1[i.Child0.i] += a1[i.i];
			a1[i.Child1.i] += a1[i.i];
			a2[i.Child0.i] += a1[i.i] * (length >> 1);
			a2[i.Child1.i] += a1[i.i] * (length >> 1);
			a1[i.i] = 0;

			return Get(i.Child0, length >> 1, l, r) + Get(i.Child1, length >> 1, l, r);
		}
	}
}
