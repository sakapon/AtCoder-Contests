using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Int
{
	// Edge with Id
	public struct UEdge
	{
		public static UEdge Invalid { get; } = new UEdge(-1, -1, -1);

		public int Id { get; }
		public int From { get; }
		public int To { get; }

		public UEdge(int id, int from, int to) { Id = id; From = from; To = to; }
		public void Deconstruct(out int from, out int to) { from = From; to = To; }
		public void Deconstruct(out int id, out int from, out int to) { id = Id; from = From; to = To; }
		public override string ToString() => $"{Id} {From} {To}";

		public UEdge Reverse() => new UEdge(Id, To, From);
	}

	public class UGraph
	{
		static readonly int[] EmptyEdges = new int[0];

		public int VertexesCount { get; }
		// Edge Id のリスト
		// map[v] が null である可能性があります。
		List<int>[] map;
		public List<int>[] Map => map;
		// 無向辺の場合、From と To は順不同です。
		public List<UEdge> Edges { get; }

		public UGraph(int n)
		{
			VertexesCount = n;
			map = new List<int>[n];
			Edges = new List<UEdge>();
		}

		public int[][] GetMapAsArray() => Array.ConvertAll(map, l => l?.ToArray() ?? EmptyEdges);
		public UEdge[] GetEdgesAsArray() => Edges.ToArray();

		public void AddEdge(int[] e, bool directed) => AddEdge(e[0], e[1], directed);
		public void AddEdge(int from, int to, bool directed)
		{
			Edges.Add(new UEdge(Edges.Count, from, to));

			var l = map[from] ?? (map[from] = new List<int>());
			l.Add(Edges.Count - 1);

			if (directed) return;
			l = map[to] ?? (map[to] = new List<int>());
			l.Add(Edges.Count - 1);
		}

		public void AddEdges(IEnumerable<int[]> es, bool directed)
		{
			foreach (var e in es) AddEdge(e[0], e[1], directed);
		}
	}
}
