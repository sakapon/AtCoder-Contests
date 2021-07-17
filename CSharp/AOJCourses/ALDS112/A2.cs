using System;
using System.Collections.Generic;
using System.Linq;

class A2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Trim().Split().Select(int.Parse).ToArray()).ToArray();

		var es = new List<int[]>();
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				if (a[i][j] != -1)
					es.Add(new[] { i, j, a[i][j] });

		var r = Prim(n, 0, es.ToArray());
		Console.WriteLine(r.Sum(e => e[2]));
	}

	static int[][] Prim(int n, int root, int[][] ues) => Prim(n, root, ToMap(n, ues, false));
	static int[][] Prim(int n, int root, List<int[]>[] map)
	{
		var u = new bool[n];
		var q = PQ<int[]>.Create(e => e[2]);
		u[root] = true;
		q.PushRange(map[root].ToArray());
		var mes = new List<int[]>();

		// 実際の頂点数に注意。
		while (q.Count > 0 && mes.Count < n - 1)
		{
			var e = q.Pop();
			if (u[e[1]]) continue;
			u[e[1]] = true;
			mes.Add(e);
			foreach (var ne in map[e[1]])
				if (ne[1] != e[0])
					q.Push(ne);
		}
		return mes.ToArray();
	}

	static List<int[]>[] ToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}

class PQ<T> : List<T>
{
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
