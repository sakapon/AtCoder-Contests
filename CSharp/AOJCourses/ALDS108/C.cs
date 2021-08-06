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
				Console.WriteLine(" " + string.Join(" ", set.GetItems()));
				var r = new List<int>();
				set.Root.WalkByPreorder(r.Add);
				Console.WriteLine(" " + string.Join(" ", r));
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

	BstNode<T> _root;
	public BstNode<T> Root
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

	public BSTree(Comparison<T> comparison = null)
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

	public bool Add(T item)
	{
		var c = Count;
		Root = Add(Root, item);
		return Count != c;
	}

	public bool Remove(T item)
	{
		var c = Count;
		Root = Remove(Root, item);
		return Count != c;
	}

	BstNode<T> Add(BstNode<T> node, T item)
	{
		if (node == null)
		{
			++Count;
			return new BstNode<T> { Key = item };
		}

		var d = compare(item, node.Key);
		if (d == 0) return node;

		if (d < 0)
		{
			node.Left = Add(node.Left, item);
		}
		else
		{
			node.Right = Add(node.Right, item);
		}
		return node;
	}

	BstNode<T> Remove(BstNode<T> node, T item)
	{
		if (node == null) return null;

		var d = compare(item, node.Key);
		if (d == 0) return RemoveTarget(node);

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

	BstNode<T> RemoveTarget(BstNode<T> node)
	{
		if (node.Left == null && node.Right == null)
		{
			--Count;
			return null;
		}

		if (node.Right == null) return node.Left;
		if (node.Left == null) return node.Right;

		var t = node.SearchNextNode();
		node.Key = t.Key;
		if (t.Parent.Left == t)
			t.Parent.Left = RemoveTarget(t);
		else
			t.Parent.Right = RemoveTarget(t);
		return node;
	}

	// Suppose values are distinct and sorted.
	public void SetItems(T[] items)
	{
		Root = CreateSubtree(items, 0, items.Length);
	}

	static BstNode<T> CreateSubtree(T[] items, int l, int r)
	{
		if (r - l == 0) return null;
		if (r - l == 1) return new BstNode<T> { Key = items[l] };

		var m = (l + r) / 2;
		return new BstNode<T>
		{
			Key = items[m],
			Left = CreateSubtree(items, l, m),
			Right = CreateSubtree(items, m + 1, r),
		};
	}
}
