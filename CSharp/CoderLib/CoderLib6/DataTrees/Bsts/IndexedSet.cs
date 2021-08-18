using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees.Bsts
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Key}\}")]
	public class IndexedSetNode<TKey> : IEnumerable<IndexedSetNode<TKey>>
	{
		public TKey Key { get; set; }
		public IndexedSetNode<TKey> Parent { get; internal set; }
		public IndexedSetNode<TKey> Left { get; private set; }
		public IndexedSetNode<TKey> Right { get; private set; }

		internal void SetLeft(IndexedSetNode<TKey> child)
		{
			Left = child;
			if (child != null) child.Parent = this;
		}

		internal void SetRight(IndexedSetNode<TKey> child)
		{
			Right = child;
			if (child != null) child.Parent = this;
		}

		public int Count { get; private set; } = 1;
		public int LeftCount => Left?.Count ?? 0;
		public int RightCount => Right?.Count ?? 0;

		internal void UpdateCount(bool recursive = false)
		{
			Count = LeftCount + RightCount + 1;
			if (recursive) Parent?.UpdateCount(true);
		}

		public IndexedSetNode<TKey> SearchFirstNode()
		{
			return Left?.SearchFirstNode() ?? this;
		}

		public IndexedSetNode<TKey> SearchLastNode()
		{
			return Right?.SearchLastNode() ?? this;
		}

		public IndexedSetNode<TKey> SearchFirstNode(Func<TKey, bool> predicate)
		{
			if (predicate(Key)) return Left?.SearchFirstNode(predicate) ?? this;
			else return Right?.SearchFirstNode(predicate);
		}

		public IndexedSetNode<TKey> SearchLastNode(Func<TKey, bool> predicate)
		{
			if (predicate(Key)) return Right?.SearchLastNode(predicate) ?? this;
			else return Left?.SearchLastNode(predicate);
		}

		public IndexedSetNode<TKey> SearchPreviousNode()
		{
			return Left?.SearchLastNode() ?? SearchPreviousAncestor();
		}

		public IndexedSetNode<TKey> SearchNextNode()
		{
			return Right?.SearchFirstNode() ?? SearchNextAncestor();
		}

		IndexedSetNode<TKey> SearchPreviousAncestor()
		{
			if (Parent == null) return null;
			if (Parent.Right == this) return Parent;
			return Parent.SearchPreviousAncestor();
		}

		IndexedSetNode<TKey> SearchNextAncestor()
		{
			if (Parent == null) return null;
			if (Parent.Left == this) return Parent;
			return Parent.SearchNextAncestor();
		}

		// not found: Count
		public int SearchFirstIndex(Func<TKey, bool> predicate)
		{
			if (predicate(Key))
				return Left?.SearchFirstIndex(predicate) ?? 0;
			else
				return LeftCount + 1 + (Right?.SearchFirstIndex(predicate) ?? 0);
		}

		// not found: -1
		public int SearchLastIndex(Func<TKey, bool> predicate)
		{
			if (predicate(Key))
				return LeftCount + 1 + (Right?.SearchLastIndex(predicate) ?? -1);
			else
				return Left?.SearchLastIndex(predicate) ?? -1;
		}

		// not found: -1
		public int SearchIndex(TKey key, IComparer<TKey> comparer)
		{
			var d = comparer.Compare(key, Key);
			if (d == 0) return LeftCount;
			if (d < 0)
			{
				return Left?.SearchIndex(key, comparer) ?? -1;
			}
			else
			{
				var i = Right?.SearchIndex(key, comparer) ?? -1;
				if (i == -1) return -1;
				return LeftCount + 1 + i;
			}
		}

		public IndexedSetNode<TKey> SearchNode(int index)
		{
			var d = index - LeftCount;
			if (d == 0) return this;
			if (d < 0) return Left?.SearchNode(index);
			else return Right?.SearchNode(d - 1);
		}

		public IndexedSetNode<TKey> SearchNode(TKey key, IComparer<TKey> comparer)
		{
			var d = comparer.Compare(key, Key);
			if (d == 0) return this;
			if (d < 0) return Left?.SearchNode(key, comparer);
			else return Right?.SearchNode(key, comparer);
		}

		public IEnumerable<IndexedSetNode<TKey>> SearchNodes()
		{
			var end = SearchNextAncestor();
			for (var n = SearchFirstNode(); n != end; n = n.SearchNextNode())
			{
				yield return n;
			}
		}

		public IEnumerator<IndexedSetNode<TKey>> GetEnumerator() => SearchNodes().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => SearchNodes().GetEnumerator();
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class IndexedSet<T> : IEnumerable<T>
	{
		public IndexedSetNode<T> Root { get; private set; }

		void SetRoot(IndexedSetNode<T> root)
		{
			Root = root;
			if (root != null) root.Parent = null;
		}

		public int Count { get; private set; }
		public IComparer<T> Comparer { get; }

		public IndexedSet(IComparer<T> comparer = null)
		{
			Comparer = comparer ?? Comparer<T>.Default;
		}

		public void Clear()
		{
			SetRoot(null);
			Count = 0;
		}

		public T GetFirst()
		{
			if (Root == null) throw new InvalidOperationException("The container is empty.");
			return Root.SearchFirstNode().Key;
		}

		public T GetLast()
		{
			if (Root == null) throw new InvalidOperationException("The container is empty.");
			return Root.SearchLastNode().Key;
		}

		public T GetFirst(Func<T, bool> predicate, T defaultValue = default(T))
		{
			var node = Root?.SearchFirstNode(predicate);
			if (node == null) return defaultValue;
			return node.Key;
		}

		public T GetLast(Func<T, bool> predicate, T defaultValue = default(T))
		{
			var node = Root?.SearchLastNode(predicate);
			if (node == null) return defaultValue;
			return node.Key;
		}

		public int GetFirstIndex(Func<T, bool> predicate)
		{
			return Root?.SearchFirstIndex(predicate) ?? 0;
		}

		public int GetLastIndex(Func<T, bool> predicate)
		{
			return Root?.SearchLastIndex(predicate) ?? -1;
		}

		public bool Contains(T item)
		{
			return Root?.SearchNode(item, Comparer) != null;
		}

		public T GetItem(int index)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
			if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

			return Root.SearchNode(index).Key;
		}

		public int GetIndex(T item)
		{
			return Root?.SearchIndex(item, Comparer) ?? -1;
		}

		public IEnumerable<T> GetItems()
		{
			for (var n = Root?.SearchFirstNode(); n != null; n = n.SearchNextNode())
			{
				yield return n.Key;
			}
		}

		public IEnumerable<T> GetItems(Func<T, bool> startPredicate, Func<T, bool> endPredicate)
		{
			for (var n = Root?.SearchFirstNode(startPredicate); n != null && endPredicate(n.Key); n = n.SearchNextNode())
			{
				yield return n.Key;
			}
		}

		public IEnumerable<T> GetItems(int l, int r)
		{
			if (l < 0) throw new ArgumentOutOfRangeException(nameof(l));
			if (r > Count) throw new ArgumentOutOfRangeException(nameof(r));
			if (l > r) throw new ArgumentOutOfRangeException(nameof(r), "l <= r must be satisfied.");

			for (var n = Root?.SearchNode(l); l < r; n = n.SearchNextNode(), ++l)
			{
				yield return n.Key;
			}
		}

		public int GetCount(Func<T, bool> startPredicate, Func<T, bool> endPredicate)
		{
			var si = Root?.SearchFirstIndex(startPredicate) ?? 0;
			var ei = Root?.SearchLastIndex(endPredicate) ?? -1;
			return ei - si + 1;
		}

		public IEnumerator<T> GetEnumerator() => GetItems().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();

		public bool Add(T item)
		{
			var c = Count;
			SetRoot(Insert(Root, item));
			return Count != c;
		}

		IndexedSetNode<T> Insert(IndexedSetNode<T> node, T item)
		{
			if (node == null)
			{
				++Count;
				return new IndexedSetNode<T> { Key = item };
			}

			var d = Comparer.Compare(item, node.Key);
			if (d == 0) return node;

			if (d < 0)
				node.SetLeft(Insert(node.Left, item));
			else
				node.SetRight(Insert(node.Right, item));

			var lc = node.LeftCount + 1;
			var rc = node.RightCount + 1;
			if (lc > 2 * rc)
			{
				node = RotateToRight(node);
				node.Right.UpdateCount();
			}
			else if (rc > 2 * lc)
			{
				node = RotateToLeft(node);
				node.Left.UpdateCount();
			}

			node.UpdateCount();
			return node;
		}

		public bool Remove(T item)
		{
			var node = Root?.SearchNode(item, Comparer);
			if (node == null) return false;

			RemoveTarget(node);
			--Count;
			return true;
		}

		public T RemoveAt(int index)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
			if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

			var node = Root.SearchNode(index);
			var item = node.Key;
			RemoveTarget(node);
			--Count;
			return item;
		}

		// Suppose t != null.
		void RemoveTarget(IndexedSetNode<T> t)
		{
			if (t.Left == null || t.Right == null)
			{
				var c = t.Left ?? t.Right;

				if (t.Parent == null)
					SetRoot(c);
				else if (t.Parent.Left == t)
					t.Parent.SetLeft(c);
				else
					t.Parent.SetRight(c);

				t.Parent?.UpdateCount(true);
			}
			else
			{
				var t2 = t.SearchNextNode();
				t.Key = t2.Key;
				RemoveTarget(t2);
			}
		}

		// Suppose t != null.
		static IndexedSetNode<T> RotateToRight(IndexedSetNode<T> t)
		{
			var p = t.Left;
			t.SetLeft(p.Right);
			p.SetRight(t);
			return p;
		}

		// Suppose t != null.
		static IndexedSetNode<T> RotateToLeft(IndexedSetNode<T> t)
		{
			var p = t.Right;
			t.SetRight(p.Left);
			p.SetLeft(t);
			return p;
		}
	}
}
