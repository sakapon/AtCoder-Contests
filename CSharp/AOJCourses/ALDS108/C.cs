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
				Console.WriteLine(" " + string.Join(" ", set));
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

	public BinarySearchTree(IEnumerable<T> collection, Comparison<T> comparison = null) : base(comparison)
	{
		if (collection == null) throw new ArgumentNullException();
		var set = new HashSet<T>(collection);
		var items = new T[set.Count];
		set.CopyTo(items);
		Array.Sort(items, Comparer<T>.Create(compare));
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
			TypedLeft = CreateSubtree(items, l, m),
			TypedRight = CreateSubtree(items, m + 1, r),
		};
	}

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
			node.TypedLeft = Add(node.TypedLeft, item);
		}
		else
		{
			node.TypedRight = Add(node.TypedRight, item);
		}
		return node;
	}

	public override bool Remove(T item)
	{
		var node = Root.SearchNode(item, compare) as BstNode<T>;
		if (node == null) return false;

		RemoveTarget(node);
		--Count;
		return true;
	}

	// Suppose t != null.
	void RemoveTarget(BstNode<T> t)
	{
		if (t.TypedLeft == null || t.TypedRight == null)
		{
			var c = t.TypedLeft ?? t.TypedRight;

			if (t.TypedParent == null)
			{
				Root = c;
			}
			else if (t.TypedParent.TypedLeft == t)
			{
				t.TypedParent.TypedLeft = c;
			}
			else
			{
				t.TypedParent.TypedRight = c;
			}
		}
		else
		{
			var t2 = t.SearchNextNode() as BstNode<T>;
			t.Key = t2.Key;
			RemoveTarget(t2);
		}
	}
}
