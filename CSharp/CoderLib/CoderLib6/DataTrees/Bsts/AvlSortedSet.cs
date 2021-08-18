﻿using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees.Bsts
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Key}\}")]
	public class AvlSortedSetNode<TKey> : IEnumerable<AvlSortedSetNode<TKey>>
	{
		public TKey Key { get; set; }
		public AvlSortedSetNode<TKey> Parent { get; internal set; }
		public AvlSortedSetNode<TKey> Left { get; private set; }
		public AvlSortedSetNode<TKey> Right { get; private set; }

		internal void SetLeft(AvlSortedSetNode<TKey> child)
		{
			Left = child;
			if (child != null) child.Parent = this;
		}

		internal void SetRight(AvlSortedSetNode<TKey> child)
		{
			Right = child;
			if (child != null) child.Parent = this;
		}

		public int Height { get; private set; } = 1;
		public int LeftHeight => Left?.Height ?? 0;
		public int RightHeight => Right?.Height ?? 0;

		internal void UpdateHeight(bool recursive = false)
		{
			Height = Math.Max(LeftHeight, RightHeight) + 1;
			if (recursive) Parent?.UpdateHeight(true);
		}

		public AvlSortedSetNode<TKey> SearchFirstNode()
		{
			return Left?.SearchFirstNode() ?? this;
		}

		public AvlSortedSetNode<TKey> SearchLastNode()
		{
			return Right?.SearchLastNode() ?? this;
		}

		public AvlSortedSetNode<TKey> SearchFirstNode(Func<TKey, bool> predicate)
		{
			if (predicate(Key)) return Left?.SearchFirstNode(predicate) ?? this;
			else return Right?.SearchFirstNode(predicate);
		}

		public AvlSortedSetNode<TKey> SearchLastNode(Func<TKey, bool> predicate)
		{
			if (predicate(Key)) return Right?.SearchLastNode(predicate) ?? this;
			else return Left?.SearchLastNode(predicate);
		}

		public AvlSortedSetNode<TKey> SearchPreviousNode()
		{
			return Left?.SearchLastNode() ?? SearchPreviousAncestor();
		}

		public AvlSortedSetNode<TKey> SearchNextNode()
		{
			return Right?.SearchFirstNode() ?? SearchNextAncestor();
		}

		AvlSortedSetNode<TKey> SearchPreviousAncestor()
		{
			if (Parent == null) return null;
			if (Parent.Right == this) return Parent;
			return Parent.SearchPreviousAncestor();
		}

		AvlSortedSetNode<TKey> SearchNextAncestor()
		{
			if (Parent == null) return null;
			if (Parent.Left == this) return Parent;
			return Parent.SearchNextAncestor();
		}

		public AvlSortedSetNode<TKey> SearchNode(TKey key, IComparer<TKey> comparer)
		{
			var d = comparer.Compare(key, Key);
			if (d == 0) return this;
			if (d < 0) return Left?.SearchNode(key, comparer);
			else return Right?.SearchNode(key, comparer);
		}

		public IEnumerable<AvlSortedSetNode<TKey>> SearchNodes()
		{
			var end = SearchNextAncestor();
			for (var n = SearchFirstNode(); n != end; n = n.SearchNextNode())
			{
				yield return n;
			}
		}

		public IEnumerable<AvlSortedSetNode<TKey>> SearchNodes(Func<TKey, bool> startPredicate, Func<TKey, bool> endPredicate)
		{
			for (var n = SearchFirstNode(startPredicate); n != null && endPredicate(n.Key); n = n.SearchNextNode())
			{
				yield return n;
			}
		}

		public IEnumerator<AvlSortedSetNode<TKey>> GetEnumerator() => SearchNodes().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => SearchNodes().GetEnumerator();
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class AvlSortedSet<T> : IEnumerable<T>
	{
		public AvlSortedSetNode<T> Root { get; private set; }

		void SetRoot(AvlSortedSetNode<T> root)
		{
			Root = root;
			if (root != null) root.Parent = null;
		}

		public int Count { get; private set; }
		public IComparer<T> Comparer { get; }

		public AvlSortedSet(IComparer<T> comparer = null)
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

		public bool Contains(T item)
		{
			return Root?.SearchNode(item, Comparer) != null;
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

		public IEnumerator<T> GetEnumerator() => GetItems().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();

		public bool Add(T item)
		{
			var c = Count;
			SetRoot(Insert(Root, item));
			return Count != c;
		}

		AvlSortedSetNode<T> Insert(AvlSortedSetNode<T> node, T item)
		{
			if (node == null)
			{
				++Count;
				return new AvlSortedSetNode<T> { Key = item };
			}

			var d = Comparer.Compare(item, node.Key);
			if (d == 0) return node;

			if (d < 0)
				node.SetLeft(Insert(node.Left, item));
			else
				node.SetRight(Insert(node.Right, item));

			var lrh = node.LeftHeight - node.RightHeight;
			if (lrh > 2 || lrh == 2 && node.Left.LeftHeight >= node.Left.RightHeight)
			{
				node = RotateToRight(node);
				node.Right.UpdateHeight();
			}
			else if (lrh < -2 || lrh == -2 && node.Right.LeftHeight <= node.Right.RightHeight)
			{
				node = RotateToLeft(node);
				node.Left.UpdateHeight();
			}

			node.UpdateHeight();
			return node;
		}

		public bool Remove(T item)
		{
			var node = Root.SearchNode(item, Comparer);
			if (node == null) return false;

			RemoveTarget(node);
			--Count;
			return true;
		}

		// Suppose t != null.
		void RemoveTarget(AvlSortedSetNode<T> t)
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

				t.Parent?.UpdateHeight(true);
			}
			else
			{
				var t2 = t.SearchNextNode();
				t.Key = t2.Key;
				RemoveTarget(t2);
			}
		}

		// Suppose t != null.
		static AvlSortedSetNode<T> RotateToRight(AvlSortedSetNode<T> t)
		{
			var p = t.Left;
			t.SetLeft(p.Right);
			p.SetRight(t);
			return p;
		}

		// Suppose t != null.
		static AvlSortedSetNode<T> RotateToLeft(AvlSortedSetNode<T> t)
		{
			var p = t.Right;
			t.SetRight(p.Left);
			p.SetLeft(t);
			return p;
		}
	}
}