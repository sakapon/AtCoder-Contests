using System;

// 一点更新・範囲取得
// T は値を表します。
class ST1<T>
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
	public int n2 = 1;
	public T[] a;

	public ST1(int n)
	{
		while (n2 < n) n2 <<= 1;
		a = new T[n2 <<= 1];
	}

	public Node Actual(int i) => (n2 >> 1) + i;
	public T this[Node n]
	{
		get { return a[n.i]; }
		set { a[n.i] = value; }
	}
	public T this[int i] => this[Actual(i)];

	// Bottom-up
	public void Init(T[] vs, T other, Func<T, T, T> union)
	{
		for (int i = 0; i < vs.Length; ++i)
			a[(n2 >> 1) + i] = vs[i];
		for (int i = (n2 >> 1) + vs.Length; i < n2; ++i)
			a[i] = other;

		for (int i = (n2 >> 1) - 1; i > 0; --i)
		{
			Node n = i;
			this[n] = union(this[n.Child0], this[n.Child1]);
		}
	}

	// Bottom-up
	public void Set(int i, T v, Func<T, T, T> union)
	{
		var n = Actual(i);
		this[n] = v;
		while ((n = n.Parent).i > 0) this[n] = union(this[n.Child0], this[n.Child1]);
	}

	// 範囲の昇順
	public T Get(int minIn, int maxEx, T v0, Func<T, T, T> union)
	{
		int l = (n2 >> 1) + minIn, r = (n2 >> 1) + maxEx;
		while (l < r)
		{
			var length = l & -l;
			while (l + length > r) length >>= 1;
			v0 = union(v0, a[l / length]);
			l += length;
		}
		return v0;
	}
}

// 範囲更新・一点取得
// ST1 の双対となる概念です。
// T は作用素を表します。
class STR<T>
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
	public T[] a;

	// (newOp, oldOp) => product
	public Func<T, T, T> Multiply;
	public T id;
	Func<T, T, bool> TEquals = System.Collections.Generic.EqualityComparer<T>.Default.Equals;

	// 全ノードを、恒等変換を表す値で初期化します。
	public STR(int n, Func<T, T, T> multiply, T _id)
	{
		while (n2 < n) n2 <<= 1;
		a = new T[n2 <<= 1];

		Multiply = multiply;
		id = _id;
		if (!TEquals(id, default(T))) Init();
	}

	public STNode Actual(int i) => (n2 >> 1) + i;
	public T this[STNode n]
	{
		get { return a[n.i]; }
		set { a[n.i] = value; }
	}

	public void Init() { for (int i = 1; i < n2; ++i) a[i] = id; }

	void PushDown(STNode n)
	{
		if (TEquals(a[n.i], id)) return;
		STNode c0 = n.Child0, c1 = n.Child1;
		a[c0.i] = Multiply(a[n.i], a[c0.i]);
		a[c1.i] = Multiply(a[n.i], a[c1.i]);
		a[n.i] = id;
	}

	// Top-down
	public void Set(int l_in, int r_ex, T v)
	{
		int al = (n2 >> 1) + l_in, ar = (n2 >> 1) + r_ex;

		Action<STNode, int> Dfs = null;
		Dfs = (n, length) =>
		{
			var nl = n.i * length;
			var nr = nl + length;

			if (al <= nl && nr <= ar)
			{
				// 対象のノード
				a[n.i] = Multiply(v, a[n.i]);
			}
			else
			{
				PushDown(n);
				var nm = (nl + nr) >> 1;
				if (al < nm && nl < ar) Dfs(n.Child0, length >> 1);
				if (al < nr && nm < ar) Dfs(n.Child1, length >> 1);
			}
		};
		Dfs(1, n2 >> 1);
	}

	// Top-down
	public T Get(int i)
	{
		var ai = (n2 >> 1) + i;
		var length = n2 >> 1;
		for (STNode n = 1; n.i < ai; n = ai < n.i * length + (length >> 1) ? n.Child0 : n.Child1, length >>= 1)
			PushDown(n);
		return a[ai];
	}
}
