using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int u, int v) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var h = Read();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var spp = new SppWeightedGraph(n + 1);

		foreach (var (u, v) in es)
		{
			var hu = h[u - 1];
			var hv = h[v - 1];

			spp.AddEdge(u, v, hu < hv ? hv - hu : 0, true);
			spp.AddEdge(v, u, hv < hu ? hu - hv : 0, true);
		}

		var d = spp.Dijkstra(1, -1);
		return Enumerable.Range(1, n).Max(v => h[0] - h[v - 1] - d[v]);
	}
}

public class SppWeightedGraph
{
	public struct Edge
	{
		public static readonly Edge Invalid = new Edge(-1, -1, long.MinValue);

		public int From, To;
		public long Cost;
		public Edge(int from, int to, long cost) { From = from; To = to; Cost = cost; }
		public Edge(int[] e) : this(e[0], e[1], e[2]) { }
		public Edge(long[] e) : this((int)e[0], (int)e[1], e[2]) { }
		public Edge GetReverse() => new Edge(To, From, Cost);
	}

	public int VertexesCount { get; }
	public List<Edge>[] Map;

	public SppWeightedGraph(int n)
	{
		VertexesCount = n;
		Map = Array.ConvertAll(new bool[n], _ => new List<Edge>());
	}

	public void AddEdge(int from, int to, long cost, bool directed)
	{
		Map[from].Add(new Edge(from, to, cost));
		if (!directed) Map[to].Add(new Edge(to, from, cost));
	}

	public long[] Dijkstra(int sv, int ev = -1) => Dijkstra(VertexesCount, v => Map[v].ToArray(), sv, ev);

	// 終点を指定しないときは、-1 を指定します。
	public static long[] Dijkstra(int n, Func<int, Edge[]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = PriorityQueue<int>.CreateWithKey(v => costs[v]);
		costs[sv] = 0;
		q.Push(sv);

		while (q.Any)
		{
			var (v, c) = q.Pop();
			if (v == ev) break;
			if (costs[v] < c) continue;

			foreach (var e in nexts(v))
			{
				var (nv, nc) = (e.To, c + e.Cost);
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				q.Push(nv);
			}
		}
		return costs;
	}
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
