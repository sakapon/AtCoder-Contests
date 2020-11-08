using System;
using System.Collections.Generic;

public static class Heap
{
	public static Heap<T> Create<T>(bool descending = false)
	{
		var c = Comparer<T>.Default;
		return descending ?
			new Heap<T>((x, y) => c.Compare(y, x)) :
			new Heap<T>(c.Compare);
	}

	public static Heap<T, TKey> Create<T, TKey>(Func<T, TKey> keySelector, bool descending = false)
	{
		var c = Comparer<TKey>.Default;
		return descending ?
			new Heap<T, TKey>(keySelector, (x, y) => c.Compare(y.Item2, x.Item2)) :
			new Heap<T, TKey>(keySelector, (x, y) => c.Compare(x.Item2, y.Item2));
	}
}

public class Heap<T>
{
	List<T> l = new List<T> { default };
	Comparison<T> c;

	public T First
	{
		get
		{
			if (l.Count <= 1) throw new InvalidOperationException("The heap is empty.");
			return l[1];
		}
	}

	public int Count => l.Count - 1;
	public bool Any => l.Count > 1;

	internal Heap(Comparison<T> comparison)
	{
		c = comparison ?? throw new ArgumentNullException(nameof(comparison));
	}

	// x の親: x/2
	// x の子: 2x, 2x+1
	void UpHeap(int i)
	{
		for (int j; (j = i >> 1) > 0 && c(l[j], l[i]) > 0; i = j)
			(l[i], l[j]) = (l[j], l[i]);
	}

	void DownHeap(int i)
	{
		for (int j; (j = i << 1) < l.Count; i = j)
		{
			if (j + 1 < l.Count && c(l[j], l[j + 1]) > 0) j++;
			if (c(l[i], l[j]) > 0) (l[i], l[j]) = (l[j], l[i]); else break;
		}
	}

	public void Push(T value)
	{
		l.Add(value);
		UpHeap(l.Count - 1);
	}

	public void PushRange(IEnumerable<T> values)
	{
		if (values != null) foreach (var v in values) Push(v);
	}

	public T Pop()
	{
		if (l.Count <= 1) throw new InvalidOperationException("The heap is empty.");

		var r = l[1];
		l[1] = l[l.Count - 1];
		l.RemoveAt(l.Count - 1);
		DownHeap(1);
		return r;
	}
}

public class Heap<T, TKey> : Heap<(T value, TKey key)>
{
	Func<T, TKey> KeySelector;

	internal Heap(Func<T, TKey> keySelector, Comparison<(T, TKey)> comparison) : base(comparison)
	{
		KeySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
	}

	public void Push(T value)
	{
		Push((value, KeySelector(value)));
	}

	public void PushRange(IEnumerable<T> values)
	{
		if (values != null) foreach (var v in values) Push(v);
	}
}
