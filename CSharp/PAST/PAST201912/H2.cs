using System;

class H2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var c = Read();
		var qc = int.Parse(Console.ReadLine());

		Func<int, int> map = i => i % 2 == 0 ? i / 2 : (n + i) / 2;

		var c2 = new int[n];
		for (int i = 0; i < n; i++)
			c2[map(i)] = c[i];

		var st = new LST<int, int>(n,
			(x, y) => x + y, 0,
			Math.Min, int.MaxValue,
			(x, p, _, l) => p + x,
			c2);

		var r = 0L;
		for (int k = 0; k < qc; k++)
		{
			var q = Read();
			if (q[0] == 1)
			{
				if (st.Get(map(q[1] - 1)) >= q[2])
				{
					st.Set(map(q[1] - 1), -q[2]);
					r += q[2];
				}
			}
			else if (q[0] == 2)
			{
				if (st.Get(0, n / 2) >= q[1])
				{
					st.Set(0, n / 2, -q[1]);
					r += (long)(n + 1) / 2 * q[1];
				}
			}
			else
			{
				if (st.Get(0, n) >= q[1])
				{
					st.Set(0, n, -q[1]);
					r += (long)n * q[1];
				}
			}
		}
		Console.WriteLine(r);
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
}
