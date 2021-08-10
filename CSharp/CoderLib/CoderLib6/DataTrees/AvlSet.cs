using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/7/ITP2_7_C
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_f
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_g
	// Test: https://atcoder.jp/contests/past202104-open/tasks/past202104_m
	// Test: https://atcoder.jp/contests/past202107-open/tasks/past202107_l
	public class AvlSetNode<TKey> : BstNode<TKey>
	{
		public int Height { get; set; } = 1;

		public AvlSetNode<TKey> TypedParent => Parent as AvlSetNode<TKey>;
		public AvlSetNode<TKey> TypedLeft => Left as AvlSetNode<TKey>;
		public AvlSetNode<TKey> TypedRight => Right as AvlSetNode<TKey>;

		public void UpdateHeight()
		{
			var lh = TypedLeft?.Height ?? 0;
			var rh = TypedRight?.Height ?? 0;
			Height = Math.Max(lh, rh) + 1;
		}
	}

	public class AvlSet<T> : BstBase<T, AvlSetNode<T>>
	{
		public static AvlSet<T> Create(bool descending = false) =>
			new AvlSet<T>(ComparisonHelper.Create<T>(descending));
		public static AvlSet<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false) =>
			new AvlSet<T>(ComparisonHelper.Create(keySelector, descending));

		public AvlSet(Comparison<T> comparison = null) : base(comparison) { }

		public override bool Add(T item)
		{
			var c = Count;
			Root = Add(Root, item) as AvlSetNode<T>;
			return Count != c;
		}

		BstNode<T> Add(BstNode<T> node, T item)
		{
			if (node == null)
			{
				++Count;
				return new AvlSetNode<T> { Key = item };
			}

			var d = compare(item, node.Key);
			if (d == 0) return node;

			var t = node as AvlSetNode<T>;
			if (d < 0)
			{
				node.Left = Add(node.Left, item);

				var lh = t.TypedLeft?.Height ?? 0;
				var rh = t.TypedRight?.Height ?? 0;
				if (lh - rh > 1)
				{
					node = node.RotateToRight();
					t.UpdateHeight();
					(node as AvlSetNode<T>).UpdateHeight();
				}
				else
				{
					t.UpdateHeight();
				}
			}
			else
			{
				node.Right = Add(node.Right, item);

				var lh = t.TypedLeft?.Height ?? 0;
				var rh = t.TypedRight?.Height ?? 0;
				if (rh - lh > 1)
				{
					node = node.RotateToLeft();
					t.UpdateHeight();
					(node as AvlSetNode<T>).UpdateHeight();
				}
				else
				{
					t.UpdateHeight();
				}
			}
			return node;
		}

		public override bool Remove(T item)
		{
			var node = Root.SearchNode(item, compare);
			if (node == null) return false;

			RemoveTarget(node as AvlSetNode<T>);
			--Count;
			return true;
		}

		// Suppose t != null.
		void RemoveTarget(AvlSetNode<T> t)
		{
			if (t.Left == null || t.Right == null)
			{
				var c = t.Left ?? t.Right;

				if (t.Parent == null)
				{
					Root = c as AvlSetNode<T>;
				}
				else if (t.Parent.Left == t)
				{
					t.Parent.Left = c;
				}
				else
				{
					t.Parent.Right = c;
				}

				UpdateHeight(t.TypedParent);
			}
			else
			{
				var t2 = t.SearchNextNode();
				t.Key = t2.Key;
				RemoveTarget(t2 as AvlSetNode<T>);
			}
		}

		// Bottom up.
		void UpdateHeight(AvlSetNode<T> node)
		{
			if (node == null) return;
			node.UpdateHeight();
			UpdateHeight(node.TypedParent);
		}
	}
}
