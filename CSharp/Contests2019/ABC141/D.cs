using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var a = Console.ReadLine().Split().Select(double.Parse).ToArray();

		var q = new PQ<double>(a, (x, y) => -x.CompareTo(y));
		for (var i = 0; i < h[1]; i++) q.Push(q.Pop() / 2);
		Console.WriteLine(q.Sum(x => (long)x));
	}
}

class PQ<T> : List<T>
{
	Comparison<T> _c;
	public T First => this[0];

	public PQ(IEnumerable<T> vs = null, Comparison<T> c = null)
	{
		_c = c ?? Comparer<T>.Default.Compare;
		if (vs != null) foreach (var v in vs) Push(v);
	}

	void Swap(int i, int j)
	{
		var t = this[i];
		this[i] = this[j];
		this[j] = t;
	}

	void UpHeap(int i) { for (int j; i > 0 && _c(this[(j = (i - 1) / 2)], this[i]) > 0; Swap(i, j), i = j) ; }
	void DownHeap(int i)
	{
		for (int j; (j = 2 * i + 1) < Count; i = j)
		{
			if (j + 1 < Count && _c(this[j], this[j + 1]) > 0) j++;
			if (_c(this[i], this[j]) > 0) Swap(i, j); else break;
		}
	}

	public void Push(T v) { Add(v); UpHeap(Count - 1); }
	public T Pop()
	{
		var r = this[0];
		this[0] = this[Count - 1];
		RemoveAt(Count - 1);
		DownHeap(0);
		return r;
	}
}
