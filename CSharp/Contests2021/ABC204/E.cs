using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int a, int b, int c, int d) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read4());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<Edge>());
		for (int j = 0; j < m; j++)
		{
			// cost に index
			var e = es[j];
			var edge = new Edge(e.a, e.b, j);
			map[e.a].Add(edge);
			map[e.b].Add(edge.Reverse());
		}

		var min_ts = es.Select(e => GetMinT(e.d)).ToArray();

		long GetCost(long j, long t0)
		{
			var t = Math.Max(t0, min_ts[j]);
			var (_, _, c, d) = es[j];
			return t - t0 + c + d / (t + 1);
		}

		var sv = 1;
		var ev = n;

		var costs = Array.ConvertAll(new bool[n + 1], _ => long.MaxValue);
		var q = PriorityQueue<int>.CreateWithKey(v => costs[v]);
		costs[sv] = 0;
		q.Push(sv);

		Edge[] Nexts(int v)
		{
			return Array.ConvertAll(map[v].ToArray(), e => new Edge(v, e.To, GetCost(e.Cost, costs[v])));
		}

		while (q.Any)
		{
			var (v, c) = q.Pop();
			if (v == ev) break;
			if (costs[v] < c) continue;

			foreach (var e in Nexts(v))
			{
				var (nv, nc) = (e.To, c + e.Cost);
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				q.Push(nv);
			}
		}

		if (costs[n] == long.MaxValue) return -1;
		return costs[n];
	}

	static int GetMinT(int d)
	{
		if (d == 0) return 0;

		var t0 = Math.Sqrt(d) - 1;
		var t1 = (int)Math.Floor(t0);
		var t2 = (int)Math.Ceiling(t0);

		if (t1 == t2) return t1;
		return d / (t1 + 1) <= 1 + d / (t2 + 1) ? t1 : t2;
	}
}

public struct Edge
{
	public static Edge Invalid { get; } = new Edge(-1, -1, long.MinValue);

	public int From { get; }
	public int To { get; }
	public long Cost { get; }

	public Edge(int from, int to, long cost = 1) { From = from; To = to; Cost = cost; }
	public void Deconstruct(out int from, out int to) { from = From; to = To; }
	public void Deconstruct(out int from, out int to, out long cost) { from = From; to = To; cost = Cost; }
	public override string ToString() => $"{From} {To} {Cost}";

	public static implicit operator Edge(int[] e) => new Edge(e[0], e[1], e.Length > 2 ? e[2] : 1);
	public static implicit operator Edge(long[] e) => new Edge((int)e[0], (int)e[1], e.Length > 2 ? e[2] : 1);
	public static implicit operator Edge((int from, int to) v) => new Edge(v.from, v.to);
	public static implicit operator Edge((int from, int to, long cost) v) => new Edge(v.from, v.to, v.cost);

	public Edge Reverse() => new Edge(To, From, Cost);
}

public class PriorityQueue<T>
{
	public static PriorityQueue<T> Create(bool descending = false)
	{
		var c = Comparer<T>.Default;
		return descending ?
			new PriorityQueue<T>((x, y) => c.Compare(y, x)) :
			new PriorityQueue<T>(c.Compare);
	}

	public static PriorityQueue<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false)
	{
		if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

		var c = Comparer<TKey>.Default;
		return descending ?
			new PriorityQueue<T>((x, y) => c.Compare(keySelector(y), keySelector(x))) :
			new PriorityQueue<T>((x, y) => c.Compare(keySelector(x), keySelector(y)));
	}

	public static PriorityQueue<T, TKey> CreateWithKey<TKey>(Func<T, TKey> keySelector, bool descending = false)
	{
		var c = Comparer<TKey>.Default;
		return descending ?
			new PriorityQueue<T, TKey>(keySelector, (x, y) => c.Compare(y.key, x.key)) :
			new PriorityQueue<T, TKey>(keySelector, (x, y) => c.Compare(x.key, y.key));
	}

	List<T> l = new List<T> { default };
	Comparison<T> c;

	public T First
	{
		get
		{
			if (l.Count <= 1) throw new InvalidOperationException("The heap is empty.");
			return l[1];
		}
	}

	public int Count => l.Count - 1;
	public bool Any => l.Count > 1;

	internal PriorityQueue(Comparison<T> comparison)
	{
		c = comparison ?? throw new ArgumentNullException(nameof(comparison));
	}

	// x の親: x/2
	// x の子: 2x, 2x+1
	void UpHeap(int i)
	{
		for (int j; (j = i >> 1) > 0 && c(l[j], l[i]) > 0; i = j)
			(l[i], l[j]) = (l[j], l[i]);
	}

	void DownHeap(int i)
	{
		for (int j; (j = i << 1) < l.Count; i = j)
		{
			if (j + 1 < l.Count && c(l[j], l[j + 1]) > 0) j++;
			if (c(l[i], l[j]) > 0) (l[i], l[j]) = (l[j], l[i]); else break;
		}
	}

	public void Push(T value)
	{
		l.Add(value);
		UpHeap(l.Count - 1);
	}

	public void PushRange(IEnumerable<T> values)
	{
		if (values != null) foreach (var v in values) Push(v);
	}

	public T Pop()
	{
		if (l.Count <= 1) throw new InvalidOperationException("The heap is empty.");

		var r = l[1];
		l[1] = l[l.Count - 1];
		l.RemoveAt(l.Count - 1);
		DownHeap(1);
		return r;
	}
}

// キーをキャッシュすることにより、キーが不変であることを保証します。
public class PriorityQueue<T, TKey> : PriorityQueue<(T value, TKey key)>
{
	Func<T, TKey> KeySelector;

	internal PriorityQueue(Func<T, TKey> keySelector, Comparison<(T value, TKey key)> comparison) : base(comparison)
	{
		KeySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
	}

	public void Push(T value)
	{
		Push((value, KeySelector(value)));
	}

	public void PushRange(IEnumerable<T> values)
	{
		if (values != null) foreach (var v in values) Push(v);
	}
}
