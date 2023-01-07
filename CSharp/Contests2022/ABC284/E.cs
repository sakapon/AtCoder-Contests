using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var graph = new ListUnweightedGraph(n + 1, es, true);

		var r = 0;
		var u = new bool[n + 1];
		u[1] = true;
		DFS(1);
		return r;

		bool DFS(int v)
		{
			if (++r == 1000000) return true;
			u[v] = true;
			foreach (var nv in graph.GetEdges(v))
			{
				if (u[nv]) continue;
				if (DFS(nv)) return true;
			}
			u[v] = false;
			return false;
		}
	}
}

[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
public abstract class UnweightedGraph
{
	protected readonly int n;
	public int VertexesCount => n;
	public abstract List<int> GetEdges(int v);
	protected UnweightedGraph(int n) { this.n = n; }
}

public class ListUnweightedGraph : UnweightedGraph
{
	protected readonly List<int>[] map;
	public List<int>[] AdjacencyList => map;
	public override List<int> GetEdges(int v) => map[v];

	public ListUnweightedGraph(List<int>[] map) : base(map.Length) { this.map = map; }
	public ListUnweightedGraph(int n) : base(n)
	{
		map = Array.ConvertAll(new bool[n], _ => new List<int>());
	}
	public ListUnweightedGraph(int n, IEnumerable<(int from, int to)> edges, bool twoWay) : this(n)
	{
		foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
	}

	public void AddEdge(int from, int to, bool twoWay)
	{
		map[from].Add(to);
		if (twoWay) map[to].Add(from);
	}
}
