using System;
using System.Collections.Generic;
using System.Linq;

class A2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		Console.WriteLine(Prim(h[0] - 1, 0, es).Sum(e => e[2]));
	}

	static int[][] Prim(int n, int sv, int[][] es)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}

		var u = new bool[n + 1];
		u[sv] = true;
		var pq = PQ<int[]>.Create(e => e[2], map[sv].ToArray());
		var minEdges = new List<int[]>();

		while (pq.Count > 0 && minEdges.Count < n)
		{
			var e = pq.Pop();
			if (u[e[1]]) continue;
			u[e[1]] = true;
			minEdges.Add(e);
			foreach (var ne in map[e[1]])
				if (ne[1] != e[0])
					pq.Push(ne);
		}
		return minEdges.ToArray();
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
