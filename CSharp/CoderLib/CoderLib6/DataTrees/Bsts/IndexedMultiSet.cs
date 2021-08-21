using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/7/ITP2_7_D
// Test: https://atcoder.jp/contests/past202012-open/tasks/past202012_d
// Test: https://atcoder.jp/contests/typical90/tasks/typical90_f
// Test: https://atcoder.jp/contests/typical90/tasks/typical90_g
// Test: https://atcoder.jp/contests/abc124/tasks/abc124_d
// Test: https://atcoder.jp/contests/abc143/tasks/abc143_d
namespace CoderLib6.DataTrees.Bsts
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class IndexedMultiSet<T> : IEnumerable<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Key}\}")]
		public class Node : IEnumerable<Node>
		{
			public T Key { get; set; }
			public Node Parent { get; internal set; }
			public Node Left { get; private set; }
			public Node Right { get; private set; }

			internal void SetLeft(Node child)
			{
				Left = child;
				if (child != null) child.Parent = this;
			}

			internal void SetRight(Node child)
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

			public Node SearchFirstNode()
			{
				return Left?.SearchFirstNode() ?? this;
			}

			public Node SearchLastNode()
			{
				return Right?.SearchLastNode() ?? this;
			}

			public Node SearchFirstNode(Func<T, bool> predicate)
			{
				if (predicate(Key)) return Left?.SearchFirstNode(predicate) ?? this;
				else return Right?.SearchFirstNode(predicate);
			}

			public Node SearchLastNode(Func<T, bool> predicate)
			{
				if (predicate(Key)) return Right?.SearchLastNode(predicate) ?? this;
				else return Left?.SearchLastNode(predicate);
			}

			public Node SearchPreviousNode()
			{
				return Left?.SearchLastNode() ?? SearchPreviousAncestor();
			}

			public Node SearchNextNode()
			{
				return Right?.SearchFirstNode() ?? SearchNextAncestor();
			}

			Node SearchPreviousAncestor()
			{
				if (Parent == null) return null;
				if (Parent.Right == this) return Parent;
				return Parent.SearchPreviousAncestor();
			}

			Node SearchNextAncestor()
			{
				if (Parent == null) return null;
				if (Parent.Left == this) return Parent;
				return Parent.SearchNextAncestor();
			}

			// not found: Count
			public int SearchFirstIndex(Func<T, bool> predicate)
			{
				if (predicate(Key))
					return Left?.SearchFirstIndex(predicate) ?? 0;
				else
					return LeftCount + 1 + (Right?.SearchFirstIndex(predicate) ?? 0);
			}

			// not found: -1
			public int SearchLastIndex(Func<T, bool> predicate)
			{
				if (predicate(Key))
					return LeftCount + 1 + (Right?.SearchLastIndex(predicate) ?? -1);
				else
					return Left?.SearchLastIndex(predicate) ?? -1;
			}

			// not found: -1
			public int SearchFirstIndex(T key, IComparer<T> comparer)
			{
				var d = comparer.Compare(key, Key);
				if (d == 0)
				{
					var i = Left?.SearchFirstIndex(key, comparer) ?? -1;
					if (i == -1) return LeftCount;
					return i;
				}
				else if (d < 0)
				{
					return Left?.SearchFirstIndex(key, comparer) ?? -1;
				}
				else
				{
					var i = Right?.SearchFirstIndex(key, comparer) ?? -1;
					if (i == -1) return -1;
					return LeftCount + 1 + i;
				}
			}

			// not found: -1
			public int SearchLastIndex(T key, IComparer<T> comparer)
			{
				var d = comparer.Compare(key, Key);
				if (d == 0)
				{
					var i = Right?.SearchLastIndex(key, comparer) ?? -1;
					if (i == -1) return LeftCount;
					return LeftCount + 1 + i;
				}
				else if (d < 0)
				{
					return Left?.SearchLastIndex(key, comparer) ?? -1;
				}
				else
				{
					var i = Right?.SearchLastIndex(key, comparer) ?? -1;
					if (i == -1) return -1;
					return LeftCount + 1 + i;
				}
			}

			public Node SearchNode(int index)
			{
				var d = index - LeftCount;
				if (d == 0) return this;
				if (d < 0) return Left?.SearchNode(index);
				else return Right?.SearchNode(d - 1);
			}

			public Node SearchFirstNode(T key, IComparer<T> comparer)
			{
				var d = comparer.Compare(key, Key);
				if (d == 0) return Left?.SearchFirstNode(key, comparer) ?? this;
				else if (d < 0) return Left?.SearchFirstNode(key, comparer);
				else return Right?.SearchFirstNode(key, comparer);
			}

			public Node SearchLastNode(T key, IComparer<T> comparer)
			{
				var d = comparer.Compare(key, Key);
				if (d == 0) return Right?.SearchLastNode(key, comparer) ?? this;
				else if (d < 0) return Left?.SearchLastNode(key, comparer);
				else return Right?.SearchLastNode(key, comparer);
			}

			public IEnumerator<Node> GetEnumerator() => SearchNodes().GetEnumerator();
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => SearchNodes().GetEnumerator();
			public IEnumerable<Node> SearchNodes()
			{
				var end = SearchNextAncestor();
				for (var n = SearchFirstNode(); n != end; n = n.SearchNextNode())
				{
					yield return n;
				}
			}
		}

		public Node Root { get; private set; }

		void SetRoot(Node root)
		{
			Root = root;
			if (root != null) root.Parent = null;
		}

		public int Count { get; private set; }
		public IComparer<T> Comparer { get; }

		public IndexedMultiSet(IComparer<T> comparer = null)
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
			return Root?.SearchFirstNode(item, Comparer) != null;
		}

		public T GetItem(int index)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
			if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

			return Root.SearchNode(index).Key;
		}

		public int GetFirstIndex(T item)
		{
			return Root?.SearchFirstIndex(item, Comparer) ?? -1;
		}

		public int GetLastIndex(T item)
		{
			return Root?.SearchLastIndex(item, Comparer) ?? -1;
		}

		public IEnumerator<T> GetEnumerator() => GetItems().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();
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
			var c = ei - si + 1;
			return c < 0 ? 0 : c;
		}

		public int GetCount(T item)
		{
			var si = Root?.SearchFirstIndex(x => Comparer.Compare(x, item) >= 0) ?? 0;
			var ei = Root?.SearchLastIndex(x => Comparer.Compare(x, item) <= 0) ?? -1;
			return ei - si + 1;
		}

		public Node Add(T item)
		{
			var newNode = new Node { Key = item };
			SetRoot(Insert(Root, newNode));
			return newNode;
		}

		public bool RemoveOne(T item)
		{
			var node = Root?.SearchLastNode(item, Comparer);
			if (node == null) return false;

			RemoveTarget(node);
			return true;
		}

		public T RemoveAt(int index)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
			if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

			var node = Root.SearchNode(index);
			var item = node.Key;
			RemoveTarget(node);
			return item;
		}

		#region Private Methods

		Node Insert(Node node, Node newNode)
		{
			if (node == null)
			{
				++Count;
				return newNode;
			}

			var d = Comparer.Compare(newNode.Key, node.Key);
			if (d < 0)
				node.SetLeft(Insert(node.Left, newNode));
			else
				node.SetRight(Insert(node.Right, newNode));

			node = Balance(node);
			node.UpdateCount();
			return node;
		}

		// Suppose t != null.
		void RemoveTarget(Node t)
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
				--Count;
			}
			else
			{
				var t2 = t.SearchNextNode();
				t.Key = t2.Key;
				RemoveTarget(t2);
			}
		}

		// Suppose t != null.
		static Node Balance(Node t)
		{
			var lc = t.LeftCount + 1;
			var rc = t.RightCount + 1;
			if (lc > 2 * rc)
			{
				t = RotateToRight(t);
				t.Right.UpdateCount();
			}
			else if (rc > 2 * lc)
			{
				t = RotateToLeft(t);
				t.Left.UpdateCount();
			}
			return t;
		}

		// Suppose t != null.
		static Node RotateToRight(Node t)
		{
			var p = t.Left;
			t.SetLeft(p.Right);
			p.SetRight(t);
			return p;
		}

		// Suppose t != null.
		static Node RotateToLeft(Node t)
		{
			var p = t.Right;
			t.SetRight(p.Left);
			p.SetLeft(t);
			return p;
		}

		#endregion
	}
}
