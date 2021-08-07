using System;
using System.Collections.Generic;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var set = new BinarySearchTree<int>();

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
				set.Root.WalkByPreorder(v => Console.Write($" {v}"));
				Console.WriteLine();
			}
		}
		Console.Out.Flush();
	}
}

public class BinarySearchTree<T> : BstBase<T, BstNode<T>>
{
	public static BinarySearchTree<T> Create(bool descending = false) =>
		new BinarySearchTree<T>(ComparisonHelper.Create<T>(descending));
	public static BinarySearchTree<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false) =>
		new BinarySearchTree<T>(ComparisonHelper.Create(keySelector, descending));

	public BinarySearchTree(Comparison<T> comparison = null) : base(comparison) { }

	public override bool Add(T item)
	{
		var c = Count;
		Root = Add(Root, item);
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

	public override bool Remove(T item)
	{
		var node = Root.SearchNode(item, compare);
		if (node == null) return false;

		RemoveTarget(node);
		--Count;
		return true;
	}

	// Suppose t != null.
	void RemoveTarget(BstNode<T> t)
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
			var t2 = t.SearchNextNode();
			t.Key = t2.Key;
			RemoveTarget(t2);
		}
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
