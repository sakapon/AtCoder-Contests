﻿using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var rs = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var xqs = rs.Select(r => new { x = r[0], d = 1, y1 = r[1], y2 = r[3] })
			.Concat(rs.Select(r => new { x = r[2], d = -1, y1 = r[1], y2 = r[3] }))
			.OrderBy(q => q.x);

		var st = new LST<int, int>(1000,
			(x, y) => x + y, 0,
			Math.Max, 0,
			(x, p, _, l) => p + x);
		int M = 0, xt = -1;

		foreach (var q in xqs)
		{
			if (xt < q.x)
			{
				M = Math.Max(M, st.Get(0, 1000));
				xt = q.x;
			}

			st.Set(q.y1, q.y2, q.d);
		}
		Console.WriteLine(M);
	}
}

class LST<TO, TV>
{
	public struct STNode
	{
		public int i;
		public static implicit operator STNode(int i) => new STNode { i = i };
		public override string ToString() => $"{i}";

		public STNode Parent => i >> 1;
		public STNode Child0 => i << 1;
		public STNode Child1 => (i << 1) + 1;
		public STNode LastLeft(int length) => i * length;
		public STNode LastRight(int length) => (i + 1) * length;
	}

	// Power of 2
	public int n2 = 1;
	public TO[] a1;
	public TV[] a2;

	public Func<TO, TO, TO> Multiply;
	public TO id;
	Func<TO, TO, bool> TOEquals = System.Collections.Generic.EqualityComparer<TO>.Default.Equals;

	public Func<TV, TV, TV> Union;
	public TV v0;

	// (operator, currentValue, node, length) => newValue
	public Func<TO, TV, STNode, int, TV> Transform;

	public LST(int n, Func<TO, TO, TO> multiply, TO _id, Func<TV, TV, TV> union, TV _v0, Func<TO, TV, STNode, int, TV> transform, TV[] a0 = null)
	{
		while (n2 < n << 1) n2 <<= 1;
		a1 = new TO[n2];
		a2 = new TV[n2];

		Multiply = multiply;
		id = _id;
		Union = union;
		v0 = _v0;
		Transform = transform;
		if (!TOEquals(id, default(TO)) || !Equals(v0, default(TV)) || a0 != null) Init(a0);
	}

	public void Init(TV[] a0 = null)
	{
		for (int i = 1; i < n2; ++i) a1[i] = id;

		if (a0 == null)
		{
			for (int i = 1; i < n2; ++i) a2[i] = v0;
		}
		else
		{
			Array.Copy(a0, 0, a2, n2 >> 1, a0.Length);
			for (int i = (n2 >> 1) + a0.Length; i < n2; ++i) a2[i] = v0;
			for (int i = (n2 >> 1) - 1; i > 0; --i) a2[i] = Union(a2[i << 1], a2[(i << 1) + 1]);
		}
	}

	public STNode Actual(int i) => (n2 >> 1) + i;
	public int Original(STNode n) => n.i - (n2 >> 1);

	void PushDown(STNode n, int length)
	{
		var op = a1[n.i];
		if (TOEquals(op, id)) return;
		STNode c0 = n.Child0, c1 = n.Child1;
		a1[c0.i] = Multiply(op, a1[c0.i]);
		a1[c1.i] = Multiply(op, a1[c1.i]);
		a2[c0.i] = Transform(op, a2[c0.i], c0, length >> 1);
		a2[c1.i] = Transform(op, a2[c1.i], c1, length >> 1);
		a1[n.i] = id;
	}

	public void Set(int i, TO op) => Set(i, i + 1, op);
	public void Set(int l_in, int r_ex, TO op)
	{
		int al = (n2 >> 1) + l_in, ar = (n2 >> 1) + r_ex;

		Action<STNode, int> Dfs = null;
		Dfs = (n, length) =>
		{
			int nl = n.i * length, nr = nl + length;

			if (al <= nl && nr <= ar)
			{
				a1[n.i] = Multiply(op, a1[n.i]);
				a2[n.i] = Transform(op, a2[n.i], n, length);
			}
			else
			{
				PushDown(n, length);
				var nm = (nl + nr) >> 1;
				if (al < nm && nl < ar) Dfs(n.Child0, length >> 1);
				if (al < nr && nm < ar) Dfs(n.Child1, length >> 1);
				a2[n.i] = Union(a2[n.Child0.i], a2[n.Child1.i]);
			}
		};
		Dfs(1, n2 >> 1);
	}

	public TV Get(int i) => Get(i, i + 1);
	public TV Get(int l_in, int r_ex)
	{
		int al = (n2 >> 1) + l_in, ar = (n2 >> 1) + r_ex;

		var v = v0;
		Action<STNode, int> Dfs = null;
		Dfs = (n, length) =>
		{
			int nl = n.i * length, nr = nl + length;

			if (al <= nl && nr <= ar)
			{
				v = Union(v, a2[n.i]);
			}
			else
			{
				PushDown(n, length);
				var nm = (nl + nr) >> 1;
				if (al < nm && nl < ar) Dfs(n.Child0, length >> 1);
				if (al < nr && nm < ar) Dfs(n.Child1, length >> 1);
			}
		};
		Dfs(1, n2 >> 1);
		return v;
	}

	// (previous, node, length) => result
	public TR Aggregate<TR>(int l_in, int r_ex, TR r0, Func<TR, STNode, int, TR> func)
	{
		int al = (n2 >> 1) + l_in, ar = (n2 >> 1) + r_ex;

		var rv = r0;
		Action<STNode, int> Dfs = null;
		Dfs = (n, length) =>
		{
			int nl = n.i * length, nr = nl + length;

			if (al <= nl && nr <= ar)
			{
				rv = func(rv, n, length);
			}
			else
			{
				PushDown(n, length);
				var nm = (nl + nr) >> 1;
				if (al < nm && nl < ar) Dfs(n.Child0, length >> 1);
				if (al < nr && nm < ar) Dfs(n.Child1, length >> 1);
			}
		};
		Dfs(1, n2 >> 1);
		return rv;
	}
}
