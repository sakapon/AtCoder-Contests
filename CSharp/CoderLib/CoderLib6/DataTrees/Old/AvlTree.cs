using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/7/ITP2_7_C
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_f
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_g
	// Test: https://atcoder.jp/contests/past202104-open/tasks/past202104_m
	// Test: https://atcoder.jp/contests/past202107-open/tasks/past202107_l
	public class AvlTree<T>
	{
		public static AvlTree<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false)
		{
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

			var c = Comparer<TKey>.Default;
			return descending ?
				new AvlTree<T>((x, y) => c.Compare(keySelector(y), keySelector(x))) :
				new AvlTree<T>((x, y) => c.Compare(keySelector(x), keySelector(y)));
		}

		[System.Diagnostics.DebuggerDisplay(@"\{{Value}\}")]
		class Node
		{
			public T Value;
			public Node Parent;
			public int Height = 1;

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

			public void UpdateHeight()
			{
				var lh = Left?.Height ?? 0;
				var rh = Right?.Height ?? 0;
				Height = Math.Max(lh, rh) + 1;
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

		Comparison<T> compare;
		public int Count { get; private set; }

		public AvlTree(Comparison<T> comparison = null)
		{
			compare = comparison ?? Comparer<T>.Default.Compare;
		}

		static Node SearchMinNode(Node node)
		{
			if (node == null) return null;
			return SearchMinNode(node.Left) ?? node;
		}

		static Node SearchMaxNode(Node node)
		{
			if (node == null) return null;
			return SearchMaxNode(node.Right) ?? node;
		}

		static Node SearchMinNode(Node node, Func<T, bool> f)
		{
			if (node == null) return null;
			if (f(node.Value)) return SearchMinNode(node.Left, f) ?? node;
			else return SearchMinNode(node.Right, f);
		}

		static Node SearchMaxNode(Node node, Func<T, bool> f)
		{
			if (node == null) return null;
			if (f(node.Value)) return SearchMaxNode(node.Right, f) ?? node;
			else return SearchMaxNode(node.Left, f);
		}

		static Node SearchPreviousAncestor(Node node)
		{
			if (node == null) return null;
			if (node.Parent == null) return null;
			if (node.Parent.Right == node) return node.Parent;
			else return SearchPreviousAncestor(node.Parent);
		}

		static Node SearchNextAncestor(Node node)
		{
			if (node == null) return null;
			if (node.Parent == null) return null;
			if (node.Parent.Left == node) return node.Parent;
			else return SearchNextAncestor(node.Parent);
		}

		static Node SearchPreviousNode(Node node)
		{
			if (node == null) return null;
			return SearchMaxNode(node.Left) ?? SearchPreviousAncestor(node);
		}

		static Node SearchNextNode(Node node)
		{
			if (node == null) return null;
			return SearchMinNode(node.Right) ?? SearchNextAncestor(node);
		}

		Node SearchNode(Node node, T value)
		{
			if (node == null) return null;
			var d = compare(value, node.Value);
			if (d == 0) return node;
			if (d < 0) return SearchNode(node.Left, value);
			else return SearchNode(node.Right, value);
		}

		public T GetMin()
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return SearchMinNode(Root).Value;
		}

		public T GetMax()
		{
			if (Root == null) throw new InvalidOperationException("The tree is empty.");
			return SearchMaxNode(Root).Value;
		}

		public T GetNextValue(T value, T defaultValue = default(T))
		{
			var node = SearchNode(Root, value);
			if (node == null) throw new InvalidOperationException("The value does not exist.");
			node = SearchNextNode(node);
			if (node == null) return defaultValue;
			return node.Value;
		}

		public T GetPreviousValue(T value, T defaultValue = default(T))
		{
			var node = SearchNode(Root, value);
			if (node == null) throw new InvalidOperationException("The value does not exist.");
			node = SearchPreviousNode(node);
			if (node == null) return defaultValue;
			return node.Value;
		}

		public IEnumerable<T> GetValues()
		{
			for (var n = SearchMinNode(Root); n != null; n = SearchNextNode(n))
			{
				yield return n.Value;
			}
		}

		public IEnumerable<T> GetValues(Func<T, bool> predicateForMin, Func<T, bool> predicateForMax)
		{
			for (var n = SearchMinNode(Root, predicateForMin); n != null && predicateForMax(n.Value); n = SearchNextNode(n))
			{
				yield return n.Value;
			}
		}

		public bool Contains(T value)
		{
			return SearchNode(Root, value) != null;
		}

		public bool Add(T value)
		{
			Node node;
			if (Root == null)
			{
				node = Root = new Node { Value = value };
			}
			else
			{
				node = Add(Root, value);
			}

			if (node != null)
			{
				Rotate(node.Parent);
				++Count;
			}
			return node != null;
		}

		// Suppose t != null.
		Node Add(Node t, T value)
		{
			var d = compare(value, t.Value);
			if (d == 0) return null;
			if (d < 0)
			{
				if (t.Left != null) return Add(t.Left, value);
				return t.Left = new Node { Value = value };
			}
			else
			{
				if (t.Right != null) return Add(t.Right, value);
				return t.Right = new Node { Value = value };
			}
		}

		void Rotate(Node t)
		{
			if (t == null) return;

			var lh = t.Left?.Height ?? 0;
			var rh = t.Right?.Height ?? 0;
			if (Math.Abs(rh - lh) <= 1)
			{
				t.UpdateHeight();
				Rotate(t.Parent);
				return;
			}

			var p = t.Parent;

			if (lh > rh)
			{
				// to right
				var np = t.Left;
				t.Left = np.Right;
				np.Right = t;
				t.UpdateHeight();
				np.UpdateHeight();
			}
			else
			{
				// to left
				var np = t.Right;
				t.Right = np.Left;
				np.Left = t;
				t.UpdateHeight();
				np.UpdateHeight();
			}

			if (p == null)
			{
				Root = t.Parent;
			}
			else if (p.Left == t)
			{
				p.Left = t.Parent;
			}
			else
			{
				p.Right = t.Parent;
			}

			Rotate(p);
		}

		public bool Remove(T value)
		{
			var node = SearchNode(Root, value);
			if (node == null) return false;

			Remove(node);
			--Count;
			return true;
		}

		// Suppose t != null.
		void Remove(Node t)
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

				Rotate(t.Parent);
			}
			else
			{
				var t2 = SearchNextNode(t);
				t.Value = t2.Value;
				Remove(t2);
			}
		}
	}
}
