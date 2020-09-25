using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var w = Console.ReadLine().GroupBy(c => c).Select(g => g.Count()).ToList();
		var k = w.Count;
		if (k == 1) { Console.WriteLine(w[0]); return; }

		var empty = new int[0];
		var map = w.Select(_ => empty).ToList();
		var pq = PQ<int>.Create(v => w[v], Enumerable.Range(0, k).ToArray());
		var root = -1;

		while (pq.Any())
		{
			var v1 = pq.Pop();
			if (!pq.Any()) { root = v1; break; }
			var v2 = pq.Pop();

			w.Add(w[v1] + w[v2]);
			map.Add(new[] { v1, v2 });
			pq.Push(w.Count - 1);
		}

		var r = 0L;
		Action<int, int> Dfs = null;
		Dfs = (v, d) =>
		{
			if (v < k) r += d * w[v];
			foreach (var nv in map[v])
				Dfs(nv, d + 1);
		};

		Dfs(root, 0);
		Console.WriteLine(r);
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
