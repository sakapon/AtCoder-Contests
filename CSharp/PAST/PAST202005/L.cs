using System;
using System.Collections.Generic;
using System.Linq;

class L
{
	class P
	{
		public int i, j, t;
		public bool end;
		public P(int _i, int _j, int _t) { i = _i; j = _j; t = _t; }
	}

	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Enumerable.Range(0, n).Select(i => Read().Skip(1).Select((t, j) => new P(i, j, t)).ToArray()).ToArray();
		var m = int.Parse(Console.ReadLine());
		var a = Read();

		var qs = Enumerable.Range(0, n).Select(i => new Queue<P>(ps[i].Skip(1))).ToArray();
		var pq1 = PQ<P>.Create(p => p.t, ps.SelectMany(x => x.Take(1)).ToArray(), true);
		var pq2 = PQ<P>.Create(p => p.t, ps.SelectMany(x => x.Take(2)).ToArray(), true);

		var r = new List<int>();
		foreach (var x in a)
		{
			P p;
			if (x == 1)
			{
				while ((p = pq1.Pop()).end) ;
				p.end = true;
				r.Add(p.t);

				var q = qs[p.i];
				if (!q.Any()) continue;
				pq1.Push(q.Dequeue());
				if (q.Any()) pq2.Push(q.Peek());
			}
			else
			{
				while ((p = pq2.Pop()).end) ;
				p.end = true;
				r.Add(p.t);

				var q = qs[p.i];
				if (!q.Any()) continue;
				if (q.Peek() == p) q.Dequeue();
				else pq1.Push(q.Dequeue());
				if (q.Any()) pq2.Push(q.Peek());
			}
		}
		Console.WriteLine(string.Join("\n", r));
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
