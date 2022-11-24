using System;
using System.Collections.Generic;

// GetEdges を抽象化します。
// 頂点 id は整数とし、グリッドに拡張します。
namespace CoderLib8.Graphs.SPPs.Int.UnweightedGraph411
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: Cost = {Cost}\}")]
	public class Vertex
	{
		public int Id { get; }
		public long Cost { get; set; } = long.MaxValue;
		public bool IsConnected => Cost != long.MaxValue;
		public Vertex Previous { get; set; }
		public Vertex(int id) { Id = id; }
		public void ClearResult()
		{
			Cost = long.MaxValue;
			Previous = null;
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public abstract class UnweightedGraphBase
	{
		public Vertex[] Vertexes { get; }
		public int VertexesCount => Vertexes.Length;
		public Vertex this[int v] => Vertexes[v];
		public abstract List<int> GetEdges(int v);

		protected UnweightedGraphBase(int vertexesCount)
		{
			Vertexes = new Vertex[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new Vertex(v);
		}

		// 最短経路とは限りません。
		// 連結性のみを判定する場合は、DFS または Union-Find を利用します。
		public Vertex ConnectivityByDFS(int svid, int evid = -1)
		{
			Vertexes[svid].Cost = 0;
			var q = new Stack<int>();
			q.Push(svid);

			while (q.Count > 0)
			{
				var vid = q.Pop();
				if (vid == evid) return Vertexes[evid];
				var v = Vertexes[vid];

				foreach (var nvid in GetEdges(vid))
				{
					var nv = Vertexes[nvid];
					if (nv.Cost == 0) continue;
					nv.Cost = 0;
					nv.Previous = v;
					q.Push(nvid);
				}
			}
			return null;
		}

		public Vertex ShortestByBFS(int svid, int evid = -1)
		{
			Vertexes[svid].Cost = 0;
			var q = new Queue<int>();
			q.Enqueue(svid);

			while (q.Count > 0)
			{
				var vid = q.Dequeue();
				if (vid == evid) return Vertexes[evid];
				var v = Vertexes[vid];
				var nc = v.Cost + 1;

				foreach (var nvid in GetEdges(vid))
				{
					var nv = Vertexes[nvid];
					if (nv.Cost <= nc) continue;
					nv.Cost = nc;
					nv.Previous = v;
					q.Enqueue(nvid);
				}
			}
			return null;
		}

		public int[] GetPathVertexes(int ev)
		{
			var path = new Stack<int>();
			for (var v = Vertexes[ev]; v != null; v = v.Previous)
				path.Push(v.Id);
			return path.ToArray();
		}

		public (int, int)[] GetPathEdges(int ev)
		{
			var path = new Stack<(int, int)>();
			for (var v = Vertexes[ev]; v.Previous != null; v = v.Previous)
				path.Push((v.Previous.Id, v.Id));
			return path.ToArray();
		}
	}

	public class IntUnweightedGraph : UnweightedGraphBase
	{
		readonly List<int>[] map;
		public List<int>[] Map => map;
		public override List<int> GetEdges(int v) => map[v];

		public IntUnweightedGraph(int vertexesCount) : base(vertexesCount)
		{
			map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int>());
		}
		public IntUnweightedGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public void AddEdge(int from, int to, bool twoWay)
		{
			map[from].Add(to);
			if (twoWay) map[to].Add(from);
		}

		public void ClearResult()
		{
			foreach (var v in Vertexes) v.ClearResult();
		}
	}
}
