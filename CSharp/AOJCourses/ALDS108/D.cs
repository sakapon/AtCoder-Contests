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
				Console.WriteLine(" " + string.Join(" ", set));
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

	public TreapNode<TKey> TypedParent => Parent as TreapNode<TKey>;
	public TreapNode<TKey> TypedLeft => Left as TreapNode<TKey>;
	public TreapNode<TKey> TypedRight => Right as TreapNode<TKey>;
}

// この問題用の Treap です。
public class TreapD<T> : BstBase<T, TreapNode<T>>
{
	public TreapD(Comparison<T> comparison = null) : base(comparison) { }

	public override bool Add(T item)
	{
		throw new NotImplementedException();
	}

	public bool Add(T item, int priority)
	{
		var c = Count;
		Root = Add(Root, item, priority) as TreapNode<T>;
		return Count != c;
	}

	BstNode<T> Add(BstNode<T> node, T item, int priority)
	{
		if (node == null)
		{
			++Count;
			return new TreapNode<T> { Key = item, Priority = priority };
		}

		var d = compare(item, node.Key);
		if (d == 0) return node;

		var t = node as TreapNode<T>;
		if (d < 0)
		{
			node.Left = Add(node.Left, item, priority);
			if (t.Priority < t.TypedLeft.Priority)
				node = node.RotateToRight();
		}
		else
		{
			node.Right = Add(node.Right, item, priority);
			if (t.Priority < t.TypedRight.Priority)
				node = node.RotateToLeft();
		}
		return node;
	}

	public override bool Remove(T item)
	{
		var c = Count;
		Root = Remove(Root, item) as TreapNode<T>;
		return Count != c;
	}

	BstNode<T> Remove(BstNode<T> node, T item)
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

	// Suppose t != null.
	BstNode<T> RemoveTarget(BstNode<T> t, T item)
	{
		if (t.Left == null && t.Right == null)
		{
			--Count;
			return null;
		}

		if (t.Right == null) return t.Left;
		if (t.Left == null) return t.Right;

		var t2 = t as TreapNode<T>;
		if (t2.TypedLeft.Priority > t2.TypedRight.Priority)
			t = t.RotateToRight();
		else
			t = t.RotateToLeft();
		return Remove(t, item);
	}
}
