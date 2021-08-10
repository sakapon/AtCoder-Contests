using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/7/ITP2_7_C
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_f
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_g
	// Test: https://atcoder.jp/contests/past202104-open/tasks/past202104_m
	// Test: https://atcoder.jp/contests/past202107-open/tasks/past202107_l
	public class AvlSetNode<TKey> : BstNodeBase<TKey>
	{
		public override BstNodeBase<TKey> Parent
		{
			get { return TypedParent; }
			set { TypedParent = (AvlSetNode<TKey>)value; }
		}
		public override BstNodeBase<TKey> Left
		{
			get { return TypedLeft; }
			set { TypedLeft = (AvlSetNode<TKey>)value; }
		}
		public override BstNodeBase<TKey> Right
		{
			get { return TypedRight; }
			set { TypedRight = (AvlSetNode<TKey>)value; }
		}

		public AvlSetNode<TKey> TypedParent { get; set; }

		AvlSetNode<TKey> _left;
		public AvlSetNode<TKey> TypedLeft
		{
			get { return _left; }
			set
			{
				_left = value;
				if (value != null) value.TypedParent = this;
			}
		}

		AvlSetNode<TKey> _right;
		public AvlSetNode<TKey> TypedRight
		{
			get { return _right; }
			set
			{
				_right = value;
				if (value != null) value.TypedParent = this;
			}
		}

		public int Height { get; set; } = 1;
		public int LeftHeight => TypedLeft?.Height ?? 0;
		public int RightHeight => TypedRight?.Height ?? 0;

		public void UpdateHeight(bool recursive = false)
		{
			Height = Math.Max(LeftHeight, RightHeight) + 1;
			if (recursive) TypedParent?.UpdateHeight(true);
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
			Root = Add(Root, item);
			return Count != c;
		}

		AvlSetNode<T> Add(AvlSetNode<T> node, T item)
		{
			if (node == null)
			{
				++Count;
				return new AvlSetNode<T> { Key = item };
			}

			var d = compare(item, node.Key);
			if (d == 0) return node;

			if (d < 0)
			{
				node.TypedLeft = Add(node.TypedLeft, item);
			}
			else
			{
				node.TypedRight = Add(node.TypedRight, item);
			}

			var lrh = node.LeftHeight - node.RightHeight;
			if (lrh > 2 || lrh == 2 && node.TypedLeft.LeftHeight >= node.TypedLeft.RightHeight)
			{
				node = node.RotateToRight() as AvlSetNode<T>;
				node.TypedRight.UpdateHeight();
			}
			else if (lrh < -2 || lrh == -2 && node.TypedRight.LeftHeight <= node.TypedRight.RightHeight)
			{
				node = node.RotateToLeft() as AvlSetNode<T>;
				node.TypedLeft.UpdateHeight();
			}

			node.UpdateHeight();
			return node;
		}

		public override bool Remove(T item)
		{
			var node = Root.SearchNode(item, compare) as AvlSetNode<T>;
			if (node == null) return false;

			RemoveTarget(node);
			--Count;
			return true;
		}

		// Suppose t != null.
		void RemoveTarget(AvlSetNode<T> t)
		{
			if (t.TypedLeft == null || t.TypedRight == null)
			{
				var c = t.TypedLeft ?? t.TypedRight;

				if (t.TypedParent == null)
				{
					Root = c;
				}
				else if (t.TypedParent.TypedLeft == t)
				{
					t.TypedParent.TypedLeft = c;
				}
				else
				{
					t.TypedParent.TypedRight = c;
				}

				t.TypedParent?.UpdateHeight(true);
			}
			else
			{
				var t2 = t.SearchNextNode() as AvlSetNode<T>;
				t.Key = t2.Key;
				RemoveTarget(t2);
			}
		}
	}
}
