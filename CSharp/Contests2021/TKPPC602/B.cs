using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees.Bsts;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s0 = Console.ReadLine().Select(c => c == 'B').ToArray();
		var t = Console.ReadLine().Select(c => c == 'B').ToArray();

		var s = new IndexedList<bool>();
		foreach (var v in s0)
		{
			s.Add(v);
		}

		var r = 0L;
		// 先行インデックス
		var q = 0;

		for (int i = 0; i < n; i++)
		{
			var v = s[i];
			if (v == t[i]) continue;

			if (i == n - 1) return -1;

			if (v == s[i + 1])
			{
				s[i] = !v;
				s[i + 1] = !v;
				r++;
				q = i + 1;
			}
			else
			{
				q = Math.Max(q, i + 1);
				while (q + 1 < n && s[q] != s[q + 1])
				{
					q++;
				}
				if (q == n - 1) return -1;

				r += q - i + 1;
				s.RemoveAt(q);
				s.RemoveAt(q);
				s.Insert(i, !v);
				s.Insert(i, !v);
			}
		}

		if (!s.SequenceEqual(t)) throw new InvalidOperationException();

		return r;
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class IndexedList<T> : IEnumerable<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Item}\}")]
		public class Node : IEnumerable<Node>
		{
			public T Item { get; set; }
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

			public Node SearchNode(int index)
			{
				var d = index - LeftCount;
				if (d == 0) return this;
				if (d < 0) return Left?.SearchNode(index);
				else return Right?.SearchNode(d - 1);
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

		public IEnumerator<T> GetEnumerator() => GetItems().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetItems().GetEnumerator();
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

		public Node Prepend(T item) => Insert(0, item);
		public Node Add(T item) => Insert(Count, item);
		public Node Insert(int index, T item)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
			if (index > Count) throw new ArgumentOutOfRangeException(nameof(index));

			var newNode = new Node { Item = item };
			SetRoot(Insert(Root, index, newNode));
			return newNode;
		}

		public T RemoveAt(int index)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
			if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

			var node = Root.SearchNode(index);
			var item = node.Item;
			RemoveTarget(node);
			return item;
		}

		#region Private Methods

		Node Insert(Node node, int index, Node newNode)
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
				t.Item = t2.Item;
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
