using System;
using System.Collections.Generic;
using System.Linq;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_D
// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_E
// Test: https://atcoder.jp/contests/abl/tasks/abl_d
namespace CoderLib8.DataTrees.SBTs
{
	public static class Monoid
	{
		public static Monoid<int> Int32_Add { get; } = new Monoid<int>((x, y) => x + y);
		public static Monoid<long> Int64_Add { get; } = new Monoid<long>((x, y) => x + y);
		public static Monoid<int> Int32_Min { get; } = new Monoid<int>((x, y) => x <= y ? x : y, int.MaxValue);
		public static Monoid<int> Int32_Max { get; } = new Monoid<int>((x, y) => x >= y ? x : y, int.MinValue);

		public static Monoid<int> Int32_ArgMin(int[] a) => new Monoid<int>((x, y) => a[x] <= a[y] ? x : y);

		public static Monoid<int> Int32_Update { get; } = new Monoid<int>((x, y) => x != -1 ? x : y, -1);
	}

	public struct Monoid<T>
	{
		public Func<T, T, T> Op;
		public T Id;
		public Monoid(Func<T, T, T> op, T id = default(T)) { Op = op; Id = id; }
	}

	// root-to-leaves segment binary tree
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class PushSBT<TOp>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Op}, Count = {Count}\}")]
		public class Node
		{
			public TOp Op;
			public int Count = 1;
			public Node Left, Right;
		}

		public Monoid<TOp> Push { get; }
		Node[] Leaves;
		public int Count => Leaves.Length;
		Node Root;

		public PushSBT(int n, Monoid<TOp> push, IEnumerable<TOp> items = null)
		{
			Push = push;
			var a = items as TOp[] ?? items?.ToArray();
			if (a?.Length < n) throw new ArgumentException("n items must be given.", nameof(items));
			Leaves = new Node[n];
			for (int i = 0; i < n; ++i) Leaves[i] = new Node { Op = a != null ? a[i] : push.Id };
			Root = CreateSubtree(0, n);
		}

		Node CreateSubtree(int l, int r)
		{
			if (r - l == 1) return Leaves[l];
			var m = (l + r) >> 1;
			return new Node
			{
				Op = Push.Id,
				Count = r - l,
				Left = CreateSubtree(l, m),
				Right = CreateSubtree(m, r),
			};
		}

		public TOp this[int index]
		{
			get => Get(Root, index);
			set => SetRange(Root, index, index + 1, value);
		}

		public TOp this[int left, int right]
		{
			set => SetRange(Root, left, right, value);
		}

		TOp Get(Node n, int i)
		{
			if (n.Count == 1) return n.Op;
			n.Left.Op = Push.Op(n.Op, n.Left.Op);
			n.Right.Op = Push.Op(n.Op, n.Right.Op);
			n.Op = Push.Id;

			if (i < n.Left.Count) return Get(n.Left, i);
			else return Get(n.Right, i - n.Left.Count);
		}

		void SetRange(Node n, int l, int r, TOp op)
		{
			if (l <= 0 && n.Count <= r) { n.Op = Push.Op(op, n.Op); return; }
			n.Left.Op = Push.Op(n.Op, n.Left.Op);
			n.Right.Op = Push.Op(n.Op, n.Right.Op);
			n.Op = Push.Id;

			var lc = n.Left.Count;
			if (l < lc) SetRange(n.Left, l, lc < r ? lc : r, op);
			if (lc < r) SetRange(n.Right, l < lc ? 0 : l - lc, r - lc, op);
		}

		public TOp[] ToArray()
		{
			PushAll(Root);
			return Array.ConvertAll(Leaves, n => n.Op);
		}

		void PushAll(Node n)
		{
			if (n.Count == 1) return;
			n.Left.Op = Push.Op(n.Op, n.Left.Op);
			n.Right.Op = Push.Op(n.Op, n.Right.Op);
			n.Op = Push.Id;

			PushAll(n.Left);
			PushAll(n.Right);
		}
	}
}
