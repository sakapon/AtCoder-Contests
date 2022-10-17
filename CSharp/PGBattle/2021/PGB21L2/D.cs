using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var p = Read();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var rn = Enumerable.Range(1, n).ToArray();
		var qs = es
			.Where(e => e[0] != e[1])
			.SelectMany(e => new[] { (e[0], false), (e[1], true) })
			.Append((1, false))
			.Append((n, true))
			.Concat(rn.Select(i => (i, false)))
			.Concat(rn.Select(i => (i, true)))
			.ToArray();
		Array.Sort(qs);

		var aq = new Stack<int>();
		var lq = new Stack<List<ArrayDeque<int>>>();
		lq.Push(new List<ArrayDeque<int>>());

		foreach (var (b, close) in qs)
		{
			if (close)
			{
				var a = aq.Pop();
				var l = lq.Pop();

				if (a == b)
				{
					var deq = new ArrayDeque<int>();
					deq.AddLast(p[a - 1]);
					lq.Peek().Add(deq);
				}
				else
				{
					if (l[0][0] > l.Last()[0])
					{
						l.Reverse();
					}

					var deq = l.Aggregate(Merge);
					lq.Peek().Add(deq);
				}
			}
			else
			{
				aq.Push(b);
				lq.Push(new List<ArrayDeque<int>>());
			}
		}

		return string.Join(" ", lq.Pop()[0]);
	}

	public static ArrayDeque<int> Merge(ArrayDeque<int> q1, ArrayDeque<int> q2)
	{
		if (q1 == null) return q2;
		if (q2 == null) return q1;

		if (q1.Count >= q2.Count)
		{
			foreach (var v in q2) q1.AddLast(v);
			return q1;
		}
		else
		{
			foreach (var v in q1.Reverse()) q2.AddFirst(v);
			return q2;
		}
	}
}

namespace CoderLib8.Collections
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayDeque<T> : IEnumerable<T>
	{
		T[] a;
		int fi, li;
		int f;

		public ArrayDeque(int capacity = 2)
		{
			var c = 1;
			while (c < capacity) c <<= 1;
			a = new T[c];
			f = c - 1;
		}

		public ArrayDeque(IEnumerable<T> items) : this() { foreach (var item in items) AddLast(item); }
		public ArrayDeque(T[] items) => Initialize(items, items.Length);
		void Initialize(T[] a0, int c)
		{
			li = c;
			while (c != (c & -c)) c += c & -c;
			a = new T[c];
			Array.Copy(a0, a, li);
			f = c - 1;
		}

		public int Count => li - fi;
		public T this[int i]
		{
			get => a[fi + i & f];
			set => a[fi + i & f] = value;
		}
		public T First
		{
			get => a[fi & f];
			set => a[fi & f] = value;
		}
		public T Last
		{
			get => a[li - 1 & f];
			set => a[li - 1 & f] = value;
		}

		public void Clear() => fi = li = 0;
		public void AddFirst(T item)
		{
			if (li - fi == a.Length) Expand();
			a[--fi & f] = item;
		}
		public void AddLast(T item)
		{
			if (li - fi == a.Length) Expand();
			a[li++ & f] = item;
		}
		public T PopFirst() => a[fi++ & f];
		public T PopLast() => a[--li & f];

		void Expand()
		{
			Array.Resize(ref a, a.Length << 1);
			Array.Copy(a, 0, a, a.Length >> 1, a.Length >> 1);
			f = a.Length - 1;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var i = fi; i < li; ++i) yield return a[i & f]; }

		public T[] ToArray()
		{
			var r = new T[Count];
			for (int i = fi; i < li; ++i) r[i - fi] = a[i & f];
			return r;
		}
	}
}
