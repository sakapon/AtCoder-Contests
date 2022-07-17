using System;
using System.Collections.Generic;

namespace CoderLib6.Collections
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/4/ALDS1_4_B
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/4/ALDS1_4_C
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/6/ITP2_6_A
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/6/ITP2_6_B
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/7/ITP2_7_B
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_aa
	public class ChainHashSet<T> : IEnumerable<T>
	{
		const long a = 1000003;
		const long p = 100000000003;
		static long ModP(long x) => (x %= p) < 0 ? x + p : x;
		static int ToPowerOf2(int n) { for (var p = 1; ; p <<= 1) if (p >= n) return p; }

		int Hash(T item)
		{
			long k = item?.GetHashCode() ?? 0;
			return (int)(ModP(a * k) & (size - 1));
		}

		class Node
		{
			public T Item;
			public Node Next;
		}

		Node[] nodes;
		int size;
		public IEqualityComparer<T> Comparer { get; }
		public int Count { get; private set; }

		public ChainHashSet(int size = 16, IEqualityComparer<T> comparer = null)
		{
			nodes = new Node[this.size = ToPowerOf2(size)];
			Comparer = comparer ?? EqualityComparer<T>.Default;
		}

		public bool Contains(T item)
		{
			var h = Hash(item);
			for (var n = nodes[h]; n != null; n = n.Next)
				if (Comparer.Equals(n.Item, item)) return true;
			return false;
		}

		public bool Add(T item)
		{
			if (Count == size >> 1) Expand();
			var h = Hash(item);

			if (nodes[h] == null)
			{
				nodes[h] = CreateNode(item);
				return true;
			}

			for (var n = nodes[h]; ; n = n.Next)
			{
				if (Comparer.Equals(n.Item, item)) return false;
				if (n.Next == null)
				{
					n.Next = CreateNode(item);
					return true;
				}
			}
		}

		Node CreateNode(T item)
		{
			++Count;
			return new Node { Item = item };
		}

		public bool Remove(T item)
		{
			var h = Hash(item);
			if (nodes[h] == null) return false;

			var n = nodes[h];
			if (Comparer.Equals(n.Item, item))
			{
				nodes[h] = n.Next;
				--Count;
				return true;
			}

			for (; n.Next != null; n = n.Next)
			{
				if (Comparer.Equals(n.Next.Item, item))
				{
					n.Next = n.Next.Next;
					--Count;
					return true;
				}
			}
			return false;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < size; ++i)
			{
				if (nodes[i] == null) continue;
				for (var n = nodes[i]; n != null; n = n.Next)
					yield return n.Item;
			}
		}

		void Expand()
		{
			var data = new T[Count];
			var i = -1;
			foreach (var item in this) data[++i] = item;

			nodes = new Node[size <<= 1];
			Count = 0;
			foreach (var item in data) Add(item);
		}
	}
}
