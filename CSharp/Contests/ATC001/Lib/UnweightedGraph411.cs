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
		public Vertex ConnectivityByDFS(int sv, int ev = -1)
		{
			var evo = ev != -1 ? Vertexes[ev] : null;

			Vertexes[sv].Cost = 0;
			var q = new Stack<int>();
			q.Push(sv);

			while (q.Count > 0)
			{
				var v = q.Pop();
				if (v == ev) return evo;
				var vo = Vertexes[v];

				foreach (var nv in GetEdges(v))
				{
					var nvo = Vertexes[nv];
					if (nvo.Cost == 0) continue;
					nvo.Cost = 0;
					nvo.Previous = vo;
					q.Push(nv);
				}
			}
			return evo;
		}

		public Vertex ShortestByBFS(int sv, int ev = -1)
		{
			var evo = ev != -1 ? Vertexes[ev] : null;

			Vertexes[sv].Cost = 0;
			var q = new Queue<int>();
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				if (v == ev) return evo;
				var vo = Vertexes[v];
				var nc = vo.Cost + 1;

				foreach (var nv in GetEdges(v))
				{
					var nvo = Vertexes[nv];
					if (nvo.Cost <= nc) continue;
					nvo.Cost = nc;
					nvo.Previous = vo;
					q.Enqueue(nv);
				}
			}
			return evo;
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

	public class UnweightedGraph : UnweightedGraphBase
	{
		protected readonly List<int>[] map;
		public List<int>[] Map => map;
		public override List<int> GetEdges(int v) => map[v];

		public UnweightedGraph(int vertexesCount) : base(vertexesCount)
		{
			map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int>());
		}
		public UnweightedGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay) : this(vertexesCount)
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

	public class UnweightedGrid : UnweightedGraphBase
	{
		protected readonly int h, w;
		public int Height => h;
		public int Width => w;
		public UnweightedGrid(int h, int w) : base(h * w) { this.h = h; this.w = w; }

		public Vertex this[int i, int j] => Vertexes[w * i + j];
		public int ToVertexId(int i, int j) => w * i + j;
		public (int i, int j) FromVertexId(int v) => (v / w, v % w);

		public static (int, int)[] NextsDelta { get; } = new[] { (0, -1), (0, 1), (-1, 0), (1, 0) };
		public static (int, int)[] NextsDelta8 { get; } = new[] { (0, -1), (0, 1), (-1, 0), (1, 0), (-1, -1), (-1, 1), (1, -1), (1, 1) };

		public override List<int> GetEdges(int v) => GetAllNexts(v);
		public List<int> GetAllNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int>();
			foreach (var (di, dj) in NextsDelta)
			{
				var (ni, nj) = (i + di, j + dj);
				if (0 <= ni && ni < h && 0 <= nj && nj < w) l.Add(w * ni + nj);
			}
			return l;
		}
	}

	public class CharUnweightedGrid : UnweightedGrid
	{
		readonly char[][] s;
		readonly char wall;
		public char[][] Raw => s;
		public CharUnweightedGrid(char[][] s, char wall = '#') : base(s.Length, s[0].Length) { this.s = s; this.wall = wall; }
		public CharUnweightedGrid(string[] s, char wall = '#') : this(ToArrays(s), wall) { }

		public static char[][] ToArrays(string[] s) => Array.ConvertAll(s, l => l.ToCharArray());

		public (int i, int j) FindCell(char c)
		{
			for (int i = 0; i < h; ++i)
				for (int j = 0; j < w; ++j)
					if (s[i][j] == c) return (i, j);
			return (-1, -1);
		}

		public int FindVertexId(char c)
		{
			for (int i = 0; i < h; ++i)
				for (int j = 0; j < w; ++j)
					if (s[i][j] == c) return w * i + j;
			return -1;
		}

		public override List<int> GetEdges(int v) => GetNexts(v);
		public List<int> GetNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int>();
			foreach (var (di, dj) in NextsDelta)
			{
				var (ni, nj) = (i + di, j + dj);
				if (0 <= ni && ni < h && 0 <= nj && nj < w && s[ni][nj] != wall) l.Add(w * ni + nj);
			}
			return l;
		}
	}
}
