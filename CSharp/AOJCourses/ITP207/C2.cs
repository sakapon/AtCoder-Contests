using System;
using System.Collections.Generic;

class C2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var r = new List<int>();
		var set = new Treap<int>();

		for (int i = 0; i < n; i++)
		{
			var q = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
			if (q[0] == 0)
			{
				set.Add(q[1]);
				r.Add(set.Count);
			}
			else if (q[0] == 1)
				r.Add(set.Contains(q[1]) ? 1 : 0);
			else if (q[0] == 2)
				set.Remove(q[1]);
			else
				r.AddRange(set.GetValues(x => x >= q[1], x => x <= q[2]));
		}
		Console.WriteLine(string.Join("\n", r));
	}
}

public class Treap<T>
{
	public static Treap<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false)
	{
		if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

		var c = Comparer<TKey>.Default;
		return descending ?
			new Treap<T>((x, y) => c.Compare(keySelector(y), keySelector(x))) :
			new Treap<T>((x, y) => c.Compare(keySelector(x), keySelector(y)));
	}

	[System.Diagnostics.DebuggerDisplay(@"\{{Value}\}")]
	class Node
	{
		public T Value;
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

	HashSet<int> prioritySet = new HashSet<int>();
	Random random = new Random();
	int CreatePriority()
	{
		int v;
		while (!prioritySet.Add(v = random.Next())) ;
		return v;
	}
	void RemovePriority(int v) => prioritySet.Remove(v);

	Comparison<T> compare;
	public int Count { get; private set; }

	public Treap(Comparison<T> comparison = null)
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
			node = Root = new Node { Value = value, Priority = CreatePriority() };
		}
		else
		{
			node = Add(Root, value);
		}

		if (node != null)
		{
			Rotate(node);
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
			return t.Left = new Node { Value = value, Priority = CreatePriority() };
		}
		else
		{
			if (t.Right != null) return Add(t.Right, value);
			return t.Right = new Node { Value = value, Priority = CreatePriority() };
		}
	}

	// Suppose t != null.
	void Rotate(Node t)
	{
		if (t.Parent == null) return;
		if (t.Parent.Priority > t.Priority) return;

		var p = t.Parent;
		var pp = p.Parent;

		if (p.Left == t)
		{
			// to right
			p.Left = t.Right;
			t.Right = p;
		}
		else
		{
			// to left
			p.Right = t.Left;
			t.Left = p;
		}

		if (pp == null)
		{
			Root = t;
		}
		else if (pp.Left == p)
		{
			pp.Left = t;
		}
		else
		{
			pp.Right = t;
		}

		Rotate(t);
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
			RemovePriority(t.Priority);
		}
		else
		{
			var t2 = SearchNextNode(t);
			t.Value = t2.Value;
			Remove(t2);
		}
	}
}
