using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Graphs.Int
{
	public static class ShortestPath
	{
		public struct Edge
		{
			public static Edge Invalid = new Edge(-1, -1, long.MinValue);

			public int From, To;
			public long Weight;
			public Edge(int from, int to, long weight) => (From, To, Weight) = (from, to, weight);
			public Edge(int[] e) : this(e[0], e[1], e[2]) { }
			public Edge(long[] e) : this((int)e[0], (int)e[1], e[2]) { }
			public Edge GetReverse() => new Edge(To, From, Weight);
		}

		public class Result
		{
			public List<Edge>[] Map;
			public int StartVertex, EndVertex;
			public long[] MinCosts;
			public Edge[] InEdges;
			internal Result(List<Edge>[] map, int sv, int ev, long[] minCosts, Edge[] inEdges) => (Map, StartVertex, EndVertex, MinCosts, InEdges) = (map, sv, ev, minCosts, inEdges);

			public int[] GetPathVertexes() => GetPathVertexes(EndVertex);
			public int[] GetPathVertexes(int ev)
			{
				var path = new Stack<int>();
				for (var v = ev; v != -1; v = InEdges[v].From)
					path.Push(v);
				return path.ToArray();
			}

			public Edge[] GetPathEdges() => GetPathEdges(EndVertex);
			public Edge[] GetPathEdges(int ev)
			{
				var path = new Stack<Edge>();
				for (var e = InEdges[ev]; e.From != -1; e = InEdges[e.From])
					path.Push(e);
				return path.ToArray();
			}
		}

		// n: 頂点の個数
		public static List<Edge>[] ToMap(int n, int[][] es, bool directed) => ToMap(n, Array.ConvertAll(es, e => new Edge(e)), directed);
		public static List<Edge>[] ToMap(int n, Edge[] es, bool directed)
		{
			var map = Array.ConvertAll(new bool[n + 1], _ => new List<Edge>());
			foreach (var e in es)
			{
				map[e.From].Add(e);
				if (!directed) map[e.To].Add(e.GetReverse());
			}
			return map;
		}

		public static Edge[] ToEdges(List<Edge>[] map, bool directed) => directed ?
			map.SelectMany(es => es).ToArray() :
			map.SelectMany(es => es).Where(e => e.From < e.To).ToArray();

		// 内部では n+1 個の領域を確保します。使われない (非連結の) 頂点が存在してもかまいません。
		public static Result Dijkstra(int n, int[][] es, bool directed, int sv, int ev = -1) => Dijkstra(n, ToMap(n, es, directed), directed, sv, ev);
		public static Result Dijkstra(int n, List<Edge>[] map, bool directed, int sv, int ev = -1)
		{
			throw new NotImplementedException();
		}
	}
}
