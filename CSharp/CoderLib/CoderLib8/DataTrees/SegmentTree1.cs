using System;

namespace CoderLib8.DataTrees
{
	// 一点更新・範囲取得
	// STR の双対となる概念です。
	// TV は値を表します。
	// 外見上は 0-indexed, 0 <= i < n
	// 内部では 1-indexed, 1 <= i < n2
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/5/ALDS1_5_D
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_A
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_B
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_b
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_j
	class ST1<TV>
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
		public TV[] a2;

		public Func<TV, TV, TV> Union;
		public TV v0;

		// 全ノードを、零元を表す値で初期化します (零元の Union もまた零元)。
		public ST1(int n, Func<TV, TV, TV> union, TV _v0, TV[] a0 = null)
		{
			while (n2 < n << 1) n2 <<= 1;
			a2 = new TV[n2];

			Union = union;
			v0 = _v0;
			if (!Equals(v0, default(TV)) || a0 != null) Init(a0);
		}

		public void Init(TV[] a0 = null)
		{
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
		public TV this[STNode n]
		{
			get { return a2[n.i]; }
			set { a2[n.i] = value; }
		}

		// Bottom-up
		public void Set(int i, TV v)
		{
			var n = Actual(i);
			a2[n.i] = v;
			while ((n = n.Parent).i > 0) a2[n.i] = Union(a2[n.Child0.i], a2[n.Child1.i]);
		}

		public TV Get(int i) => a2[(n2 >> 1) + i];
		// 範囲の昇順
		public TV Get(int l_in, int r_ex) => Aggregate(l_in, r_ex, v0, (p, n, l) => Union(p, a2[n.i]));

		// 範囲の昇順
		// (previous, node, length) => result
		public TR Aggregate<TR>(int l_in, int r_ex, TR r0, Func<TR, STNode, int, TR> func)
		{
			int al = (n2 >> 1) + l_in, ar = (n2 >> 1) + r_ex;

			var rv = r0;
			while (al < ar)
			{
				var length = al & -al;
				while (al + length > ar) length >>= 1;
				rv = func(rv, al / length, length);
				al += length;
			}
			return rv;
		}
	}
}
