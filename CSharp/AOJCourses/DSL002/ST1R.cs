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
	public int[] length;

	// (newOp, oldOp) => product
	public Func<T, T, T> Multiply;
	public T id;

	// 全ノードを、恒等変換を表す値で初期化します。
	public STR(int n, Func<T, T, T> multiply, T _id)
	{
		while (n2 < n) n2 <<= 1;
		a = new T[n2 <<= 1];

		length = new int[n2];
		for (int f = 1, l = n2 >> 1; f < n2; f <<= 1, l >>= 1)
			for (int i = 0; i < f; i++)
				length[f + i] = l;

		Multiply = multiply;
		id = _id;
		if (!Equals(id, default(T))) Init();
	}

	public Node Actual(int i) => (n2 >> 1) + i;
	public T this[Node n]
	{
		get { return a[n.i]; }
		set { a[n.i] = value; }
	}

	public void Init() { for (int i = 1; i < n2; ++i) a[i] = id; }

	// Top-down
	public void Set(int lIn, int rEx, T v)
	{
		int al = Actual(lIn).i, ar = Actual(rEx).i;

		Action<Node> Dfs = null;
		Dfs = n =>
		{
			var nl = n.i * length[n.i];
			var nr = nl + length[n.i];

			if (al <= nl && nr <= ar)
			{
				// 対象のノード
				this[n] = Multiply(v, this[n]);
			}
			else
			{
				this[n.Child0] = Multiply(this[n], this[n.Child0]);
				this[n.Child1] = Multiply(this[n], this[n.Child1]);
				this[n] = id;

				var nm = (nl + nr) >> 1;
				if (al < nm && nl < ar) Dfs(n.Child0);
				if (al < nr && nm < ar) Dfs(n.Child1);
			}
		};
		Dfs(1);
	}

	// Top-down
	public T Get(int i)
	{
		var ai = Actual(i).i;
		for (Node n = 1; n.i < ai; n = ai < n.Child1.i * length[n.Child1.i] ? n.Child0 : n.Child1)
		{
			this[n.Child0] = Multiply(this[n], this[n.Child0]);
			this[n.Child1] = Multiply(this[n], this[n.Child1]);
			this[n] = id;
		}
		return a[ai];
	}
}
