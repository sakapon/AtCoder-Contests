using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees.Bsts;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (l, qc) = Read2();

		var set = new AvlSortedSet<int>();
		set.Add(0);
		set.Add(l);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var query = Read();
			var x = query[1];

			if (query[0] == 1)
			{
				set.Add(x);
			}
			else
			{
				var n2 = set.Root.SearchFirstNode(v => v > x);
				var n1 = n2.SearchPreviousNode();
				Console.WriteLine(n2.Key - n1.Key);
			}
		}
		Console.Out.Flush();
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class AvlSortedSet<T> : IEnumerable<T>
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

			public int Height { get; private set; } = 1;
			public int LeftHeight => Left?.Height ?? 0;
			public int RightHeight => Right?.Height ?? 0;

			internal void UpdateHeight(bool recursive = false)
			{
				Height = Math.Max(LeftHeight, RightHeight) + 1;
				if (recursive) Parent?.UpdateHeight(true);
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

			public Node SearchNode(T key, IComparer<T> comparer)
			{
				var d = comparer.Compare(key, Key);
				if (d == 0) return this;
				if (d < 0) return Left?.SearchNode(key, comparer);
				else return Right?.SearchNode(key, comparer);
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

		public bool Add(T item)
		{
			var c = Count;
			SetRoot(Insert(Root, item));
			return Count != c;
		}

		public bool Remove(T item)
		{
			var node = Root?.SearchNode(item, Comparer);
			if (node == null) return false;

			RemoveTarget(node);
			return true;
		}

		#region Private Methods

		Node Insert(Node node, T item)
		{
			if (node == null)
			{
				++Count;
				return new Node { Key = item };
			}

			var d = Comparer.Compare(item, node.Key);
			if (d == 0) return node;

			if (d < 0)
				node.SetLeft(Insert(node.Left, item));
			else
				node.SetRight(Insert(node.Right, item));

			node = Balance(node);
			node.UpdateHeight();
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

				t.Parent?.UpdateHeight(true);
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
			var lrh = t.LeftHeight - t.RightHeight;
			if (lrh > 2 || lrh == 2 && t.Left.LeftHeight >= t.Left.RightHeight)
			{
				t = RotateToRight(t);
				t.Right.UpdateHeight();
			}
			else if (lrh < -2 || lrh == -2 && t.Right.LeftHeight <= t.Right.RightHeight)
			{
				t = RotateToLeft(t);
				t.Left.UpdateHeight();
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
