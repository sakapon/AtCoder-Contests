using System;
using System.Collections.Generic;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var spp = new SppUnweightedGraph(n + 1);
		foreach (var (l, r) in es)
			spp.AddEdge(l - 1, r, false);
		return spp.Dfs(0, n)[n];
	}
}

public class SppUnweightedGraph
{
	static readonly int[] EmptyVertexes = new int[0];

	public int VertexesCount { get; }
	// Map[v] が null である可能性があります。
	List<int>[] Map;

	public SppUnweightedGraph(int n)
	{
		VertexesCount = n;
		Map = new List<int>[n];
	}

	public int[][] GetMap() => Array.ConvertAll(Map, l => l?.ToArray() ?? EmptyVertexes);

	public void AddEdge(int[] e, bool directed) => AddEdge(e[0], e[1], directed);
	public void AddEdge(int from, int to, bool directed)
	{
		if (Map[from] == null) Map[from] = new List<int>();
		Map[from].Add(to);

		if (directed) return;
		if (Map[to] == null) Map[to] = new List<int>();
		Map[to].Add(from);
	}

	public void AddEdges(IEnumerable<int[]> es, bool directed)
	{
		foreach (var e in es) AddEdge(e[0], e[1], directed);
	}

	public bool[] Dfs(int sv, int ev = -1) => Dfs(VertexesCount, v => Map[v]?.ToArray() ?? EmptyVertexes, sv, ev);

	// 終点を指定しないときは、-1 を指定します。
	public static bool[] Dfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var u = new bool[n];
		var q = new Stack<int>();
		u[sv] = true;
		q.Push(sv);

		while (q.Count > 0)
		{
			var v = q.Pop();

			foreach (var nv in nexts(v))
			{
				if (u[nv]) continue;
				u[nv] = true;
				if (nv == ev) return u;
				q.Push(nv);
			}
		}
		return u;
	}

	public static bool[] Dfs2(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var u = new bool[n];
		u[sv] = true;
		_Dfs(sv);
		return u;

		bool _Dfs(int v)
		{
			foreach (var nv in nexts(v))
			{
				if (u[nv]) continue;
				u[nv] = true;
				if (nv == ev) return true;
				if (_Dfs(nv)) return true;
			}
			return false;
		}
	}
}
