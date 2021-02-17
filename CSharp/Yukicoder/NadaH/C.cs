using System;
using System.Collections.Generic;
using Bang.Graphs.Int.Spp;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main()
	{
		var (n, m, s, t) = Read4();
		var p = Read();
		var map = GraphConsole.ReadUnweightedMap(n + 1, m, false);

		int x = p[s - 1], y = 0;
		var u = new bool[n + 1];
		var q = PriorityQueue<int>.CreateWithKey(v => p[v - 1], true);
		u[s] = true;
		q.Push(s);

		while (q.Any)
		{
			var (v, pv) = q.Pop();
			if (x > pv)
			{
				x = pv;
				y++;
			}

			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				u[nv] = true;
				q.Push(nv);
			}
		}
		Console.WriteLine(y);
	}
}

namespace Bang.Graphs.Int.Spp
{
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

	public static class GraphConsole
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		public static Edge[] ReadEdges(int count)
		{
			return Array.ConvertAll(new bool[count], _ => (Edge)Read());
		}

		public static UnweightedMap ReadUnweightedMap(int vertexesCount, int edgesCount, bool directed)
		{
			return new UnweightedMap(vertexesCount, ReadEdges(edgesCount), directed);
		}
	}

	public static class GraphConvert
	{
		public static void UnweightedEdgesToMap(List<int>[] map, Edge[] edges, bool directed)
		{
			foreach (var e in edges)
			{
				map[e.From].Add(e.To);
				if (!directed) map[e.To].Add(e.From);
			}
		}

		public static void UnweightedEdgesToMap(List<int>[] map, int[][] edges, bool directed)
		{
			foreach (var e in edges)
			{
				map[e[0]].Add(e[1]);
				if (!directed) map[e[1]].Add(e[0]);
			}
		}

		public static void WeightedEdgesToMap(List<Edge>[] map, Edge[] edges, bool directed)
		{
			foreach (var e in edges)
			{
				map[e.From].Add(e);
				if (!directed) map[e.To].Add(e.Reverse());
			}
		}

		public static void WeightedEdgesToMap(List<Edge>[] map, int[][] edges, bool directed)
		{
			foreach (var e0 in edges)
			{
				Edge e = e0;
				map[e.From].Add(e);
				if (!directed) map[e.To].Add(e.Reverse());
			}
		}

		public static List<int>[] UnweightedEdgesToMap(int vertexesCount, Edge[] edges, bool directed)
		{
			var map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int>());
			UnweightedEdgesToMap(map, edges, directed);
			return map;
		}

		public static List<int>[] UnweightedEdgesToMap(int vertexesCount, int[][] edges, bool directed)
		{
			var map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int>());
			UnweightedEdgesToMap(map, edges, directed);
			return map;
		}

		public static List<Edge>[] WeightedEdgesToMap(int vertexesCount, Edge[] edges, bool directed)
		{
			var map = Array.ConvertAll(new bool[vertexesCount], _ => new List<Edge>());
			WeightedEdgesToMap(map, edges, directed);
			return map;
		}

		public static List<Edge>[] WeightedEdgesToMap(int vertexesCount, int[][] edges, bool directed)
		{
			var map = Array.ConvertAll(new bool[vertexesCount], _ => new List<Edge>());
			WeightedEdgesToMap(map, edges, directed);
			return map;
		}
	}

	public class UnweightedMap
	{
		public int VertexesCount { get; }
		List<int>[] map;
		public List<int>[] RawMap => map;
		public int[] this[int vertex] => map[vertex].ToArray();

		public UnweightedMap(int vertexesCount, List<int>[] map)
		{
			VertexesCount = vertexesCount;
			this.map = map;
		}

		public UnweightedMap(int vertexesCount)
		{
			VertexesCount = vertexesCount;
			map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int>());
		}

		public UnweightedMap(int vertexesCount, Edge[] edges, bool directed) : this(vertexesCount)
		{
			AddEdges(edges, directed);
		}

		public UnweightedMap(int vertexesCount, int[][] edges, bool directed) : this(vertexesCount)
		{
			AddEdges(edges, directed);
		}

		public void AddEdges(Edge[] edges, bool directed)
		{
			GraphConvert.UnweightedEdgesToMap(map, edges, directed);
		}

		public void AddEdges(int[][] edges, bool directed)
		{
			GraphConvert.UnweightedEdgesToMap(map, edges, directed);
		}

		public void AddEdge(Edge edge, bool directed)
		{
			map[edge.From].Add(edge.To);
			if (!directed) map[edge.To].Add(edge.From);
		}

		public void AddEdge(int from, int to, bool directed)
		{
			map[from].Add(to);
			if (!directed) map[to].Add(from);
		}
	}

	/// <summary>
	/// 優先度付きキューを表します。
	/// </summary>
	/// <typeparam name="T">オブジェクトの型。</typeparam>
	/// <remarks>
	/// 二分ヒープによる実装です。<br/>
	/// 内部では 1-indexed のため、raw array を直接ソートする用途では使われません。
	/// </remarks>
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
}
