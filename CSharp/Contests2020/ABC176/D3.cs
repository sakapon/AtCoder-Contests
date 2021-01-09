using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.Graphs;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int i, int j) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		// 1-indexed に注意
		var sv = Read2();
		var ev = Read2();
		var s = GridHelper.ReadEnclosedGrid(ref h, ref w);

		sv = (sv.i + 1, sv.j + 1);
		ev = (ev.i + 1, ev.j + 1);
		GridHelper.EncloseGrid(ref h, ref w, ref s);

		const char Wall = '#';
		var idMap = new int[h, w];
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				idMap[i, j] = i * w + j;

		var uf = new UF(h * w);

		for (int i = 2; i < h - 2; i++)
			for (int j = 2; j < w - 2; j++)
			{
				if (s[i][j] == Wall) continue;

				if (s[i - 1][j] != Wall) uf.Unite(idMap[i, j], idMap[i - 1, j]);
				if (s[i][j - 1] != Wall) uf.Unite(idMap[i, j], idMap[i, j - 1]);
			}

		var es = new List<(int v1, int v2)>();
		var sv2 = uf.GetRoot(idMap[sv.i, sv.j]);
		var ev2 = uf.GetRoot(idMap[ev.i, ev.j]);

		for (int i = 2; i < h - 2; i++)
			for (int j = 2; j < w - 2; j++)
			{
				if (s[i][j] == Wall) continue;

				var id = idMap[i, j];

				for (int di = -2; di <= 0; di++)
					for (int dj = -2; dj <= 2; dj++)
					{
						if (s[i + di][j + dj] == Wall) continue;

						var did = idMap[i + di, j + dj];
						if (uf.AreUnited(id, did)) continue;

						var (v1, v2) = (uf.GetRoot(id), uf.GetRoot(did));
						if (v1 > v2) (v1, v2) = (v2, v1);
						es.Add((v1, v2));
					}
			}

		var r = ShortestPath.Bfs(h * w, es.Distinct().Select(e => new[] { e.v1, e.v2 }).ToArray(), false, sv2, ev2);
		Console.WriteLine(r.IsConnected(ev2) ? r[ev2] : -1);
	}
}

class UF
{
	int[] p, sizes;
	public int GroupsCount;
	public UF(int n)
	{
		p = Enumerable.Range(0, n).ToArray();
		sizes = Array.ConvertAll(p, _ => 1);
		GroupsCount = n;
	}

	public int GetRoot(int x) => p[x] == x ? x : p[x] = GetRoot(p[x]);
	public int GetSize(int x) => sizes[GetRoot(x)];

	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public bool Unite(int x, int y)
	{
		if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

		// 要素数が大きいほうのグループにマージします。
		if (sizes[x] < sizes[y]) Merge(y, x);
		else Merge(x, y);
		return true;
	}
	protected virtual void Merge(int x, int y)
	{
		p[y] = x;
		sizes[x] += sizes[y];
		--GroupsCount;
	}
	public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
}

namespace AlgorithmLab.Graphs
{
	public static class GridHelper
	{
		const char Road = '.';
		const char Wall = '#';

		// 2 次元配列に 2 次元インデックスでアクセスします。
		public static T GetByP<T>(this T[][] a, (int i, int j) p) => a[p.i][p.j];
		public static void SetByP<T>(this T[][] a, (int i, int j) p, T value) => a[p.i][p.j] = value;
		public static char GetByP(this string[] s, (int i, int j) p) => s[p.i][p.j];

		public static string[] ReadEnclosedGrid(ref int h, ref int w, char c = Wall)
		{
			var s = new string[h + 2];
			s[h + 1] = s[0] = new string(c, w += 2);
			for (int i = 1; i <= h; ++i) s[i] = c + Console.ReadLine() + c;
			h += 2;
			return s;
		}

		public static void EncloseGrid(ref int h, ref int w, ref string[] s, char c = Wall)
		{
			var t = new string[h + 2];
			t[h + 1] = t[0] = new string(c, w += 2);
			for (int i = 1; i <= h; ++i) t[i] = c + s[i - 1] + c;
			h += 2;
			s = t;
		}

		public static (int i, int j) FindChar(string[] s, char c)
		{
			var (h, w) = (s.Length, s[0].Length);
			for (int i = 0; i < h; ++i)
				for (int j = 0; j < w; ++j)
					if (s[i][j] == c) return (i, j);
			return (-1, -1);
		}

		public static int ToId((int i, int j) p, int w) => p.i * w + p.j;
		public static (int i, int j) FromId(int id, int w) => (id / w, id % w);

		public static readonly (int i, int j)[] NextsByDelta = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
		public static (int i, int j)[] Nexts((int i, int j) v)
		{
			var (i, j) = v;
			return new[] { (i - 1, j), (i + 1, j), (i, j - 1), (i, j + 1) };
		}
	}

	/// <summary>
	/// 最短経路を求めるための静的メソッドを提供します。
	/// </summary>
	public static class ShortestPath
	{
		public static UnweightedResult Bfs(int vertexesCount, int[][] edges, bool directed, int startVertexId, int endVertexId = -1)
		{
			var map = UnweightedEdgesToMap(vertexesCount, edges, directed);
			return ShortestPathCore.Bfs(vertexesCount, v => map[v], startVertexId, endVertexId);
		}

		#region Adjacent List

		public static List<int>[] UnweightedEdgesToMap(int vertexesCount, int[][] edges, bool directed)
		{
			var map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int>());
			foreach (var e in edges)
			{
				// 入力チェックは省略。
				map[e[0]].Add(e[1]);
				if (!directed) map[e[1]].Add(e[0]);
			}
			return map;
		}

		public static List<int[]>[] WeightedEdgesToMap(int vertexesCount, int[][] edges, bool directed)
		{
			var map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int[]>());
			foreach (var e in edges)
			{
				// 入力チェックは省略。
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}
			return map;
		}
		#endregion
	}

	/// <summary>
	/// 最短経路アルゴリズムの核となる機能を提供します。
	/// ここでは整数型の ID を使用します。
	/// 整数型以外の ID を使用するには、<see cref="ShortestPath"/> クラスを呼び出します。
	/// </summary>
	public static class ShortestPathCore
	{
		/// <summary>
		/// 幅優先探索により、始点から各頂点への最短経路を求めます。
		/// </summary>
		/// <param name="vertexesCount">頂点の個数。これ未満の値を ID として使用できます。</param>
		/// <param name="getNextVertexes">指定された頂点からの行先を取得するための関数。</param>
		/// <param name="startVertexId">始点の ID。</param>
		/// <param name="endVertexId">終点の ID。終点を指定しない場合、-1。</param>
		/// <returns>頂点ごとの最小コスト。到達不可能の場合、<see cref="long.MaxValue"/>。</returns>
		/// <remarks>
		/// グラフの有向性、連結性、多重性、開閉を問いません。したがって、1-indexed でも利用できます。<br/>
		/// 辺のコストはすべて 1 です。
		/// </remarks>
		public static UnweightedResult Bfs(int vertexesCount, Func<int, IEnumerable<int>> getNextVertexes, int startVertexId, int endVertexId = -1)
		{
			var costs = Array.ConvertAll(new bool[vertexesCount], _ => long.MaxValue);
			var inVertexs = Array.ConvertAll(new bool[vertexesCount], _ => -1);
			var q = new Queue<int>();
			costs[startVertexId] = 0;
			q.Enqueue(startVertexId);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var nc = costs[v] + 1;

				foreach (var nv in getNextVertexes(v))
				{
					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					inVertexs[nv] = v;
					if (nv == endVertexId) return new UnweightedResult(costs, inVertexs);
					q.Enqueue(nv);
				}
			}
			return new UnweightedResult(costs, inVertexs);
		}
	}

	public class UnweightedResult
	{
		public long[] RawCosts { get; }
		public int[] RawInVertexes { get; }
		public long this[int vertexId] => RawCosts[vertexId];
		public bool IsConnected(int vertexId) => RawCosts[vertexId] != long.MaxValue;

		public UnweightedResult(long[] costs, int[] inVertexes)
		{
			RawCosts = costs;
			RawInVertexes = inVertexes;
		}

		public int[] GetPathVertexes(int endVertexId)
		{
			var path = new Stack<int>();
			for (var v = endVertexId; v != -1; v = RawInVertexes[v])
				path.Push(v);
			return path.ToArray();
		}
	}
}
