using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var t = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToLookup(x => x[0], x => x[1]);

		var pq = PQ<int>.Create(desc: true);
		var p = new int[n + 1];
		for (int i = 1; i <= n; i++)
		{
			foreach (var b in t[i])
				pq.Push(b);
			p[i] = p[i - 1] + pq.Pop();
		}
		Console.WriteLine(string.Join("\n", p.Skip(1)));
	}
}

class PQ<T> : List<T>
{
	public static PQ<T> Create(T[] vs = null, bool desc = false)
	{
		var c = Comparer<T>.Default;
		return desc ?
			new PQ<T>(vs, (x, y) => c.Compare(y, x)) :
			new PQ<T>(vs, c.Compare);
	}

	public static PQ<T> Create<TKey>(Func<T, TKey> getKey, T[] vs = null, bool desc = false)
	{
		var c = Comparer<TKey>.Default;
		return desc ?
			new PQ<T>(vs, (x, y) => c.Compare(getKey(y), getKey(x))) :
			new PQ<T>(vs, (x, y) => c.Compare(getKey(x), getKey(y)));
	}

	Comparison<T> c;
	public T First => this[0];

	PQ(T[] vs, Comparison<T> _c)
	{
		c = _c;
		if (vs != null) foreach (var v in vs) Push(v);
	}

	void Swap(int i, int j) { var o = this[i]; this[i] = this[j]; this[j] = o; }
	void UpHeap(int i) { for (int j; i > 0 && c(this[j = (i - 1) / 2], this[i]) > 0; Swap(i, i = j)) ; }
	void DownHeap(int i)
	{
		for (int j; (j = 2 * i + 1) < Count;)
		{
			if (j + 1 < Count && c(this[j], this[j + 1]) > 0) j++;
			if (c(this[i], this[j]) > 0) Swap(i, i = j); else break;
		}
	}

	public void Push(T v)
	{
		Add(v);
		UpHeap(Count - 1);
	}
	public T Pop()
	{
		var r = this[0];
		this[0] = this[Count - 1];
		RemoveAt(Count - 1);
		DownHeap(0);
		return r;
	}
}
