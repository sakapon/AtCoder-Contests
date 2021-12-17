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

public class TreapNode<TKey> : BstNodeBase<TKey>
{
	public override BstNodeBase<TKey> Parent
	{
		get { return TypedParent; }
		set { TypedParent = (TreapNode<TKey>)value; }
	}
	public override BstNodeBase<TKey> Left
	{
		get { return TypedLeft; }
		set { TypedLeft = (TreapNode<TKey>)value; }
	}
	public override BstNodeBase<TKey> Right
	{
		get { return TypedRight; }
		set { TypedRight = (TreapNode<TKey>)value; }
	}

	public TreapNode<TKey> TypedParent { get; set; }

	TreapNode<TKey> _left;
	public TreapNode<TKey> TypedLeft
	{
		get { return _left; }
		set
		{
			_left = value;
			if (value != null) value.TypedParent = this;
		}
	}

	TreapNode<TKey> _right;
	public TreapNode<TKey> TypedRight
	{
		get { return _right; }
		set
		{
			_right = value;
			if (value != null) value.TypedParent = this;
		}
	}

	public int Priority { get; set; }
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
			node.TypedLeft = Add(node.TypedLeft, item, priority);
			if (node.Priority < node.TypedLeft.Priority)
				node = node.RotateToRight() as TreapNode<T>;
		}
		else
		{
			node.TypedRight = Add(node.TypedRight, item, priority);
			if (node.Priority < node.TypedRight.Priority)
				node = node.RotateToLeft() as TreapNode<T>;
		}
		return node;
	}

	public override bool Remove(T item)
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
			node.TypedLeft = Remove(node.TypedLeft, item);
		}
		else
		{
			node.TypedRight = Remove(node.TypedRight, item);
		}
		return node;
	}

	// Suppose t != null.
	TreapNode<T> RemoveTarget(TreapNode<T> t, T item)
	{
		if (t.TypedLeft == null && t.TypedRight == null)
		{
			--Count;
			return null;
		}

		if (t.TypedRight == null) return t.TypedLeft;
		if (t.TypedLeft == null) return t.TypedRight;

		if (t.TypedLeft.Priority > t.TypedRight.Priority)
			t = t.RotateToRight() as TreapNode<T>;
		else
			t = t.RotateToLeft() as TreapNode<T>;
		return Remove(t, item);
	}
}
