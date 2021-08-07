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
				Console.WriteLine(" " + string.Join(" ", set.GetItems()));
				set.Root.WalkByPreorder(v => Console.Write($" {v}"));
				Console.WriteLine();
			}
		}
		Console.Out.Flush();
	}
}

public class TreapNode<TKey> : BstNode<TKey>
{
	public int Priority { get; set; }

	public new TreapNode<TKey> Left
	{
		get { return (TreapNode<TKey>)base.Left; }
		set { base.Left = value; }
	}

	public new TreapNode<TKey> Right
	{
		get { return (TreapNode<TKey>)base.Right; }
		set { base.Right = value; }
	}
}

// この問題用の Treap です。
public class TreapD<T>
{
	TreapNode<T> _root;
	public TreapNode<T> Root
	{
		get { return _root; }
		private set
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

	public bool Contains(T item)
	{
		return Root.SearchNode(item, compare) != null;
	}

	public T GetMin()
	{
		if (Root == null) throw new InvalidOperationException("The tree is empty.");
		return Root.SearchMinNode().Key;
	}

	public T GetMax()
	{
		if (Root == null) throw new InvalidOperationException("The tree is empty.");
		return Root.SearchMaxNode().Key;
	}

	public T GetMin(Func<T, bool> predicate)
	{
		if (Root == null) throw new InvalidOperationException("The tree is empty.");
		return Root.SearchMinNode(predicate).Key;
	}

	public T GetMax(Func<T, bool> predicate)
	{
		if (Root == null) throw new InvalidOperationException("The tree is empty.");
		return Root.SearchMaxNode(predicate).Key;
	}

	public T GetPrevious(T item, T defaultValue = default(T))
	{
		var node = Root.SearchNode(item, compare);
		if (node == null) throw new InvalidOperationException("The item is not found.");
		node = node.SearchPreviousNode();
		if (node == null) return defaultValue;
		return node.Key;
	}

	public T GetNext(T item, T defaultValue = default(T))
	{
		var node = Root.SearchNode(item, compare);
		if (node == null) throw new InvalidOperationException("The item is not found.");
		node = node.SearchNextNode();
		if (node == null) return defaultValue;
		return node.Key;
	}

	public IEnumerable<T> GetItems() => Root.GetKeys();
	public IEnumerable<T> GetItems(Func<T, bool> predicateForMin, Func<T, bool> predicateForMax) => Root.GetKeys(predicateForMin, predicateForMax);

	public bool Add(T item, int priority)
	{
		var c = Count;
		Root = Add(Root, item, priority);
		return Count != c;
	}

	TreapNode<T> Add(TreapNode<T> node, T item, int priority)
	{
		if (node == null)
		{
			++Count;
			return new TreapNode<T> { Key = item, Priority = priority };
		}

		var d = compare(item, node.Key);
		if (d == 0) return node;

		if (d < 0)
		{
			node.Left = Add(node.Left, item, priority);
			if (node.Priority < node.Left.Priority)
				node = RotateToRight(node);
		}
		else
		{
			node.Right = Add(node.Right, item, priority);
			if (node.Priority < node.Right.Priority)
				node = RotateToLeft(node);
		}
		return node;
	}

	public bool Remove(T item)
	{
		var c = Count;
		Root = Remove(Root, item);
		return Count != c;
	}

	TreapNode<T> Remove(TreapNode<T> node, T item)
	{
		if (node == null) return null;

		var d = compare(item, node.Key);
		if (d == 0) return RemoveTarget(node, item);

		if (d < 0)
		{
			node.Left = Remove(node.Left, item);
		}
		else
		{
			node.Right = Remove(node.Right, item);
		}
		return node;
	}

	TreapNode<T> RemoveTarget(TreapNode<T> t, T item)
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
		return Remove(t, item);
	}

	static TreapNode<T> RotateToRight(TreapNode<T> t)
	{
		var np = t.Left;
		t.Left = np.Right;
		np.Right = t;
		return np;
	}

	static TreapNode<T> RotateToLeft(TreapNode<T> t)
	{
		var np = t.Right;
		t.Right = np.Left;
		np.Left = t;
		return np;
	}
}
