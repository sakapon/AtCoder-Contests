using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();

		var x1 = new PQ<long>(a.Take(n));
		var x2 = new PQ<long>(a.Skip(2 * n), (x, y) => -x.CompareTo(y));
		var s1 = new long[n + 1];
		var s2 = new long[n + 1];
		s1[0] = x1.Sum();
		s2[n] = x2.Sum();

		for (int i = 0, j = n - 1; i < n; i++, j--)
		{
			var v1 = a[n + i];
			if (v1 > x1.First)
			{
				s1[i + 1] = s1[i] + v1 - x1.Pop();
				x1.Push(v1);
			}
			else s1[i + 1] = s1[i];

			var v2 = a[n + j];
			if (v2 < x2.First)
			{
				s2[j] = s2[j + 1] + v2 - x2.Pop();
				x2.Push(v2);
			}
			else s2[j] = s2[j + 1];
		}
		Console.WriteLine(Enumerable.Range(0, n + 1).Max(i => s1[i] - s2[i]));
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
