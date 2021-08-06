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

	[System.Diagnostics.DebuggerDisplay(@"\{{Key}\}")]
	class BstNode
	{
		public T Key;
		public BstNode Parent;

		BstNode _left;
		public BstNode Left
		{
			get { return _left; }
			set
			{
				_left = value;
				if (value != null) value.Parent = this;
			}
		}

		BstNode _right;
		public BstNode Right
		{
			get { return _right; }
			set
			{
				_right = value;
				if (value != null) value.Parent = this;
			}
		}
	}

	BstNode _root;
	BstNode Root
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

	public BSTree(Comparison<T> comparison = null)
	{
		compare = comparison ?? Comparer<T>.Default.Compare;
	}

	static BstNode SearchMinNode(BstNode node)
	{
		if (node == null) return null;
		return SearchMinNode(node.Left) ?? node;
	}

	static BstNode SearchMaxNode(BstNode node)
	{
		if (node == null) return null;
		return SearchMaxNode(node.Right) ?? node;
	}

	static BstNode SearchMinNode(BstNode node, Func<T, bool> f)
	{
		if (node == null) return null;
		if (f(node.Key)) return SearchMinNode(node.Left, f) ?? node;
		else return SearchMinNode(node.Right, f);
	}

	static BstNode SearchMaxNode(BstNode node, Func<T, bool> f)
	{
		if (node == null) return null;
		if (f(node.Key)) return SearchMaxNode(node.Right, f) ?? node;
		else return SearchMaxNode(node.Left, f);
	}

	static BstNode SearchPreviousAncestor(BstNode node)
	{
		if (node == null) return null;
		if (node.Parent == null) return null;
		if (node.Parent.Right == node) return node.Parent;
		else return SearchPreviousAncestor(node.Parent);
	}

	static BstNode SearchNextAncestor(BstNode node)
	{
		if (node == null) return null;
		if (node.Parent == null) return null;
		if (node.Parent.Left == node) return node.Parent;
		else return SearchNextAncestor(node.Parent);
	}

	static BstNode SearchPreviousNode(BstNode node)
	{
		if (node == null) return null;
		return SearchMaxNode(node.Left) ?? SearchPreviousAncestor(node);
	}

	static BstNode SearchNextNode(BstNode node)
	{
		if (node == null) return null;
		return SearchMinNode(node.Right) ?? SearchNextAncestor(node);
	}

	BstNode SearchNode(BstNode node, T value)
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

	public bool Add(T value)
	{
		bool r;
		if (Root == null)
		{
			Root = new BstNode { Key = value };
			r = true;
		}
		else
		{
			r = Add(Root, value);
		}

		if (r) ++Count;
		return r;
	}

	// Suppose t != null.
	bool Add(BstNode t, T value)
	{
		var d = compare(value, t.Key);
		if (d == 0) return false;
		if (d < 0)
		{
			if (t.Left != null) return Add(t.Left, value);
			t.Left = new BstNode { Key = value };
		}
		else
		{
			if (t.Right != null) return Add(t.Right, value);
			t.Right = new BstNode { Key = value };
		}
		return true;
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
	void Remove(BstNode t)
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
		}
		else
		{
			var t2 = SearchNextNode(t);
			t.Key = t2.Key;
			Remove(t2);
		}
	}

	// Suppose values are distinct and sorted.
	public void SetValues(T[] values)
	{
		Root = CreateSubtree(values, 0, values.Length);
	}

	BstNode CreateSubtree(T[] values, int l, int r)
	{
		if (r - l == 0) return null;
		if (r - l == 1) return new BstNode { Key = values[l] };

		var m = (l + r) / 2;
		return new BstNode
		{
			Key = values[m],
			Left = CreateSubtree(values, l, m),
			Right = CreateSubtree(values, m + 1, r),
		};
	}

	// Additional
	public T[] GetByPreorder()
	{
		var r = new List<T>();

		Action<BstNode> Dfs = null;
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
