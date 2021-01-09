using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));

	static long Solve()
	{
		var h = Console.ReadLine().Split().Select(long.Parse).ToArray();
		long n = h[0], d = h[4], Mx = (1L << 60) / d;
		var ops = new[] { (2, h[1]), (3, h[2]), (5, h[3]) };

		var m = n <= Mx ? n * d : 1L << 60;
		var set = new HashSet<long>();
		var pq = PQ<(long x, long cost)>.Create(v => v.cost);
		pq.Push((n, 0));

		while (pq.Any())
		{
			var (x, cost) = pq.Pop();
			if (x <= Mx) m = Math.Min(m, cost + x * d);
			if (x == 1) return m;
			if (set.Contains(x)) continue;
			set.Add(x);

			foreach (var (p, c) in ops)
			{
				var div = x / p;
				for (int i = 0; i < 2; i++)
				{
					var nx = div + i;
					if (nx > 0) pq.Push((nx, cost + c + Math.Abs(nx * p - x) * d));
				}
			}
		}
		return -1;
	}
}

class PQ<T> : List<T>
{
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
