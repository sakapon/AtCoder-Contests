using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var es = Array.ConvertAll(new bool[h[1]], _ => Read());

		var r = Dijkstra(h[0], es, true, h[2]);
		Console.WriteLine(string.Join("\n", r.Item1.Select(x => x == long.MaxValue ? "INF" : $"{x}")));
	}

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
