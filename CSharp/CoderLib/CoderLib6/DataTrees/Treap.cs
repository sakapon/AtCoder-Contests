using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/7/ITP2_7_C
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_f
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_g
	// Test: https://atcoder.jp/contests/past202104-open/tasks/past202104_m
	// Test: https://atcoder.jp/contests/past202107-open/tasks/past202107_l
	public class TreapNode<TKey> : BstNode<TKey>
	{
		public int Priority { get; set; }

		public TreapNode<TKey> TypedParent => Parent as TreapNode<TKey>;
		public TreapNode<TKey> TypedLeft => Left as TreapNode<TKey>;
		public TreapNode<TKey> TypedRight => Right as TreapNode<TKey>;
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
			Root = Add(Root, item) as TreapNode<T>;
			return Count != c;
		}

		BstNode<T> Add(BstNode<T> node, T item)
		{
			if (node == null)
			{
				++Count;
				return new TreapNode<T> { Key = item, Priority = CreatePriority() };
			}

			var d = compare(item, node.Key);
			if (d == 0) return node;

			var t = node as TreapNode<T>;
			if (d < 0)
			{
				node.Left = Add(node.Left, item);
				if (t.Priority < t.TypedLeft.Priority)
					node = node.RotateToRight();
			}
			else
			{
				node.Right = Add(node.Right, item);
				if (t.Priority < t.TypedRight.Priority)
					node = node.RotateToLeft();
			}
			return node;
		}

		public override bool Remove(T item)
		{
			var node = Root.SearchNode(item, compare);
			if (node == null) return false;

			RemoveTarget(node as TreapNode<T>);
			--Count;
			return true;
		}

		// Suppose t != null.
		void RemoveTarget(TreapNode<T> t)
		{
			if (t.Left == null || t.Right == null)
			{
				var c = t.Left ?? t.Right;

				if (t.Parent == null)
				{
					Root = c as TreapNode<T>;
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
				var t2 = t.SearchNextNode();
				t.Key = t2.Key;
				RemoveTarget(t2 as TreapNode<T>);
			}
		}
	}
}
