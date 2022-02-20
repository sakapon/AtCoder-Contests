using System;
using System.Collections.Generic;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var spp = new SppUnweightedGraph<int>();
		foreach (var (l, r) in es)
			spp.AddEdge(l - 1, r, false);
		return spp.Dfs(0, n).Contains(n);
	}
}

public class SppUnweightedGraph<TVertex>
{
	static readonly TVertex[] EmptyVertexes = new TVertex[0];

	public Dictionary<TVertex, List<TVertex>> Map = new Dictionary<TVertex, List<TVertex>>();

	public void AddEdge(TVertex[] e, bool directed) => AddEdge(e[0], e[1], directed);
	public void AddEdge(TVertex from, TVertex to, bool directed)
	{
		var l = Map.ContainsKey(from) ? Map[from] : Map[from] = new List<TVertex>();
		l.Add(to);

		if (directed) return;
		l = Map.ContainsKey(to) ? Map[to] : Map[to] = new List<TVertex>();
		l.Add(from);
	}

	public void AddEdges(IEnumerable<TVertex[]> es, bool directed)
	{
		foreach (var e in es) AddEdge(e[0], e[1], directed);
	}

	public HashSet<TVertex> Dfs(TVertex sv, TVertex ev) => Dfs(v => Map.ContainsKey(v) ? Map[v].ToArray() : EmptyVertexes, sv, ev);

	// 終点を指定しないときは、ev に null, -1 などを指定します。
	public static HashSet<TVertex> Dfs(Func<TVertex, TVertex[]> nexts, TVertex sv, TVertex ev)
	{
		var comp = EqualityComparer<TVertex>.Default;
		var u = new HashSet<TVertex>();
		var q = new Stack<TVertex>();
		u.Add(sv);
		q.Push(sv);

		while (q.Count > 0)
		{
			var v = q.Pop();

			foreach (var nv in nexts(v))
			{
				if (u.Contains(nv)) continue;
				u.Add(nv);
				if (comp.Equals(nv, ev)) return u;
				q.Push(nv);
			}
		}
		return u;
	}

	public static HashSet<TVertex> Dfs2(Func<TVertex, TVertex[]> nexts, TVertex sv, TVertex ev)
	{
		var comp = EqualityComparer<TVertex>.Default;
		var u = new HashSet<TVertex>();
		u.Add(sv);
		_Dfs(sv);
		return u;

		bool _Dfs(TVertex v)
		{
			foreach (var nv in nexts(v))
			{
				if (u.Contains(nv)) continue;
				u.Add(nv);
				if (comp.Equals(nv, ev)) return true;
				if (_Dfs(nv)) return true;
			}
			return false;
		}
	}
}
