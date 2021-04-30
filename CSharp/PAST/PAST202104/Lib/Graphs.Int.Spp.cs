using System;
using System.Collections.Generic;

namespace Bang.Graphs.Int.Spp
{
	public struct Point : IEquatable<Point>
	{
		public int i, j;
		public Point(int i, int j) { this.i = i; this.j = j; }
		public void Deconstruct(out int i, out int j) { i = this.i; j = this.j; }
		public override string ToString() => $"{i} {j}";
		public static Point Parse(string s) => Array.ConvertAll(s.Split(), int.Parse);

		public static implicit operator Point(int[] v) => (v[0], v[1]);
		public static explicit operator int[](Point v) => new[] { v.i, v.j };
		public static implicit operator Point((int i, int j) v) => new Point(v.i, v.j);
		public static explicit operator (int, int)(Point v) => (v.i, v.j);

		public bool Equals(Point other) => i == other.i && j == other.j;
		public static bool operator ==(Point v1, Point v2) => v1.Equals(v2);
		public static bool operator !=(Point v1, Point v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is Point v && Equals(v);
		public override int GetHashCode() => (i, j).GetHashCode();

		public static Point operator -(Point v) => new Point(-v.i, -v.j);
		public static Point operator +(Point v1, Point v2) => new Point(v1.i + v2.i, v1.j + v2.j);
		public static Point operator -(Point v1, Point v2) => new Point(v1.i - v2.i, v1.j - v2.j);

		public bool IsInRange(int height, int width) => 0 <= i && i < height && 0 <= j && j < width;
		public Point[] Nexts() => new[] { new Point(i - 1, j), new Point(i + 1, j), new Point(i, j - 1), new Point(i, j + 1) };
		public static Point[] NextsByDelta { get; } = new[] { new Point(-1, 0), new Point(1, 0), new Point(0, -1), new Point(0, 1) };

		public int NormL1 => Math.Abs(i) + Math.Abs(j);
		public double Norm => Math.Sqrt(i * i + j * j);
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

	public class CostResult
	{
		protected const int InvalidVertex = -1;

		public long[] RawCosts { get; }
		public CostResult(long[] costs) { RawCosts = costs; }

		public long this[int vertex] => RawCosts[vertex];
		public bool IsConnected(int vertex) => RawCosts[vertex] != long.MaxValue;
		public long GetCost(int vertex, long invalid = -1) => IsConnected(vertex) ? RawCosts[vertex] : invalid;
	}

	public class UnweightedResult : CostResult
	{
		public int[] RawInVertexes { get; }

		public UnweightedResult(long[] costs, int[] inVertexes) : base(costs)
		{
			RawInVertexes = inVertexes;
		}

		public int[] GetPathVertexes(int endVertex)
		{
			var path = new Stack<int>();
			for (var v = endVertex; v != InvalidVertex; v = RawInVertexes[v])
				path.Push(v);
			return path.ToArray();
		}

		public Edge[] GetPathEdges(int endVertex)
		{
			var path = new Stack<Edge>();
			for (var v = endVertex; RawInVertexes[v] != InvalidVertex; v = RawInVertexes[v])
				path.Push(new Edge(RawInVertexes[v], v));
			return path.ToArray();
		}
	}

	public class WeightedResult : CostResult
	{
		public Edge[] RawInEdges { get; }

		public WeightedResult(long[] costs, Edge[] inEdges) : base(costs)
		{
			RawInEdges = inEdges;
		}

		public int[] GetPathVertexes(int endVertex)
		{
			var path = new Stack<int>();
			for (var v = endVertex; v != InvalidVertex; v = RawInEdges[v].From)
				path.Push(v);
			return path.ToArray();
		}

		public Edge[] GetPathEdges(int endVertex)
		{
			var path = new Stack<Edge>();
			for (var e = RawInEdges[endVertex]; e.From != InvalidVertex; e = RawInEdges[e.From])
				path.Push(e);
			return path.ToArray();
		}
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

		public static WeightedMap ReadWeightedMap(int vertexesCount, int edgesCount, bool directed)
		{
			return new WeightedMap(vertexesCount, ReadEdges(edgesCount), directed);
		}

		public static Point ReadPoint()
		{
			return Point.Parse(Console.ReadLine());
		}

		public static string[] ReadGrid(int height)
		{
			return Array.ConvertAll(new bool[height], _ => Console.ReadLine());
		}

		public static char[][] ReadGridAsChar(int height)
		{
			return Array.ConvertAll(new bool[height], _ => Console.ReadLine().ToCharArray());
		}

		public static int[][] ReadGridAsInt(int height)
		{
			return Array.ConvertAll(new bool[height], _ => Read());
		}

		public static string[] ReadEnclosedGrid(ref int height, ref int width, char c = '#', int delta = 1)
		{
			var h = height + 2 * delta;
			var w = width + 2 * delta;
			var cw = new string(c, w);
			var cd = new string(c, delta);

			var s = new string[h];
			for (int i = 0; i < delta; ++i) s[delta + height + i] = s[i] = cw;
			for (int i = 0; i < height; ++i) s[delta + i] = cd + Console.ReadLine() + cd;
			(height, width) = (h, w);
			return s;
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

	public static class GridHelper
	{
		public static T GetValue<T>(this T[,] a, Point p) => a[p.i, p.j];
		public static void SetValue<T>(this T[,] a, Point p, T value) => a[p.i, p.j] = value;
		public static T GetValue<T>(this T[][] a, Point p) => a[p.i][p.j];
		public static void SetValue<T>(this T[][] a, Point p, T value) => a[p.i][p.j] = value;
		public static char GetValue(this string[] s, Point p) => s[p.i][p.j];

		public static Point[] GetPoints(int height, int width)
		{
			var ps = new List<Point>();
			for (int i = 0, j = 0; i < height;)
			{
				ps.Add(new Point(i, j));
				if (++j == width) { ++i; j = 0; }
			}
			return ps.ToArray();
		}

		public static Point FindValue<T>(T[][] a, T value)
		{
			var ec = EqualityComparer<T>.Default;
			var (h, w) = (a.Length, a[0].Length);
			for (int i = 0; i < h; ++i)
				for (int j = 0; j < w; ++j)
					if (ec.Equals(a[i][j], value)) return new Point(i, j);
			return new Point(-1, -1);
		}

		public static Point FindValue(string[] s, char c)
		{
			var (h, w) = (s.Length, s[0].Length);
			for (int i = 0; i < h; ++i)
				for (int j = 0; j < w; ++j)
					if (s[i][j] == c) return new Point(i, j);
			return new Point(-1, -1);
		}

		public static int ToHash(Point p, int width) => p.i * width + p.j;
		public static Point FromHash(int hash, int width) => new Point(hash / width, hash % width);
		public static Func<Point, int> CreateToHash(int width) => p => p.i * width + p.j;
		public static Func<int, Point> CreateFromHash(int width) => hash => new Point(hash / width, hash % width);

		public static int[] Nexts(int hash, int width) => new[] { hash - width, hash + width, hash - 1, hash + 1 };
		public static Func<int, int[]> CreateNexts(int width) => hash => Nexts(hash, width);

		//int ToId(Point p) => p.i * w + p.j;
		//Point FromId(int id) => new Point(id / w, id % w);
		//int[] Nexts(int id) => new[] { id + 1, id - 1, id + w, id - w };

		// 負値を指定できます。
		public static void Enclose<T>(ref int height, ref int width, ref T[][] a, T value, int delta = 1)
		{
			var (h, w) = (height + 2 * delta, width + 2 * delta);
			var (li, ri) = (Math.Max(0, -delta), Math.Min(height, height + delta));
			var (lj, rj) = (Math.Max(0, -delta), Math.Min(width, width + delta));

			var t = Array.ConvertAll(new bool[h], _ => Array.ConvertAll(new bool[w], __ => value));
			for (int i = li; i < ri; ++i)
				for (int j = lj; j < rj; ++j)
					t[delta + i][delta + j] = a[i][j];
			(height, width, a) = (h, w, t);
		}

		// 負値を指定できます。
		public static void Enclose(ref int height, ref int width, ref string[] s, char c = '#', int delta = 1)
		{
			var (h, w) = (height + 2 * delta, width + 2 * delta);
			var (li, ri) = (Math.Max(0, -delta), Math.Min(height, height + delta));
			var cw = new string(c, w);
			var cd = new string(c, Math.Max(0, delta));

			var t = new string[h];
			for (int i = 0; i < delta; ++i) t[delta + height + i] = t[i] = cw;
			for (int i = li; i < ri; ++i) t[delta + i] = delta >= 0 ? cd + s[i] + cd : s[i][-delta..(width + delta)];
			(height, width, s) = (h, w, t);
		}

		public static T[][] Rotate180<T>(T[][] a)
		{
			var (h, w) = (a.Length, a[0].Length);
			var r = Array.ConvertAll(new bool[h], _ => new T[w]);
			for (int i = 0; i < h; ++i)
				for (int j = 0; j < w; ++j)
					r[i][j] = a[h - 1 - i][w - 1 - j];
			return r;
		}

		public static T[][] RotateLeft<T>(T[][] a)
		{
			var (h, w) = (a.Length, a[0].Length);
			var r = Array.ConvertAll(new bool[w], _ => new T[h]);
			for (int i = 0; i < w; ++i)
				for (int j = 0; j < h; ++j)
					r[i][j] = a[j][w - 1 - i];
			return r;
		}

		public static T[][] RotateRight<T>(T[][] a)
		{
			var (h, w) = (a.Length, a[0].Length);
			var r = Array.ConvertAll(new bool[w], _ => new T[h]);
			for (int i = 0; i < w; ++i)
				for (int j = 0; j < h; ++j)
					r[i][j] = a[h - 1 - j][i];
			return r;
		}

		public static string[] Rotate180(string[] s)
		{
			var h = s.Length;
			var r = new string[h];
			for (int i = 0; i < h; ++i)
			{
				var cs = s[h - 1 - i].ToCharArray();
				Array.Reverse(cs);
				r[i] = new string(cs);
			}
			return r;
		}

		public static string[] RotateLeft(string[] s)
		{
			var (h, w) = (s.Length, s[0].Length);
			var r = new string[w];
			for (int i = 0; i < w; ++i)
			{
				var cs = new char[h];
				for (int j = 0; j < h; ++j)
					cs[j] = s[j][w - 1 - i];
				r[i] = new string(cs);
			}
			return r;
		}

		public static string[] RotateRight(string[] s)
		{
			var (h, w) = (s.Length, s[0].Length);
			var r = new string[w];
			for (int i = 0; i < w; ++i)
			{
				var cs = new char[h];
				for (int j = 0; j < h; ++j)
					cs[j] = s[h - 1 - j][i];
				r[i] = new string(cs);
			}
			return r;
		}
	}

	/// <summary>
	/// 最短経路アルゴリズムの核となる機能を提供します。
	/// ここでは整数型の ID を使用します。
	/// </summary>
	public static class ShortestPathCore
	{
		public static bool[] Dfs(int vertexesCount, Func<int, int[]> getNextVertexes, int startVertex, int endVertex = -1)
		{
			var u = new bool[vertexesCount];
			var q = new Stack<int>();
			u[startVertex] = true;
			q.Push(startVertex);

			while (q.Count > 0)
			{
				var v = q.Pop();

				foreach (var nv in getNextVertexes(v))
				{
					if (u[nv]) continue;
					u[nv] = true;
					if (nv == endVertex) return u;
					q.Push(nv);
				}
			}
			return u;
		}

		/// <summary>
		/// 幅優先探索により、始点から各頂点への最短経路を求めます。<br/>
		/// 辺のコストはすべて 1 として扱われます。
		/// </summary>
		/// <param name="vertexesCount">頂点の個数。これ未満の値を ID として使用できます。</param>
		/// <param name="getNextVertexes">指定された頂点からの行先を取得するための関数。</param>
		/// <param name="startVertex">始点。</param>
		/// <param name="endVertex">終点。終点を指定しない場合、-1。</param>
		/// <returns>探索結果を表す <see cref="UnweightedResult"/> オブジェクト。</returns>
		/// <remarks>
		/// グラフの有向性、連結性、多重性、開閉を問いません。
		/// </remarks>
		public static UnweightedResult Bfs(int vertexesCount, Func<int, int[]> getNextVertexes, int startVertex, int endVertex = -1)
		{
			var costs = Array.ConvertAll(new bool[vertexesCount], _ => long.MaxValue);
			var inVertexes = Array.ConvertAll(costs, _ => -1);
			var q = new Queue<int>();
			costs[startVertex] = 0;
			q.Enqueue(startVertex);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var nc = costs[v] + 1;

				// IEnumerable<T>, List<T>, T[] の順に高速になります。
				foreach (var nv in getNextVertexes(v))
				{
					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					inVertexes[nv] = v;
					if (nv == endVertex) return new UnweightedResult(costs, inVertexes);
					q.Enqueue(nv);
				}
			}
			return new UnweightedResult(costs, inVertexes);
		}

		/// <summary>
		/// Dijkstra 法により、始点から各頂点への最短経路を求めます。<br/>
		/// 辺のコストは非負でなければなりません。
		/// </summary>
		/// <param name="vertexesCount">頂点の個数。これ未満の値を ID として使用できます。</param>
		/// <param name="getNextEdges">指定された頂点からの出辺を取得するための関数。</param>
		/// <param name="startVertex">始点。</param>
		/// <param name="endVertex">終点。終点を指定しない場合、-1。</param>
		/// <returns>探索結果を表す <see cref="WeightedResult"/> オブジェクト。</returns>
		/// <remarks>
		/// グラフの有向性、連結性、多重性、開閉を問いません。
		/// </remarks>
		public static WeightedResult Dijkstra(int vertexesCount, Func<int, Edge[]> getNextEdges, int startVertex, int endVertex = -1)
		{
			var costs = Array.ConvertAll(new bool[vertexesCount], _ => long.MaxValue);
			var inEdges = Array.ConvertAll(costs, _ => Edge.Invalid);
			var q = PriorityQueue<int>.CreateWithKey(v => costs[v]);
			costs[startVertex] = 0;
			q.Push(startVertex);

			while (q.Any)
			{
				var (v, c) = q.Pop();
				if (v == endVertex) break;
				if (costs[v] < c) continue;

				// IEnumerable<T>, List<T>, T[] の順に高速になります。
				foreach (var e in getNextEdges(v))
				{
					var (nv, nc) = (e.To, c + e.Cost);
					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					inEdges[nv] = e;
					q.Push(nv);
				}
			}
			return new WeightedResult(costs, inEdges);
		}

		/// <summary>
		/// 幅優先探索の拡張により、始点から各頂点への最短経路を求めます。<br/>
		/// 例えば <paramref name="m"/> = 3 のとき、012-BFS を表します。<br/>
		/// 辺のコストの範囲は [0, <paramref name="m"/>) です。
		/// </summary>
		/// <param name="m">辺のコストの候補となる数。</param>
		/// <param name="vertexesCount">頂点の個数。これ未満の値を ID として使用できます。</param>
		/// <param name="getNextEdges">指定された頂点からの出辺を取得するための関数。</param>
		/// <param name="startVertex">始点。</param>
		/// <param name="endVertex">終点。終点を指定しない場合、-1。</param>
		/// <returns>探索結果を表す <see cref="WeightedResult"/> オブジェクト。</returns>
		/// <remarks>
		/// グラフの有向性、連結性、多重性、開閉を問いません。
		/// </remarks>
		public static WeightedResult BfsMod(int m, int vertexesCount, Func<int, Edge[]> getNextEdges, int startVertex, int endVertex = -1)
		{
			var costs = Array.ConvertAll(new bool[vertexesCount], _ => long.MaxValue);
			var inEdges = Array.ConvertAll(costs, _ => Edge.Invalid);
			var qs = Array.ConvertAll(new bool[m], _ => new Queue<int>());
			costs[startVertex] = 0;
			qs[0].Enqueue(startVertex);

			for (long c = 0; Array.Exists(qs, q => q.Count > 0); ++c)
			{
				var q = qs[c % m];
				while (q.Count > 0)
				{
					var v = q.Dequeue();
					if (v == endVertex) return new WeightedResult(costs, inEdges);
					if (costs[v] < c) continue;

					foreach (var e in getNextEdges(v))
					{
						var (nv, nc) = (e.To, c + e.Cost);
						if (costs[nv] <= nc) continue;
						costs[nv] = nc;
						inEdges[nv] = e;
						qs[nc % m].Enqueue(nv);
					}
				}
			}
			return new WeightedResult(costs, inEdges);
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

		public UnweightedResult Bfs(int startVertex, int endVertex = -1)
		{
			return ShortestPathCore.Bfs(VertexesCount, v => this[v], startVertex, endVertex);
		}
	}

	public class WeightedMap
	{
		public int VertexesCount { get; }
		List<Edge>[] map;
		public List<Edge>[] RawMap => map;
		public Edge[] this[int vertex] => map[vertex].ToArray();

		public WeightedMap(int vertexesCount, List<Edge>[] map)
		{
			VertexesCount = vertexesCount;
			this.map = map;
		}

		public WeightedMap(int vertexesCount)
		{
			VertexesCount = vertexesCount;
			map = Array.ConvertAll(new bool[vertexesCount], _ => new List<Edge>());
		}

		public WeightedMap(int vertexesCount, Edge[] edges, bool directed) : this(vertexesCount)
		{
			AddEdges(edges, directed);
		}

		public WeightedMap(int vertexesCount, int[][] edges, bool directed) : this(vertexesCount)
		{
			AddEdges(edges, directed);
		}

		public void AddEdges(Edge[] edges, bool directed)
		{
			GraphConvert.WeightedEdgesToMap(map, edges, directed);
		}

		public void AddEdges(int[][] edges, bool directed)
		{
			GraphConvert.WeightedEdgesToMap(map, edges, directed);
		}

		public void AddEdge(Edge edge, bool directed)
		{
			map[edge.From].Add(edge);
			if (!directed) map[edge.To].Add(edge.Reverse());
		}

		public void AddEdge(int from, int to, long cost, bool directed)
		{
			map[from].Add(new Edge(from, to, cost));
			if (!directed) map[to].Add(new Edge(to, from, cost));
		}

		public WeightedResult Dijkstra(int startVertex, int endVertex = -1)
		{
			return ShortestPathCore.Dijkstra(VertexesCount, v => this[v], startVertex, endVertex);
		}

		public WeightedResult BfsMod(int m, int startVertex, int endVertex = -1)
		{
			return ShortestPathCore.BfsMod(m, VertexesCount, v => this[v], startVertex, endVertex);
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
