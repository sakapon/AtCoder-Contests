using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/8/ITP2_8_C
	// Test: https://atcoder.jp/contests/past202104-open/tasks/past202104_m
	public class AvlMap<TKey, TValue> : BstMapBase<TKey, TValue, AvlNode<KeyValuePair<TKey, TValue>>>
	{
		public static AvlMap<TKey, TValue> Create(bool descending = false) =>
			new AvlMap<TKey, TValue>(ComparisonHelper.Create<TKey>(descending));

		public AvlMap(Comparison<TKey> comparison = null) : base(comparison) { }

		public override bool Add(TKey key, TValue value)
		{
			var c = Count;
			Root = Add(Root, key, value);
			return Count != c;
		}

		AvlNode<KeyValuePair<TKey, TValue>> Add(AvlNode<KeyValuePair<TKey, TValue>> node, TKey key, TValue value)
		{
			if (node == null)
			{
				++Count;
				return new AvlNode<KeyValuePair<TKey, TValue>> { Key = new KeyValuePair<TKey, TValue>(key, value) };
			}

			var d = compare(key, node.Key.Key);
			if (d == 0) return node;

			if (d < 0)
			{
				node.TypedLeft = Add(node.TypedLeft, key, value);
			}
			else
			{
				node.TypedRight = Add(node.TypedRight, key, value);
			}

			var lrh = node.LeftHeight - node.RightHeight;
			if (lrh > 2 || lrh == 2 && node.TypedLeft.LeftHeight >= node.TypedLeft.RightHeight)
			{
				node = node.RotateToRight() as AvlNode<KeyValuePair<TKey, TValue>>;
				node.TypedRight.UpdateHeight();
			}
			else if (lrh < -2 || lrh == -2 && node.TypedRight.LeftHeight <= node.TypedRight.RightHeight)
			{
				node = node.RotateToLeft() as AvlNode<KeyValuePair<TKey, TValue>>;
				node.TypedLeft.UpdateHeight();
			}

			node.UpdateHeight();
			return node;
		}

		public override bool Remove(TKey key)
		{
			var node = Root.SearchNode(key, compare) as AvlNode<KeyValuePair<TKey, TValue>>;
			if (node == null) return false;

			RemoveTarget(node);
			--Count;
			return true;
		}

		// Suppose t != null.
		void RemoveTarget(AvlNode<KeyValuePair<TKey, TValue>> t)
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
				var t2 = t.SearchNextNode() as AvlNode<KeyValuePair<TKey, TValue>>;
				t.Key = t2.Key;
				RemoveTarget(t2);
			}
		}
	}
}
