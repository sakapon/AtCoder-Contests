using System;
using System.Collections.Generic;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new TreapD<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (n-- > 0)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "insert")
			{
				var v = int.Parse(q[1]);
				var p = int.Parse(q[2]);
				set.Add(v, p);
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

// この問題用の Treap です。
public class TreapD<T>
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Key}\}")]
	class Node
	{
		public T Key;
		public Node Parent;
		public int Priority;

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

	public TreapD(Comparison<T> comparison = null)
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
		if (f(node.Key)) return SearchMinNode(node.Left, f) ?? node;
		else return SearchMinNode(node.Right, f);
	}

	static Node SearchMaxNode(Node node, Func<T, bool> f)
	{
		if (node == null) return null;
		if (f(node.Key)) return SearchMaxNode(node.Right, f) ?? node;
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
		var d = compare(value, node.Key);
		if (d == 0) return node;
		if (d < 0) return SearchNode(node.Left, value);
		else return SearchNode(node.Right, value);
	}

	public T GetMin()
	{
		if (Root == null) throw new InvalidOperationException("The tree is empty.");
		return SearchMinNode(Root).Key;
	}

	public T GetMax()
	{
		if (Root == null) throw new InvalidOperationException("The tree is empty.");
		return SearchMaxNode(Root).Key;
	}

	public T GetNextValue(T value, T defaultValue = default(T))
	{
		var node = SearchNode(Root, value);
		if (node == null) throw new InvalidOperationException("The value does not exist.");
		node = SearchNextNode(node);
		if (node == null) return defaultValue;
		return node.Key;
	}

	public T GetPreviousValue(T value, T defaultValue = default(T))
	{
		var node = SearchNode(Root, value);
		if (node == null) throw new InvalidOperationException("The value does not exist.");
		node = SearchPreviousNode(node);
		if (node == null) return defaultValue;
		return node.Key;
	}

	public IEnumerable<T> GetValues()
	{
		for (var n = SearchMinNode(Root); n != null; n = SearchNextNode(n))
		{
			yield return n.Key;
		}
	}

	public IEnumerable<T> GetValues(Func<T, bool> predicateForMin, Func<T, bool> predicateForMax)
	{
		for (var n = SearchMinNode(Root, predicateForMin); n != null && predicateForMax(n.Key); n = SearchNextNode(n))
		{
			yield return n.Key;
		}
	}

	public bool Contains(T value)
	{
		return SearchNode(Root, value) != null;
	}

	public bool Add(T value, int priority)
	{
		var c = Count;
		Root = Add(Root, value, priority);
		return Count != c;
	}

	Node Add(Node t, T value, int priority)
	{
		if (t == null)
		{
			++Count;
			return new Node { Key = value, Priority = priority };
		}

		var d = compare(value, t.Key);
		if (d == 0) return t;

		if (d < 0)
		{
			t.Left = Add(t.Left, value, priority);
			if (t.Priority < t.Left.Priority)
				t = RotateToRight(t);
		}
		else
		{
			t.Right = Add(t.Right, value, priority);
			if (t.Priority < t.Right.Priority)
				t = RotateToLeft(t);
		}
		return t;
	}

	public bool Remove(T value)
	{
		var c = Count;
		Root = Remove(Root, value);
		return Count != c;
	}

	Node Remove(Node t, T value)
	{
		if (t == null) return null;

		var d = compare(value, t.Key);
		if (d == 0) return RemoveTarget(t, value);

		if (d < 0)
		{
			t.Left = Remove(t.Left, value);
		}
		else
		{
			t.Right = Remove(t.Right, value);
		}
		return t;
	}

	Node RemoveTarget(Node t, T value)
	{
		if (t.Left == null && t.Right == null)
		{
			--Count;
			return null;
		}

		if (t.Right == null) return t.Left;
		if (t.Left == null) return t.Right;

		if (t.Left.Priority > t.Right.Priority)
			t = RotateToRight(t);
		else
			t = RotateToLeft(t);
		return Remove(t, value);
	}

	static Node RotateToRight(Node t)
	{
		var np = t.Left;
		t.Left = np.Right;
		np.Right = t;
		return np;
	}

	static Node RotateToLeft(Node t)
	{
		var np = t.Right;
		t.Right = np.Left;
		np.Left = t;
		return np;
	}

	// Additional
	public T[] GetByPreorder()
	{
		var r = new List<T>();

		Action<Node> Dfs = null;
		Dfs = n =>
		{
			if (n == null) return;
			r.Add(n.Key);
			Dfs(n.Left);
			Dfs(n.Right);
		};

		Dfs(Root);
		return r.ToArray();
	}
}
