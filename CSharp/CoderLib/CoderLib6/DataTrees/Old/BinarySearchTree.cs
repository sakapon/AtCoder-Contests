using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/8/ALDS1_8_C
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_g
	public class BstNode<TKey> : BstNodeBase<TKey>
	{
		public override BstNodeBase<TKey> Parent
		{
			get { return TypedParent; }
			set { TypedParent = (BstNode<TKey>)value; }
		}
		public override BstNodeBase<TKey> Left
		{
			get { return TypedLeft; }
			set { TypedLeft = (BstNode<TKey>)value; }
		}
		public override BstNodeBase<TKey> Right
		{
			get { return TypedRight; }
			set { TypedRight = (BstNode<TKey>)value; }
		}

		public BstNode<TKey> TypedParent { get; set; }

		BstNode<TKey> _left;
		public BstNode<TKey> TypedLeft
		{
			get { return _left; }
			set
			{
				_left = value;
				if (value != null) value.TypedParent = this;
			}
		}

		BstNode<TKey> _right;
		public BstNode<TKey> TypedRight
		{
			get { return _right; }
			set
			{
				_right = value;
				if (value != null) value.TypedParent = this;
			}
		}
	}

	public class BinarySearchTree<T> : BstBase<T, BstNode<T>>
	{
		public static BinarySearchTree<T> Create(bool descending = false) =>
			new BinarySearchTree<T>(ComparisonHelper.Create<T>(descending));
		public static BinarySearchTree<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false) =>
			new BinarySearchTree<T>(ComparisonHelper.Create(keySelector, descending));

		public BinarySearchTree(Comparison<T> comparison = null) : base(comparison) { }

		public BinarySearchTree(IEnumerable<T> collection, Comparison<T> comparison = null) : base(comparison)
		{
			if (collection == null) throw new ArgumentNullException();
			var set = new HashSet<T>(collection);
			var items = new T[set.Count];
			set.CopyTo(items);
			Array.Sort(items, Comparer<T>.Create(compare));
			Root = CreateSubtree(items, 0, items.Length);
		}

		static BstNode<T> CreateSubtree(T[] items, int l, int r)
		{
			if (r - l == 0) return null;
			if (r - l == 1) return new BstNode<T> { Key = items[l] };

			var m = (l + r) / 2;
			return new BstNode<T>
			{
				Key = items[m],
				TypedLeft = CreateSubtree(items, l, m),
				TypedRight = CreateSubtree(items, m + 1, r),
			};
		}

		public override bool Add(T item)
		{
			var c = Count;
			Root = Add(Root, item);
			return Count != c;
		}

		BstNode<T> Add(BstNode<T> node, T item)
		{
			if (node == null)
			{
				++Count;
				return new BstNode<T> { Key = item };
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
			return node;
		}

		public override bool Remove(T item)
		{
			var node = Root.SearchNode(item, compare) as BstNode<T>;
			if (node == null) return false;

			RemoveTarget(node);
			--Count;
			return true;
		}

		// Suppose t != null.
		void RemoveTarget(BstNode<T> t)
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
			}
			else
			{
				var t2 = t.SearchNextNode() as BstNode<T>;
				t.Key = t2.Key;
				RemoveTarget(t2);
			}
		}
	}
}
