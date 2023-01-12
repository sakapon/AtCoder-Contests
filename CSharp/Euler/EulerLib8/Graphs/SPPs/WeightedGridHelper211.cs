using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.Int.WeightedGraph211
{
	// undirected
	public class WeightedGridHelper
	{
		readonly int h, w;
		public int Height => h;
		public int Width => w;
		public WeightedGridHelper(int h, int w) { this.h = h; this.w = w; }

		public int ToVertexId(int i, int j) => w * i + j;
		public (int i, int j) FromVertexId(int v) => (v / w, v % w);

		public WeightedGraph GetWeightedAdjacencyList(int[][] s)
		{
			var graph = new WeightedGraph(h * w);
			for (int i = 0; i < h; ++i)
				for (int j = 1; j < w; ++j)
				{
					var v = w * i + j;
					graph.AddEdge(v, v - 1, false, s[i][j - 1]);
					graph.AddEdge(v - 1, v, false, s[i][j]);
				}
			for (int j = 0; j < w; ++j)
				for (int i = 1; i < h; ++i)
				{
					var v = w * i + j;
					graph.AddEdge(v, v - w, false, s[i - 1][j]);
					graph.AddEdge(v - w, v, false, s[i][j]);
				}
			return graph;
		}
	}
}
