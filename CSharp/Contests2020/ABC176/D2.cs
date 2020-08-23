using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public bool IsInRange => 0 <= i && i < h && 0 <= j && j < w;
		public P[] Nexts() => new[] { new P(i, j - 1), new P(i, j + 1), new P(i - 1, j), new P(i + 1, j) };
	}

	static int h, w;

	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var z = Read();
		h = z[0]; w = z[1];
		var s = Read();
		var g = Read();
		var c = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var sp = new P(s[0] - 1, s[1] - 1);
		var gp = new P(g[0] - 1, g[1] - 1);

		var u = new int[h, w];
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				u[i, j] = c[i][j] == '.' ? 1 << 30 : -1;
		var pq = PQ<P>.Create(p => u[p.i, p.j]);

		u[sp.i, sp.j] = 0;
		pq.Push(sp);

		// Dijkstra
		while (pq.Any())
		{
			var p = pq.Pop();

			foreach (var x in p.Nexts())
			{
				if (!x.IsInRange || u[x.i, x.j] <= u[p.i, p.j]) continue;
				u[x.i, x.j] = u[p.i, p.j];
				pq.Push(x);
			}

			for (int i = -2; i <= 2; i++)
				for (int j = -2; j <= 2; j++)
				{
					var x = new P(p.i + i, p.j + j);
					if (!x.IsInRange || u[x.i, x.j] <= u[p.i, p.j] + 1) continue;
					u[x.i, x.j] = u[p.i, p.j] + 1;
					pq.Push(x);
				}
		}
		var r = u[gp.i, gp.j];
		Console.WriteLine(r == 1 << 30 ? -1 : r);
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
