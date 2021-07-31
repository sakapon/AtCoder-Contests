using System;
using System.Collections.Generic;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new BSTree<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (n-- > 0)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "insert")
			{
				var v = int.Parse(q[1]);
				set.Add(v);
			}
			else if (q[0] == "find")
			{
				var v = int.Parse(q[1]);
				Console.WriteLine(set.Contains(v) ? "yes" : "no");
			}
			else if (q[0] == "delete")
			{
				var v = int.Parse(q[1]);
				set.Remove(v);
			}
			else
			{
				Console.WriteLine(" " + string.Join(" ", set.GetValues()));
				Console.WriteLine(" " + string.Join(" ", set.GetByPreorder()));
			}
		}
		Console.Out.Flush();
	}
}

public class BSTree<T>
{
	public static BSTree<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false)
	{
		if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

		var c = Comparer<TKey>.Default;
		return descending ?
			new BSTree<T>((x, y) => c.Compare(keySelector(y), keySelector(x))) :
			new BSTree<T>((x, y) => c.Compare(keySelector(x), keySelector(y)));
	}

	[System.Diagnostics.DebuggerDisplay(@"\{{Value}\}")]
	class Node
	{
		public T Value;
		public Node Parent, Left, Right;

		public void SetLeft(Node child)
		{
			Left = child;
			if (child != null) child.Parent = this;
		}

		public void SetRight(Node child)
		{
			Right = child;
			if (child != null) child.Parent = this;
		}
	}

	const int None = -1;

	Node root;
	Comparison<T> c;
	public int Count { get; private set; }

	public BSTree(Comparison<T> comparison = null)
	{
		c = comparison ?? Comparer<T>.Default.Compare;
	}

	void SetRoot(Node node)
	{
		root = node;
		if (node != null) node.Parent = null;
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

	Node SearchPreviousParent(Node node, T value)
	{
		if (node == null) return null;
		if (c(node.Value, value) < 0) return node;
		else return SearchPreviousParent(node.Parent, value);
	}

	Node SearchNextParent(Node node, T value)
	{
		if (node == null) return null;
		if (c(node.Value, value) > 0) return node;
		else return SearchNextParent(node.Parent, value);
	}

	Node SearchPreviousNode(Node node)
	{
		if (node == null) return null;
		return SearchMaxNode(node.Left) ?? SearchPreviousParent(node.Parent, node.Value);
	}

	Node SearchNextNode(Node node)
	{
		if (node == null) return null;
		return SearchMinNode(node.Right) ?? SearchNextParent(node.Parent, node.Value);
	}

	public T GetMin()
	{
		if (root == null) throw new InvalidOperationException("The tree is empty.");
		return SearchMinNode(root).Value;
	}

	public T GetMax()
	{
		if (root == null) throw new InvalidOperationException("The tree is empty.");
		return SearchMaxNode(root).Value;
	}

	public IEnumerable<T> GetValues()
	{
		for (var n = SearchMinNode(root); n != null; n = SearchNextNode(n))
		{
			yield return n.Value;
		}
	}

	public IEnumerable<T> GetValues(Func<T, bool> predicateForMin, Func<T, bool> predicateForMax)
	{
		for (var n = SearchMinNode(root, predicateForMin); n != null && predicateForMax(n.Value); n = SearchNextNode(n))
		{
			yield return n.Value;
		}
	}

	Node SearchNode(T value)
	{
		if (root == null) return null;

		var t = root;
		int d;
		while ((d = c(value, t.Value)) != 0)
		{
			if (d < 0)
			{
				if (t.Left == null) return null;
				t = t.Left;
			}
			else
			{
				if (t.Right == null) return null;
				t = t.Right;
			}
		}
		return t;
	}

	public bool Contains(T value)
	{
		return SearchNode(value) != null;
	}

	public bool Add(T value)
	{
		if (root == null)
		{
			SetRoot(new Node { Value = value });
			++Count;
			return true;
		}

		var t = root;
		int d;
		while ((d = c(value, t.Value)) != 0)
		{
			if (d < 0)
			{
				if (t.Left == null)
				{
					t.SetLeft(new Node { Value = value });
					++Count;
					return true;
				}
				t = t.Left;
			}
			else
			{
				if (t.Right == null)
				{
					t.SetRight(new Node { Value = value });
					++Count;
					return true;
				}
				t = t.Right;
			}
		}
		return false;
	}

	public bool Remove(T value)
	{
		var node = SearchNode(value);
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
				SetRoot(c);
			}
			else if (t.Parent.Left == t)
			{
				t.Parent.SetLeft(c);
			}
			else
			{
				t.Parent.SetRight(c);
			}
		}
		else
		{
			var t2 = SearchNextNode(t);
			t.Value = t2.Value;
			Remove(t2);
		}
	}

	// Additional
	public T[] GetByPreorder()
	{
		var r = new List<T>();

		Action<Node> Dfs = null;
		Dfs = n =>
		{
			if (n == null) return;
			r.Add(n.Value);
			Dfs(n.Left);
			Dfs(n.Right);
		};

		Dfs(root);
		return r.ToArray();
	}
}
