﻿using System;

namespace CoderLib8.Trees
{
	// 一点更新・範囲取得
	// T は値を表します。
	// 外見上は 0-indexed, 0 <= i < n
	// 内部では 1-indexed, 1 <= i < n2
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/5/ALDS1_5_D
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_A
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_B
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_b
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_j
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
		public T Get(int l_in, int r_ex) => Aggregate(l_in, r_ex, v0, (p, n, l) => Union(p, a[n.i]));

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
}
