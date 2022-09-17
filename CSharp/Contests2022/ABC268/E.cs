using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();

		var s = new int[n];
		for (int i = 0; i < n; i++)
		{
			var j = p[i] - i + n;
			s[j % n]++;
		}

		var n2 = (n + 1) / 2;
		var rq = new ArrayDeque<int>(s[..n2]);
		var lq = new ArrayDeque<int>(s[n2..].Reverse());
		var rc = rq.Sum();
		var lc = lq.Sum();

		var t = Enumerable.Range(0, rq.Count).Sum(i => (long)i * rq[i]);
		t += Enumerable.Range(0, lq.Count).Sum(i => (long)(i + 1) * lq[i]);
		var r = t;

		for (int i = 0; i < n; i++)
		{
			var r0 = rq.PopLast();
			var l0 = lq.PopFirst();
			lq.AddLast(r0);
			rq.AddFirst(l0);

			t += rc - r0;
			t -= lc;
			t -= (long)r0 * (rq.Count - 1);
			t += (long)r0 * lq.Count;

			rc += l0 - r0;
			lc -= l0 - r0;

			r = Math.Min(r, t);
		}
		return r;
	}
}

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
