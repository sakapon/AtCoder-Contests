using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Read();
		var ls = Read();
		var ds = Read();

		Array.Sort(ps);
		Array.Sort(ls, ds);

		var r = 0L;
		var pq = PQ<int>.Create(true);
		var i = 0;

		foreach (var p in ps)
		{
			while (i < m && ls[i] <= p)
			{
				pq.Push(ds[i]);
				i++;
			}

			r += p;
			if (pq.Count > 0) r -= pq.Pop();
		}

		return r;
	}
}

class PQ<T> : List<T>
{
	public static PQ<T> Create(bool desc = false)
	{
		var c = Comparer<T>.Default;
		return desc ?
			new PQ<T>((x, y) => c.Compare(y, x)) :
			new PQ<T>(c.Compare);
	}

	public static PQ<T> Create<TKey>(Func<T, TKey> toKey, bool desc = false)
	{
		var c = Comparer<TKey>.Default;
		return desc ?
			new PQ<T>((x, y) => c.Compare(toKey(y), toKey(x))) :
			new PQ<T>((x, y) => c.Compare(toKey(x), toKey(y)));
	}

	Comparison<T> c;
	public T First => this[0];
	internal PQ(Comparison<T> _c) { c = _c; }

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
	public void PushRange(T[] vs) { foreach (var v in vs) Push(v); }

	public T Pop()
	{
		var r = this[0];
		this[0] = this[Count - 1];
		RemoveAt(Count - 1);
		DownHeap(0);
		return r;
	}
}
