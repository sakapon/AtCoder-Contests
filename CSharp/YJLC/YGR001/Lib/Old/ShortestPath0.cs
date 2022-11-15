using System;
using System.Collections.Generic;
using System.Linq;

// 配列を用いた基本的な実装です。
public static class ShortestPath0
{
	public static (long[] minCosts, int[][] inEdges) Dijkstra(int vertexesCount, int[][] edges, bool directed, int startVertexId, int endVertexId = -1)
	{
		if (edges == null) throw new ArgumentNullException(nameof(edges));

		var map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int[]>());
		foreach (var e in edges)
		{
			// 入力チェックは省略。
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}

		var costs = Enumerable.Repeat(long.MaxValue, vertexesCount).ToArray();
		var inEdges = new int[vertexesCount][];
		var q = Heap.CreateWithKey<int, long>(v => costs[v]);
		costs[startVertexId] = 0;
		q.Push(startVertexId);

		while (q.Any)
		{
			var (v, c) = q.Pop();
			if (v == endVertexId) break;
			if (costs[v] < c) continue;

			foreach (var e in map[v])
			{
				var nc = costs[v] + e[2];
				if (costs[e[1]] <= nc) continue;
				costs[e[1]] = nc;
				inEdges[e[1]] = e;
				q.Push(e[1]);
			}
		}
		return (costs, inEdges);
	}

	public static int[] GetPathVertexes(int[][] inEdges, int endVertexId)
	{
		var path = new Stack<int>();
		path.Push(endVertexId);
		for (var e = inEdges[endVertexId]; e != null; e = inEdges[e[0]])
			path.Push(e[0]);
		return path.ToArray();
	}

	public static int[][] GetPathEdges(int[][] inEdges, int endVertexId)
	{
		var path = new Stack<int[]>();
		for (var e = inEdges[endVertexId]; e != null; e = inEdges[e[0]])
			path.Push(e);
		return path.ToArray();
	}
}
