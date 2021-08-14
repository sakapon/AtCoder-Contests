using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
	public class BstList<T> : IEnumerable<T>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Value}\}")]
		class Node
		{
			public T Value;
			public Node Parent;

			Node _left;
			public Node Left
			{
				get { return _left; }
				set
				{
					_left = value;
					if (value != null) value.Parent = this;
				}
			}

			Node _right;
			public Node Right
			{
				get { return _right; }
				set
				{
					_right = value;
					if (value != null) value.Parent = this;
				}
			}

			public int Count = 1;
			public int LeftCount => Left?.Count ?? 0;
			public int RightCount => Right?.Count ?? 0;

			public void UpdateCount(bool recursive = false)
			{
				Count = LeftCount + RightCount + 1;
				if (recursive) Parent?.UpdateCount(true);
			}
		}

		Node _root;
		Node Root
		{
			get { return _root; }
			set
			{
				_root = value;
				if (value != null) value.Parent = null;
			}
		}

		public int Count { get; private set; }

		static Node SearchNode(Node node, int index)
		{
			if (node == null) return null;
			var d = index - node.LeftCount;
			if (d == 0) return node;
			if (d < 0) return SearchNode(node.Left, index);
			else return SearchNode(node.Right, d - 1);
		}

		static Node SearchFirstNode(Node node)
		{
			if (node == null) return null;
			return SearchFirstNode(node.Left) ?? node;
		}

		static Node SearchLastNode(Node node)
		{
			if (node == null) return null;
			return SearchLastNode(node.Right) ?? node;
		}

		static Node SearchPreviousNode(Node node)
		{
			if (node == null) return null;
			return SearchLastNode(node.Left) ?? SearchPreviousAncestor(node);
		}

		static Node SearchNextNode(Node node)
		{
			if (node == null) return null;
			return SearchFirstNode(node.Right) ?? SearchNextAncestor(node);
		}

		static Node SearchPreviousAncestor(Node node)
		{
			if (node?.Parent == null) return null;
			if (node.Parent.Right == node) return node.Parent;
			else return SearchPreviousAncestor(node.Parent);
		}

		static Node SearchNextAncestor(Node node)
		{
			if (node?.Parent == null) return null;
			if (node.Parent.Left == node) return node.Parent;
			else return SearchNextAncestor(node.Parent);
		}

		// Suppose t != null.
		static Node RotateToRight(Node t)
		{
			var p = t.Left;
			t.Left = p.Right;
			p.Right = t;
			return p;
		}

		// Suppose t != null.
		static Node RotateToLeft(Node t)
		{
			var p = t.Right;
			t.Right = p.Left;
			p.Left = t;
			return p;
		}

		public T GetFirst()
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return SearchFirstNode(Root).Value;
		}

		public T GetLast()
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return SearchLastNode(Root).Value;
		}

		public IEnumerable<T> GetValues()
		{
			for (var n = SearchFirstNode(Root); n != null; n = SearchNextNode(n))
			{
				yield return n.Value;
			}
		}

		public IEnumerable<T> GetValues(int l, int r)
		{
			if (l < 0) throw new ArgumentOutOfRangeException(nameof(l));
			if (r > Count) throw new ArgumentOutOfRangeException(nameof(r));
			if (l > r) throw new ArgumentOutOfRangeException();

			for (var n = SearchNode(Root, l); l < r; n = SearchNextNode(n), ++l)
			{
				yield return n.Value;
			}
		}

		public IEnumerator<T> GetEnumerator() => GetValues().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetValues().GetEnumerator();

		public T this[int index]
		{
			get
			{
				if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
				if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

				var node = SearchNode(Root, index);
				return node.Value;
			}
			set
			{
				if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
				if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

				var node = SearchNode(Root, index);
				node.Value = value;
			}
		}

		public void Prepend(T value)
		{
			Insert(0, value);
		}

		public void Add(T value)
		{
			Insert(Count, value);
		}

		public void Insert(int index, T value)
		{
			if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
			if (index > Count) throw new ArgumentOutOfRangeException(nameof(index));

			var newNode = new Node { Value = value };
			Root = Insert(Root, index, newNode);
		}

		Node Insert(Node node, int index, Node newNode)
		{
			if (node == null)
			{
				++Count;
				return newNode;
			}

			var d = index - node.LeftCount;
			if (d <= 0)
				node.Left = Insert(node.Left, index, newNode);
			else
				node.Right = Insert(node.Right, d - 1, newNode);

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

			var node = SearchNode(Root, index);
			var value = node.Value;
			RemoveTarget(node);
			--Count;
			return value;
		}

		// Suppose t != null.
		void RemoveTarget(Node t)
		{
			if (t.Left == null || t.Right == null)
			{
				var c = t.Left ?? t.Right;

				if (t.Parent == null)
				{
					Root = c;
				}
				else if (t.Parent.Left == t)
				{
					t.Parent.Left = c;
				}
				else
				{
					t.Parent.Right = c;
				}

				t.Parent?.UpdateCount(true);
			}
			else
			{
				var t2 = SearchNextNode(t);
				t.Value = t2.Value;
				RemoveTarget(t2);
			}
		}
	}
}
