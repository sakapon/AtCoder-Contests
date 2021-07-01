using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Int
{
	public class BipartiteMatching
	{
		int n1, n2;
		List<int>[] map;
		int[] match;
		bool[] u;

		// 0 <= v1 < n1, 0 <= v2 < n2
		public BipartiteMatching(int n1, int n2)
		{
			this.n1 = n1;
			this.n2 = n2;
			var n = n1 + n2;
			map = Array.ConvertAll(new bool[n], _ => new List<int>());
			match = new int[n];
			u = new bool[n];
		}

		public void AddEdge(int from, int to)
		{
			map[from].Add(n1 + to);
			map[n1 + to].Add(from);
		}

		// { from, to }
		public void AddEdges(int[][] des)
		{
			foreach (var e in des) AddEdge(e[0], e[1]);
		}

		bool Dfs(int v1)
		{
			u[v1] = true;
			foreach (var u2 in map[v1])
			{
				var u1 = match[u2];
				if (u1 == -1 || !u[u1] && Dfs(u1))
				{
					match[v1] = u2;
					match[u2] = v1;
					return true;
				}
			}
			return false;
		}

		public int[][] Dinic()
		{
			match = Array.ConvertAll(map, _ => -1);

			for (int v1 = 0; v1 < n1; ++v1)
			{
				if (match[v1] != -1) continue;
				Array.Clear(u, 0, u.Length);
				Dfs(v1);
			}

			var r = new List<int[]>();
			for (int v1 = 0; v1 < n1; ++v1)
				if (match[v1] != -1)
					r.Add(new[] { v1, match[v1] - n1 });
			return r.ToArray();
		}
	}
}
