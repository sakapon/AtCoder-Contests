using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees.Bsts
{
	// Test: https://atcoder.jp/contests/past202104-open/tasks/past202104_e
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_ar
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bi
	[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
	public class AvlListNode<T> : IEnumerable<AvlListNode<T>>
	{
		public T Item { get; set; }
		public AvlListNode<T> Parent { get; internal set; }
		public AvlListNode<T> Left { get; private set; }
		public AvlListNode<T> Right { get; private set; }

		internal void SetLeft(AvlListNode<T> child)
		{
			Left = child;
			if (child != null) child.Parent = this;
		}

		internal void SetRight(AvlListNode<T> child)
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

		public AvlListNode<T> SearchFirstNode()
		{
			return Left?.SearchFirstNode() ?? this;
		}

		public AvlListNode<T> SearchLastNode()
		{
			return Right?.SearchLastNode() ?? this;
		}

		public AvlListNode<T> SearchPreviousNode()
		{
			return Left?.SearchLastNode() ?? SearchPreviousAncestor();
		}

		public AvlListNode<T> SearchNextNode()
		{
			return Right?.SearchFirstNode() ?? SearchNextAncestor();
		}

		AvlListNode<T> SearchPreviousAncestor()
		{
			if (Parent == null) return null;
			if (Parent.Right == this) return Parent;
			return Parent.SearchPreviousAncestor();
		}

		AvlListNode<T> SearchNextAncestor()
		{
			if (Parent == null) return null;
			if (Parent.Left == this) return Parent;
			return Parent.SearchNextAncestor();
		}

		public AvlListNode<T> SearchNode(int index)
		{
			var d = index - LeftCount;
			if (d == 0) return this;
			if (d < 0) return Left?.SearchNode(index);
			else return Right?.SearchNode(d - 1);
		}

		public IEnumerable<AvlListNode<T>> SearchNodes() => SearchNodes(0, Count);
		public IEnumerable<AvlListNode<T>> SearchNodes(int l, int r)
		{
			if (l < 0) throw new ArgumentOutOfRangeException(nameof(l));
			if (r > Count) throw new ArgumentOutOfRangeException(nameof(r));
			if (l > r) throw new ArgumentOutOfRangeException(nameof(r), "l <= r must be satisfied.");

			for (var n = SearchNode(l); l < r; n = n.SearchNextNode(), ++l)
			{
				yield return n;
			}
		}

		public IEnumerator<AvlListNode<T>> GetEnumerator() => SearchNodes().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => SearchNodes().GetEnumerator();
	}

	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class AvlList<T> : IEnumerable<T>
	{
		public AvlListNode<T> Root { get; private set; }

		void SetRoot(AvlListNode<T> root)
		{
			Root = root;
			if (root != null) root.Parent = null;
		}

		public int Count { get; private set; }

		public void Clear()
		{
			SetRoot(null);
			Count = 0;
		}

		public T GetFirst()
		{
			if (Root == null) throw new InvalidOperationException("The container is empty.");
			return Root.SearchFirstNode().Item;
		}

		public T GetLast()
		{
			if (Root == null) throw new InvalidOperationException("The container is empty.");
			return Root.SearchLastNode().Item;
		}

		public IEnumerable<T> GetItems()
		{
			for (var n = Root?.SearchFirstNode(); n != null; n = n.SearchNextNode())
			{
				yield return n.Item;
			}
		}

		public IEnumerable<T> GetItems(int l, int r)
		{
			if (l < 0) throw new ArgumentOutOfRangeException(nameof(l));
			if (r > Count) throw new ArgumentOutOfRangeException(nameof(r));
			if (l > r) throw new ArgumentOutOfRangeException(nameof(r), "l <= r must be satisfied.");

			for (var n = Root?.SearchNode(l); l < r; n = n.SearchNextNode(), ++l)
			{
				yield return n.Item;
			}
		}

		public IEnumerator<T> GetEnumerator() => GetItems().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();

		public T this[int index]
		{
			get
			{
				if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
				if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

				var node = Root.SearchNode(index);
				return node.Item;
			}
			set
			{
				if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
				if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

				var node = Root.SearchNode(index);
				node.Item = value;
			}
		}

		public AvlListNode<T> Prepend(T item) => Insert(0, item);
		public AvlListNode<T> Add(T item) => Insert(Count, item);
		public AvlListNode<T> Insert(int index, T item)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
			if (index > Count) throw new ArgumentOutOfRangeException(nameof(index));

			var newNode = new AvlListNode<T> { Item = item };
			SetRoot(Insert(Root, index, newNode));
			return newNode;
		}

		AvlListNode<T> Insert(AvlListNode<T> node, int index, AvlListNode<T> newNode)
		{
			if (node == null)
			{
				++Count;
				return newNode;
			}

			var d = index - node.LeftCount;
			if (d <= 0)
				node.SetLeft(Insert(node.Left, index, newNode));
			else
				node.SetRight(Insert(node.Right, d - 1, newNode));

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

		public T RemoveAt(int index)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
			if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

			var node = Root.SearchNode(index);
			var item = node.Item;
			RemoveTarget(node);
			--Count;
			return item;
		}

		// Suppose t != null.
		void RemoveTarget(AvlListNode<T> t)
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
				t.Item = t2.Item;
				RemoveTarget(t2);
			}
		}

		// Suppose t != null.
		static AvlListNode<T> RotateToRight(AvlListNode<T> t)
		{
			var p = t.Left;
			t.SetLeft(p.Right);
			p.SetRight(t);
			return p;
		}

		// Suppose t != null.
		static AvlListNode<T> RotateToLeft(AvlListNode<T> t)
		{
			var p = t.Right;
			t.SetRight(p.Left);
			p.SetLeft(t);
			return p;
		}
	}
}
