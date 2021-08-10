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
				r.AddRange(set.GetItems(x => x >= q[1], x => x <= q[2]));
		}
		Console.WriteLine(string.Join("\n", r));
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

public class Treap<T> : BstBase<T, TreapNode<T>>
{
	public static Treap<T> Create(bool descending = false) =>
		new Treap<T>(ComparisonHelper.Create<T>(descending));
	public static Treap<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false) =>
		new Treap<T>(ComparisonHelper.Create(keySelector, descending));

	public Treap(Comparison<T> comparison = null) : base(comparison) { }

	static readonly Random random = new Random();
	HashSet<int> prioritySet = new HashSet<int>();
	int CreatePriority()
	{
		int v;
		while (!prioritySet.Add(v = random.Next())) ;
		return v;
	}
	void RemovePriority(int v) => prioritySet.Remove(v);

	public override bool Add(T item)
	{
		var c = Count;
		Root = Add(Root, item);
		return Count != c;
	}

	TreapNode<T> Add(TreapNode<T> node, T item)
	{
		if (node == null)
		{
			++Count;
			return new TreapNode<T> { Key = item, Priority = CreatePriority() };
		}

		var d = compare(item, node.Key);
		if (d == 0) return node;

		if (d < 0)
		{
			node.TypedLeft = Add(node.TypedLeft, item);
			if (node.Priority < node.TypedLeft.Priority)
				node = node.RotateToRight() as TreapNode<T>;
		}
		else
		{
			node.TypedRight = Add(node.TypedRight, item);
			if (node.Priority < node.TypedRight.Priority)
				node = node.RotateToLeft() as TreapNode<T>;
		}
		return node;
	}

	public override bool Remove(T item)
	{
		var node = Root.SearchNode(item, compare) as TreapNode<T>;
		if (node == null) return false;

		RemoveTarget(node);
		--Count;
		return true;
	}

	// Suppose t != null.
	void RemoveTarget(TreapNode<T> t)
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

			RemovePriority(t.Priority);
		}
		else
		{
			var t2 = t.SearchNextNode() as TreapNode<T>;
			t.Key = t2.Key;
			RemoveTarget(t2);
		}
	}
}
