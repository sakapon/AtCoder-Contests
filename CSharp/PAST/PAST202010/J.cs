using System;
using System.Collections.Generic;
using System.Linq;

class J
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var x = Read();
		int xab = x[0], xac = x[1], xbc = x[2];
		var s = Console.ReadLine();
		var es = new bool[m].Select(_ => Read()).ToList();
		es.AddRange(es.ToArray().Select(e => new[] { e[1], e[0], e[2] }));

		// 1: AB, 2: BA
		// 3: AC, 4: CA
		// 5: BC, 6: CB
		for (int v = 1; v <= n; v++)
		{
			switch (s[v - 1])
			{
				case 'A':
					es.Add(new[] { v, n + 1, xab });
					es.Add(new[] { v, n + 3, xac });
					es.Add(new[] { n + 2, v, 0 });
					es.Add(new[] { n + 4, v, 0 });
					break;
				case 'B':
					es.Add(new[] { v, n + 2, xab });
					es.Add(new[] { v, n + 5, xbc });
					es.Add(new[] { n + 1, v, 0 });
					es.Add(new[] { n + 6, v, 0 });
					break;
				case 'C':
					es.Add(new[] { v, n + 4, xac });
					es.Add(new[] { v, n + 6, xbc });
					es.Add(new[] { n + 3, v, 0 });
					es.Add(new[] { n + 5, v, 0 });
					break;
				default:
					break;
			}
		}

		Console.WriteLine(Dijkstra(n + 7, es.ToArray(), true, 1, n).Item1[n]);
	}

	// es: { from, to, cost }
	// 最小コスト: 到達不可能の場合、MaxValue。
	// 入辺: 到達不可能の場合、null。
	static Tuple<long[], int[][]> Dijkstra(int n, int[][] es, bool directed, int sv, int ev = -1)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}

		var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var inEdges = new int[n][];
		var q = PQ<int>.CreateWithKey(v => cs[v]);
		cs[sv] = 0;
		q.Push(sv);

		while (q.Count > 0)
		{
			var vc = q.Pop();
			var v = vc.Value;
			if (v == ev) break;
			if (cs[v] < vc.Key) continue;

			foreach (var e in map[v])
			{
				if (cs[e[1]] <= cs[v] + e[2]) continue;
				cs[e[1]] = cs[v] + e[2];
				inEdges[e[1]] = e;
				q.Push(e[1]);
			}
		}
		return Tuple.Create(cs, inEdges);
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

	public static PQ<T, TKey> CreateWithKey<TKey>(Func<T, TKey> toKey, bool desc = false)
	{
		var c = Comparer<TKey>.Default;
		return desc ?
			new PQ<T, TKey>(toKey, (x, y) => c.Compare(y.Key, x.Key)) :
			new PQ<T, TKey>(toKey, (x, y) => c.Compare(x.Key, y.Key));
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

class PQ<T, TKey> : PQ<KeyValuePair<TKey, T>>
{
	Func<T, TKey> ToKey;
	internal PQ(Func<T, TKey> toKey, Comparison<KeyValuePair<TKey, T>> c) : base(c) { ToKey = toKey; }

	public void Push(T v) => Push(new KeyValuePair<TKey, T>(ToKey(v), v));
	public void PushRange(T[] vs) { foreach (var v in vs) Push(v); }
}
