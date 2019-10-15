using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var a = read().GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		var pq = new PQ<int>(a.Keys);

		var os = new int[h[1]].Select(_ => read()).ToArray();
		foreach (var o in os)
		{
			while (o[0] > 0 && o[1] > pq.First)
			{
				var c = o[1];
				if (o[0] >= a[pq.First])
				{
					var c0 = pq.Pop();
					var d = a[c0];
					if (a.ContainsKey(c)) a[c] += d;
					else
					{
						a[c] = d;
						pq.Push(c);
					}
					a.Remove(c0);
					o[0] -= d;
				}
				else
				{
					var d = o[0];
					if (a.ContainsKey(c)) a[c] += d;
					else
					{
						a[c] = d;
						pq.Push(c);
					}
					a[pq.First] -= d;
					o[0] = 0;
				}
			}
		}
		Console.WriteLine(a.Sum(p => (long)p.Key * p.Value));
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

	void Swap(int i, int j) { var o = this[i]; this[i] = this[j]; this[j] = o; }
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
