using System;
using System.Collections.Generic;
using System.Linq;

class G12
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];

		var st = new ST_RangeAddSum2(n + 1);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Add(q[1], q[2] + 1, q[3]);
			else
				r.Add(st.Sum(q[1], q[2] + 1));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}

class ST_RangeAddSum2
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
	// original: 通常の Range Add
	public long[] a1;
	// shadow: 自身を含む子孫の和
	public long[] a2;

	public ST_RangeAddSum2(int n)
	{
		while (n2 < n) n2 <<= 1;
		n2 <<= 1;
		a1 = new long[n2];
		a2 = new long[n2];
	}

	protected Node Actual(int i) => (n2 >> 1) + i;

	// 範囲の昇順 (再帰)
	public void ForRangeR(int minIn, int maxEx, Action<Node> action, Action<Node> actionP) => ForRangeR(1, n2 >> 1, Actual(minIn), Actual(maxEx), action, actionP);
	protected int ForRangeR(Node i, int length, Node l, Node r, Action<Node> action, Action<Node> actionP)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return 0;

		if (l.i <= nl && nr <= r.i)
		{
			action(i);
			return length;
		}
		else
		{
			var cl = ForRangeR(i.Child0, length >> 1, l, r, action, actionP) + ForRangeR(i.Child1, length >> 1, l, r, action, actionP);
			actionP(i);
			return cl;
		}
	}

	public void Add(int minIn, int maxEx, long v) => Add(1, n2 >> 1, Actual(minIn), Actual(maxEx), v);
	int Add(Node i, int length, Node l, Node r, long v)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return 0;

		if (l.i <= nl && nr <= r.i)
		{
			a1[i.i] += v;
			a2[i.i] += v * length;
			return length;
		}
		else
		{
			var cl = Add(i.Child0, length >> 1, l, r, v) + Add(i.Child1, length >> 1, l, r, v);
			a2[i.i] = a1[i.i] * length + a2[i.Child0.i] + a2[i.Child1.i];
			return cl;
		}
	}

	public long Sum(int minIn, int maxEx)
	{
		var r = 0L;
		Sum(1, n2 >> 1, Actual(minIn), Actual(maxEx), v => r += v);
		return r;
	}
	int Sum(Node i, int length, Node l, Node r, Action<long> action)
	{
		int nl = i.i * length, nr = nl + length;
		if (r.i <= nl || nr <= l.i) return 0;

		if (l.i <= nl && nr <= r.i)
		{
			action(a2[i.i]);
			return length;
		}
		else
		{
			var cl = Sum(i.Child0, length >> 1, l, r, action) + Sum(i.Child1, length >> 1, l, r, action);
			action(a1[i.i] * cl);
			return cl;
		}
	}
}
