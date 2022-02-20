using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var spp = new SppUnweightedGraph(n + 1);
		spp.AddEdges(es, true);
		return Enumerable.Range(1, n).Sum(sv => spp.ConnectionByDfs(sv).Count(b => b));
	}
}

public class SppUnweightedGraph
{
	static readonly int[] EmptyVertexes = new int[0];

	public int VertexesCount { get; }
	// map[v] が null である可能性があります。
	List<int>[] map;

	public SppUnweightedGraph(int n)
	{
		VertexesCount = n;
		map = new List<int>[n];
	}

	public int[][] GetMap() => Array.ConvertAll(map, l => l?.ToArray() ?? EmptyVertexes);

	public void AddEdge(int[] e, bool directed) => AddEdge(e[0], e[1], directed);
	public void AddEdge(int from, int to, bool directed)
	{
		var l = map[from] ?? (map[from] = new List<int>());
		l.Add(to);

		if (directed) return;
		l = map[to] ?? (map[to] = new List<int>());
		l.Add(from);
	}

	public void AddEdges(IEnumerable<int[]> es, bool directed)
	{
		foreach (var e in es) AddEdge(e[0], e[1], directed);
	}

	public bool[] ConnectionByDfs(int sv, int ev = -1) => ConnectionByDfs(VertexesCount, v => map[v]?.ToArray() ?? EmptyVertexes, sv, ev);

	// 終点を指定しないときは、-1 を指定します。
	public static bool[] ConnectionByDfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
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

	public static bool[] ConnectionByDfs2(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var u = new bool[n];
		u[sv] = true;
		Dfs(sv);
		return u;

		bool Dfs(int v)
		{
			foreach (var nv in nexts(v))
			{
				if (u[nv]) continue;
				u[nv] = true;
				if (nv == ev) return true;
				if (Dfs(nv)) return true;
			}
			return false;
		}
	}
}
