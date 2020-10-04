using System;

// 一点更新・範囲取得
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
