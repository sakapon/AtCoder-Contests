using System;
using System.Collections.Generic;
using System.Text;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var sb = new StringBuilder();

		var gs = SCCToGroups(n, es);

		sb.Append(gs.Length).AppendLine();
		foreach (var g in gs)
			sb.Append(g.Count).Append(' ').AppendLine(string.Join(" ", g));
		Console.Write(sb);
	}

	public static (int gc, int[] gis) SCC(int n, int[][] es)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		var mapr = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			mapr[e[1]].Add(e[0]);
		}

		var u = new bool[n];
		var t = n;
		var vs = new int[n];
		for (int v = 0; v < n; ++v) Dfs(v);

		Array.Clear(u, 0, n);
		var gis = new int[n];
		foreach (var v in vs) if (Dfsr(v)) ++t;
		return (t, gis);

		void Dfs(int v)
		{
			if (u[v]) return;
			u[v] = true;
			foreach (var nv in map[v]) Dfs(nv);
			vs[--t] = v;
		}

		bool Dfsr(int v)
		{
			if (u[v]) return false;
			u[v] = true;
			foreach (var nv in mapr[v]) Dfsr(nv);
			gis[v] = t;
			return true;
		}
	}

	public static List<int>[] SCCToGroups(int n, int[][] es)
	{
		var (gc, gis) = SCC(n, es);
		var gs = Array.ConvertAll(new bool[gc], _ => new List<int>());
		for (int v = 0; v < n; ++v) gs[gis[v]].Add(v);
		return gs;
	}
}
