using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Arrays
{
	public static class DirectedGraphHelper
	{
		// 連結性や重みは問いません。
		// 閉路があるとき、null。
		// DAG であるとき、ソートされた頂点集合。
		public static int[] TopologicalSort(int n, int[][] des)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			var indeg = new int[n];
			foreach (var e in des)
			{
				map[e[0]].Add(e);
				++indeg[e[1]];
			}

			var r = new List<int>();
			var q = new Queue<int>();
			for (int v = 0; v < n; ++v)
			{
				if (indeg[v] == 0)
				{
					r.Add(v);
					q.Enqueue(v);
				}
			}

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				foreach (var e in map[v])
				{
					if (--indeg[e[1]] > 0) continue;
					r.Add(e[1]);
					q.Enqueue(e[1]);
				}
			}

			if (r.Count < n) return null;
			return r.ToArray();
		}
	}
}
