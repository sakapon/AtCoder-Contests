using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Key}\}")]
	public abstract class BstNodeBase<TKey>
	{
		public TKey Key { get; set; }
		public abstract BstNodeBase<TKey> Parent { get; set; }
		public abstract BstNodeBase<TKey> Left { get; set; }
		public abstract BstNodeBase<TKey> Right { get; set; }
	}

	public static class BstNode
	{
		public static BstNodeBase<TKey> SearchNode<TKey>(this BstNodeBase<TKey> node, TKey key, Comparison<TKey> comparison)
		{
			if (node == null) return null;
			var d = comparison(key, node.Key);
			if (d == 0) return node;
			if (d < 0) return SearchNode(node.Left, key, comparison);
			else return SearchNode(node.Right, key, comparison);
		}

		public static BstNodeBase<KeyValuePair<TKey, TValue>> SearchNode<TKey, TValue>(this BstNodeBase<KeyValuePair<TKey, TValue>> node, TKey key, Comparison<TKey> comparison)
		{
			if (node == null) return null;
			var d = comparison(key, node.Key.Key);
			if (d == 0) return node;
			if (d < 0) return SearchNode(node.Left, key, comparison);
			else return SearchNode(node.Right, key, comparison);
		}

		public static BstNodeBase<TKey> SearchMinNode<TKey>(this BstNodeBase<TKey> node)
		{
			if (node == null) return null;
			return SearchMinNode(node.Left) ?? node;
		}

		public static BstNodeBase<TKey> SearchMaxNode<TKey>(this BstNodeBase<TKey> node)
		{
			if (node == null) return null;
			return SearchMaxNode(node.Right) ?? node;
		}

		public static BstNodeBase<TKey> SearchMinNode<TKey>(this BstNodeBase<TKey> node, Func<TKey, bool> predicate)
		{
			if (node == null) return null;
			if (predicate(node.Key)) return SearchMinNode(node.Left, predicate) ?? node;
			else return SearchMinNode(node.Right, predicate);
		}

		public static BstNodeBase<TKey> SearchMaxNode<TKey>(this BstNodeBase<TKey> node, Func<TKey, bool> predicate)
		{
			if (node == null) return null;
			if (predicate(node.Key)) return SearchMaxNode(node.Right, predicate) ?? node;
			else return SearchMaxNode(node.Left, predicate);
		}

		public static BstNodeBase<KeyValuePair<TKey, TValue>> SearchMinNode<TKey, TValue>(this BstNodeBase<KeyValuePair<TKey, TValue>> node, Func<TKey, bool> predicate)
		{
			if (node == null) return null;
			if (predicate(node.Key.Key)) return SearchMinNode(node.Left, predicate) ?? node;
			else return SearchMinNode(node.Right, predicate);
		}

		public static BstNodeBase<KeyValuePair<TKey, TValue>> SearchMaxNode<TKey, TValue>(this BstNodeBase<KeyValuePair<TKey, TValue>> node, Func<TKey, bool> predicate)
		{
			if (node == null) return null;
			if (predicate(node.Key.Key)) return SearchMaxNode(node.Right, predicate) ?? node;
			else return SearchMaxNode(node.Left, predicate);
		}

		public static BstNodeBase<TKey> SearchPreviousNode<TKey>(this BstNodeBase<TKey> node)
		{
			if (node == null) return null;
			return SearchMaxNode(node.Left) ?? SearchPreviousAncestor(node);
		}

		public static BstNodeBase<TKey> SearchNextNode<TKey>(this BstNodeBase<TKey> node)
		{
			if (node == null) return null;
			return SearchMinNode(node.Right) ?? SearchNextAncestor(node);
		}

		static BstNodeBase<TKey> SearchPreviousAncestor<TKey>(this BstNodeBase<TKey> node)
		{
			if (node?.Parent == null) return null;
			if (node.Parent.Right == node) return node.Parent;
			else return SearchPreviousAncestor(node.Parent);
		}

		static BstNodeBase<TKey> SearchNextAncestor<TKey>(this BstNodeBase<TKey> node)
		{
			if (node?.Parent == null) return null;
			if (node.Parent.Left == node) return node.Parent;
			else return SearchNextAncestor(node.Parent);
		}

		public static IEnumerable<TKey> GetKeys<TKey>(this BstNodeBase<TKey> node)
		{
			for (var n = SearchMinNode(node); n != null; n = SearchNextNode(n))
			{
				yield return n.Key;
			}
		}

		public static IEnumerable<TKey> GetKeys<TKey>(this BstNodeBase<TKey> node, Func<TKey, bool> predicateForMin, Func<TKey, bool> predicateForMax)
		{
			for (var n = SearchMinNode(node, predicateForMin); n != null && predicateForMax(n.Key); n = SearchNextNode(n))
			{
				yield return n.Key;
			}
		}

		public static IEnumerable<KeyValuePair<TKey, TValue>> GetKeys<TKey, TValue>(this BstNodeBase<KeyValuePair<TKey, TValue>> node, Func<TKey, bool> predicateForMin, Func<TKey, bool> predicateForMax)
		{
			for (var n = SearchMinNode(node, predicateForMin); n != null && predicateForMax(n.Key.Key); n = SearchNextNode(n))
			{
				yield return n.Key;
			}
		}

		public static BstNodeBase<TKey> RotateToRight<TKey>(this BstNodeBase<TKey> node)
		{
			if (node == null) throw new ArgumentNullException();
			var p = node.Left;
			node.Left = p.Right;
			p.Right = node;
			return p;
		}

		public static BstNodeBase<TKey> RotateToLeft<TKey>(this BstNodeBase<TKey> node)
		{
			if (node == null) throw new ArgumentNullException();
			var p = node.Right;
			node.Right = p.Left;
			p.Left = node;
			return p;
		}

		public static void WalkByPreorder<TKey>(this BstNodeBase<TKey> node, Action<TKey> action)
		{
			if (node == null) return;
			action(node.Key);
			WalkByPreorder(node.Left, action);
			WalkByPreorder(node.Right, action);
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public abstract class BstBase<T, TNode> : IEnumerable<T> where TNode : BstNodeBase<T>
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

		protected Comparison<T> compare;
		public int Count { get; protected set; }

		protected BstBase(Comparison<T> comparison = null)
		{
			compare = comparison ?? Comparer<T>.Default.Compare;
		}

		public bool Contains(T item)
		{
			return Root.SearchNode(item, compare) != null;
		}

		public T GetMin()
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return Root.SearchMinNode().Key;
		}

		public T GetMax()
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return Root.SearchMaxNode().Key;
		}

		public T GetMin(Func<T, bool> predicate)
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return Root.SearchMinNode(predicate).Key;
		}

		public T GetMax(Func<T, bool> predicate)
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return Root.SearchMaxNode(predicate).Key;
		}

		public T GetPrevious(T item, T defaultValue = default(T))
		{
			var node = Root.SearchNode(item, compare);
			if (node == null) throw new InvalidOperationException("The item is not found.");
			node = node.SearchPreviousNode();
			if (node == null) return defaultValue;
			return node.Key;
		}

		public T GetNext(T item, T defaultValue = default(T))
		{
			var node = Root.SearchNode(item, compare);
			if (node == null) throw new InvalidOperationException("The item is not found.");
			node = node.SearchNextNode();
			if (node == null) return defaultValue;
			return node.Key;
		}

		public IEnumerable<T> GetItems() => Root.GetKeys();
		public IEnumerable<T> GetItems(Func<T, bool> predicateForMin, Func<T, bool> predicateForMax) => Root.GetKeys(predicateForMin, predicateForMax);

		public IEnumerator<T> GetEnumerator() => GetItems().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();

		public abstract bool Add(T item);
		public abstract bool Remove(T item);
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public abstract class BstMapBase<TKey, TValue, TNode> : IEnumerable<KeyValuePair<TKey, TValue>> where TNode : BstNodeBase<KeyValuePair<TKey, TValue>>
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

		protected BstMapBase(Comparison<TKey> comparison = null)
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
			return Root.SearchMinNode().Key;
		}

		public KeyValuePair<TKey, TValue> GetMax()
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return Root.SearchMaxNode().Key;
		}

		public KeyValuePair<TKey, TValue> GetMin(Func<TKey, bool> predicate)
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return Root.SearchMinNode(predicate).Key;
		}

		public KeyValuePair<TKey, TValue> GetMax(Func<TKey, bool> predicate)
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return Root.SearchMaxNode(predicate).Key;
		}

		public KeyValuePair<TKey, TValue> GetPrevious(TKey key)
		{
			var node = Root.SearchNode(key, compare);
			if (node == null) throw new InvalidOperationException("The item is not found.");
			node = node.SearchPreviousNode();
			if (node == null) throw new InvalidOperationException("The target item is not found.");
			return node.Key;
		}

		public KeyValuePair<TKey, TValue> GetNext(TKey key)
		{
			var node = Root.SearchNode(key, compare);
			if (node == null) throw new InvalidOperationException("The item is not found.");
			node = node.SearchNextNode();
			if (node == null) throw new InvalidOperationException("The target item is not found.");
			return node.Key;
		}

		public IEnumerable<KeyValuePair<TKey, TValue>> GetItems() => Root.GetKeys();
		public IEnumerable<KeyValuePair<TKey, TValue>> GetItems(Func<TKey, bool> predicateForMin, Func<TKey, bool> predicateForMax) => Root.GetKeys(predicateForMin, predicateForMax);

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => GetItems().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();

		public abstract bool Add(TKey key, TValue value);
		public abstract bool Remove(TKey key);

		public TValue this[TKey key]
		{
			get
			{
				var node = Root.SearchNode(key, compare);
				if (node == null) throw new InvalidOperationException("The item is not found.");
				return node.Key.Value;
			}
			set
			{
				var node = Root.SearchNode(key, compare);
				if (node == null) Add(key, value);
				else node.Key = new KeyValuePair<TKey, TValue>(key, value);
			}
		}
	}

	public static class ComparisonHelper
	{
		public static Comparison<T> Create<T>(bool descending = false)
		{
			var c = Comparer<T>.Default;
			if (descending) return (x, y) => c.Compare(y, x);
			else return c.Compare;
		}

		public static Comparison<T> Create<T, TKey>(Func<T, TKey> keySelector, bool descending = false)
		{
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
			var c = Comparer<TKey>.Default;
			if (descending) return (x, y) => c.Compare(keySelector(y), keySelector(x));
			else return (x, y) => c.Compare(keySelector(x), keySelector(y));
		}
	}
}
