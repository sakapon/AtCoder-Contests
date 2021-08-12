using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Key}, {Value}\}")]
	public abstract class BstMapNodeBase<TKey, TValue> : BstNodeBase<TKey>
	{
		public TValue Value { get; set; }
		public KeyValuePair<TKey, TValue> Pair => new KeyValuePair<TKey, TValue>(Key, Value);
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public abstract class BstMapBase2<TKey, TValue, TNode> : IEnumerable<KeyValuePair<TKey, TValue>> where TNode : BstMapNodeBase<TKey, TValue>
	{
		TNode _root;
		public TNode Root
		{
			get { return _root; }
			protected set
			{
				_root = value;
				if (value != null) value.Parent = null;
			}
		}

		protected Comparison<TKey> compare;
		public int Count { get; protected set; }

		protected BstMapBase2(Comparison<TKey> comparison = null)
		{
			compare = comparison ?? Comparer<TKey>.Default.Compare;
		}

		public bool ContainsKey(TKey key)
		{
			return Root.SearchNode(key, compare) != null;
		}

		public KeyValuePair<TKey, TValue> GetMin()
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return (Root.SearchMinNode() as TNode).Pair;
		}

		public KeyValuePair<TKey, TValue> GetMax()
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return (Root.SearchMaxNode() as TNode).Pair;
		}

		public KeyValuePair<TKey, TValue> GetMin(Func<TKey, bool> predicate)
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return (Root.SearchMinNode(predicate) as TNode).Pair;
		}

		public KeyValuePair<TKey, TValue> GetMax(Func<TKey, bool> predicate)
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return (Root.SearchMaxNode(predicate) as TNode).Pair;
		}

		public KeyValuePair<TKey, TValue> GetPrevious(TKey key)
		{
			var node = Root.SearchNode(key, compare);
			if (node == null) throw new InvalidOperationException("The item is not found.");
			node = node.SearchPreviousNode();
			if (node == null) throw new InvalidOperationException("The target item is not found.");
			return (node as TNode).Pair;
		}

		public KeyValuePair<TKey, TValue> GetNext(TKey key)
		{
			var node = Root.SearchNode(key, compare);
			if (node == null) throw new InvalidOperationException("The item is not found.");
			node = node.SearchNextNode();
			if (node == null) throw new InvalidOperationException("The target item is not found.");
			return (node as TNode).Pair;
		}

		public IEnumerable<KeyValuePair<TKey, TValue>> GetItems()
		{
			for (var n = Root.SearchMinNode(); n != null; n = n.SearchNextNode())
			{
				yield return (n as TNode).Pair;
			}
		}

		public IEnumerable<KeyValuePair<TKey, TValue>> GetItems(Func<TKey, bool> predicateForMin, Func<TKey, bool> predicateForMax)
		{
			for (var n = Root.SearchMinNode(predicateForMin); n != null && predicateForMax(n.Key); n = n.SearchNextNode())
			{
				yield return (n as TNode).Pair;
			}
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => GetItems().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();

		public abstract bool Add(TKey key, TValue value);
		public abstract bool Remove(TKey key);

		public TValue this[TKey key]
		{
			get
			{
				var node = Root.SearchNode(key, compare) as TNode;
				if (node == null) throw new InvalidOperationException("The item is not found.");
				return node.Value;
			}
			set
			{
				var node = Root.SearchNode(key, compare) as TNode;
				if (node == null) Add(key, value);
				else node.Value = value;
			}
		}
	}

	public class AvlMapNode<TKey, TValue> : BstMapNodeBase<TKey, TValue>
	{
		public override BstNodeBase<TKey> Parent
		{
			get { return TypedParent; }
			set { TypedParent = (AvlMapNode<TKey, TValue>)value; }
		}
		public override BstNodeBase<TKey> Left
		{
			get { return TypedLeft; }
			set { TypedLeft = (AvlMapNode<TKey, TValue>)value; }
		}
		public override BstNodeBase<TKey> Right
		{
			get { return TypedRight; }
			set { TypedRight = (AvlMapNode<TKey, TValue>)value; }
		}

		public AvlMapNode<TKey, TValue> TypedParent { get; set; }

		AvlMapNode<TKey, TValue> _left;
		public AvlMapNode<TKey, TValue> TypedLeft
		{
			get { return _left; }
			set
			{
				_left = value;
				if (value != null) value.TypedParent = this;
			}
		}

		AvlMapNode<TKey, TValue> _right;
		public AvlMapNode<TKey, TValue> TypedRight
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

	public class AvlMap2<TKey, TValue> : BstMapBase2<TKey, TValue, AvlMapNode<TKey, TValue>>
	{
		public static AvlMap2<TKey, TValue> Create(bool descending = false) =>
			new AvlMap2<TKey, TValue>(ComparisonHelper.Create<TKey>(descending));

		public AvlMap2(Comparison<TKey> comparison = null) : base(comparison) { }

		public override bool Add(TKey key, TValue value)
		{
			var c = Count;
			Root = Add(Root, key, value);
			return Count != c;
		}

		AvlMapNode<TKey, TValue> Add(AvlMapNode<TKey, TValue> node, TKey key, TValue value)
		{
			if (node == null)
			{
				++Count;
				return new AvlMapNode<TKey, TValue> { Key = key, Value = value };
			}

			var d = compare(key, node.Key);
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
				node = node.RotateToRight() as AvlMapNode<TKey, TValue>;
				node.TypedRight.UpdateHeight();
			}
			else if (lrh < -2 || lrh == -2 && node.TypedRight.LeftHeight <= node.TypedRight.RightHeight)
			{
				node = node.RotateToLeft() as AvlMapNode<TKey, TValue>;
				node.TypedLeft.UpdateHeight();
			}

			node.UpdateHeight();
			return node;
		}

		public override bool Remove(TKey key)
		{
			var node = Root.SearchNode(key, compare) as AvlMapNode<TKey, TValue>;
			if (node == null) return false;

			RemoveTarget(node);
			--Count;
			return true;
		}

		// Suppose t != null.
		void RemoveTarget(AvlMapNode<TKey, TValue> t)
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
				var t2 = t.SearchNextNode() as AvlMapNode<TKey, TValue>;
				t.Key = t2.Key;
				t.Value = t2.Value;
				RemoveTarget(t2);
			}
		}
	}
}
