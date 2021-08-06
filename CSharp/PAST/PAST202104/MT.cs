﻿using System;
using System.Collections.Generic;
using System.Linq;

class MT
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());

		var d = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());
		var sum = d.Values.Sum(c => c * (c - 1) / 2);

		void Add(int x, int count)
		{
			var c = d.GetValueOrDefault(x);
			sum -= c * (c - 1) / 2;
			c += count;
			sum += c * (c - 1) / 2;
			d[x] = c;
		}

		var set = Treap<(int l, int r)>.Create(_ => _.l);

		for (var (l, r) = (0, 1); r <= n; r++)
		{
			if (r == n || a[r] != a[r - 1])
			{
				set.Add((l, r));
				l = r;
			}
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var (L, R, X) = Read3();
			L--;

			var ranges = set.GetValues(_ => L < _.r, _ => _.l < R).ToArray();
			var (ll, _) = ranges[0];
			var (rl, rr) = ranges[^1];
			var lx = a[ll];
			var rx = a[rl];

			// 関連する区間を全て除きます。
			foreach (var (l, r) in ranges)
			{
				Add(a[l], -(r - l));
				set.Remove((l, r));
			}

			Add(lx, L - ll);
			Add(X, R - L);
			Add(rx, rr - R);

			a[L] = X;
			if (R < rr) a[R] = rx;

			if (ll < L) set.Add((ll, L));
			set.Add((L, R));
			if (R < rr) set.Add((R, rr));

			Console.WriteLine(sum);
		}
		Console.Out.Flush();
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