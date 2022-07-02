using System;
using System.Collections.Generic;
using System.Linq;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_A
// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_B
// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/5/GRL/5/GRL_5_D
// Test: https://atcoder.jp/contests/practice2/tasks/practice2_b
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

	// leaves-to-root segment binary tree
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class MergeSBT<TValue>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Value}, Count = {Count}\}")]
		public class Node
		{
			public TValue Value;
			public int Count = 1;
			public Node Parent, Left, Right;
		}

		public Monoid<TValue> Merge { get; }
		Node[] Leaves;
		public int Count => Leaves.Length;
		Node Root;

		public MergeSBT(int n, Monoid<TValue> merge, IEnumerable<TValue> items = null)
		{
			Merge = merge;
			var a = items as TValue[] ?? items?.ToArray();
			if (a?.Length < n) throw new ArgumentException("n items must be given.", nameof(items));
			Leaves = new Node[n];
			for (int i = 0; i < n; ++i) Leaves[i] = new Node { Value = a != null ? a[i] : merge.Id };
			Root = CreateSubtree(0, n);
		}

		Node CreateSubtree(int l, int r)
		{
			if (r - l == 1) return Leaves[l];
			var m = (l + r) >> 1;
			var n = new Node
			{
				Count = r - l,
				Left = CreateSubtree(l, m),
				Right = CreateSubtree(m, r),
			};
			n.Left.Parent = n.Right.Parent = n;
			n.Value = Merge.Op(n.Left.Value, n.Right.Value);
			return n;
		}

		public TValue this[int index]
		{
			get => Leaves[index].Value;
			set
			{
				Leaves[index].Value = value;
				for (var n = Leaves[index].Parent; n != null; n = n.Parent)
					n.Value = Merge.Op(n.Left.Value, n.Right.Value);
			}
		}

		public TValue this[int left, int right] => left < right ? GetRange(Root, left, right) : Merge.Id;

		TValue GetRange(Node n, int l, int r)
		{
			if (l <= 0 && n.Count <= r) return n.Value;
			var lc = n.Left.Count;
			if (r <= lc) return GetRange(n.Left, l, r);
			if (lc <= l) return GetRange(n.Right, l - lc, r - lc);
			return Merge.Op(GetRange(n.Left, l, lc), GetRange(n.Right, 0, r - lc));
		}
	}
}
