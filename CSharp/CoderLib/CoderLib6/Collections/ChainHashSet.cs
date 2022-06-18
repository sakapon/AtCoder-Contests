using System;
using System.Collections.Generic;

namespace CoderLib6.Collections
{
	public class ChainHashSet<T>
	{
		const long a = 10007;
		const long p = 2013265921;
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
		public IEqualityComparer<T> Comparer { get; } = EqualityComparer<T>.Default;
		public int Count { get; private set; }

		public ChainHashSet(int size)
		{
			nodes = new Node[this.size = ToPowerOf2(size)];
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
			var h = Hash(item);

			if (nodes[h] == null)
			{
				nodes[h] = new Node { Item = item };
				++Count;
				return true;
			}

			for (var n = nodes[h]; ; n = n.Next)
			{
				if (Comparer.Equals(n.Item, item)) return false;
				if (n.Next == null)
				{
					n.Next = new Node { Item = item };
					++Count;
					return true;
				}
			}
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
	}
}
