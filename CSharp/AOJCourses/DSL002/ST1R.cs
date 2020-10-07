using System;

// 一点更新・範囲取得
// T は値を表します。
class ST1<T>
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

	public Func<T, T, T> Union;
	public T v0;

	// 全ノードを、零元を表す値で初期化します (零元の Union もまた零元)。
	public ST1(int n, Func<T, T, T> union, T _v0)
	{
		while (n2 < n) n2 <<= 1;
		a = new T[n2 <<= 1];

		Union = union;
		v0 = _v0;
		if (!Equals(v0, default(T))) Init();
	}

	public void Init() { for (int i = 1; i < n2; ++i) a[i] = v0; }

	public STNode Actual(int i) => (n2 >> 1) + i;
	public int Original(STNode n) => n.i - (n2 >> 1);
	public T this[STNode n]
	{
		get { return a[n.i]; }
		set { a[n.i] = value; }
	}
	public T this[int i] => a[(n2 >> 1) + i];

	// Bottom-up
	public void Set(int i, T v)
	{
		var n = Actual(i);
		a[n.i] = v;
		while ((n = n.Parent).i > 0) a[n.i] = Union(a[n.Child0.i], a[n.Child1.i]);
	}

	// 範囲の昇順
	public T Get(int l_in, int r_ex)
	{
		int l = (n2 >> 1) + l_in, r = (n2 >> 1) + r_ex;

		var v = v0;
		while (l < r)
		{
			var length = l & -l;
			while (l + length > r) length >>= 1;
			v = Union(v, a[l / length]);
			l += length;
		}
		return v;
	}

	// 範囲の昇順
	// (previous, node, length) => result
	public TV Aggregate<TV>(int l_in, int r_ex, TV v0, Func<TV, STNode, int, TV> func)
	{
		int l = (n2 >> 1) + l_in, r = (n2 >> 1) + r_ex;

		var v = v0;
		while (l < r)
		{
			var length = l & -l;
			while (l + length > r) length >>= 1;
			v = func(v, l / length, length);
			l += length;
		}
		return v;
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

	public void Init() { for (int i = 1; i < n2; ++i) a[i] = id; }

	public STNode Actual(int i) => (n2 >> 1) + i;
	public int Original(STNode n) => n.i - (n2 >> 1);
	public T this[STNode n]
	{
		get { return a[n.i]; }
		set { a[n.i] = value; }
	}

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
			int nl = n.i * length, nr = nl + length;

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
