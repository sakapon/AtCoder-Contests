using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
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

	public class AvlMap<TKey, TValue> : BstMapBase<TKey, TValue, AvlSetNode<KeyValuePair<TKey, TValue>>>
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

		AvlSetNode<KeyValuePair<TKey, TValue>> Add(AvlSetNode<KeyValuePair<TKey, TValue>> node, TKey key, TValue value)
		{
			if (node == null)
			{
				++Count;
				return new AvlSetNode<KeyValuePair<TKey, TValue>> { Key = new KeyValuePair<TKey, TValue>(key, value) };
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
				node = node.RotateToRight() as AvlSetNode<KeyValuePair<TKey, TValue>>;
				node.TypedRight.UpdateHeight();
			}
			else if (lrh < -2 || lrh == -2 && node.TypedRight.LeftHeight <= node.TypedRight.RightHeight)
			{
				node = node.RotateToLeft() as AvlSetNode<KeyValuePair<TKey, TValue>>;
				node.TypedLeft.UpdateHeight();
			}

			node.UpdateHeight();
			return node;
		}

		public override bool Remove(TKey key)
		{
			var node = Root.SearchNode(key, compare) as AvlSetNode<KeyValuePair<TKey, TValue>>;
			if (node == null) return false;

			RemoveTarget(node);
			--Count;
			return true;
		}

		// Suppose t != null.
		void RemoveTarget(AvlSetNode<KeyValuePair<TKey, TValue>> t)
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
				var t2 = t.SearchNextNode() as AvlSetNode<KeyValuePair<TKey, TValue>>;
				t.Key = t2.Key;
				RemoveTarget(t2);
			}
		}
	}
}
