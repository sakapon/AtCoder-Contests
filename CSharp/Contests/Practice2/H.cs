using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, d) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var sat = new TwoSat(n);

		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
			{
				Prop pi = i, pj = j;
				// 0: F, 1: T
				// !(x & y)
				if (Math.Abs(ps[i][0] - ps[j][0]) < d) sat.AddOr(pi, pj);
				if (Math.Abs(ps[i][0] - ps[j][1]) < d) sat.AddOr(pi, !pj);
				if (Math.Abs(ps[i][1] - ps[j][0]) < d) sat.AddOr(!pi, pj);
				if (Math.Abs(ps[i][1] - ps[j][1]) < d) sat.AddOr(!pi, !pj);
			}

		var r = sat.Execute();
		if (r == null) { Console.WriteLine("No"); return; }

		Console.WriteLine("Yes");
		Console.WriteLine(string.Join("\n", r.Select((v, i) => ps[i][v ? 1 : 0])));
	}
}

public struct Prop
{
	public int Id;
	public bool Value;
	public Prop(int id, bool value = true) { Id = id; Value = value; }
	public static implicit operator Prop(int id) => new Prop(id);
	public static Prop operator !(Prop v) => new Prop(v.Id, !v.Value);
}

public class TwoSat
{
	int n;
	List<int>[] map;
	public TwoSat(int n)
	{
		this.n = n;
		map = Array.ConvertAll(new int[2 * n], _ => new List<int>());
	}

	public void AddOr(Prop x, Prop y)
	{
		map[x.Value ? x.Id + n : x.Id].Add(y.Value ? y.Id : y.Id + n);
		map[y.Value ? y.Id + n : y.Id].Add(x.Value ? x.Id : x.Id + n);
	}

	public bool[] Execute()
	{
		var g = Scc(2 * n, map);
		var r = new bool[n];

		for (int v = 0; v < n; ++v)
		{
			if (g[v] == g[v + n]) return null;
			r[v] = g[v] < g[v + n];
		}
		return r;
	}

	// 結果のグループ ID は逆順。
	static int[] Scc(int n, List<int>[] map)
	{
		var g = new int[n];
		var order = new int[n];
		var back = new int[n];
		int gi = 0, oi = 0;
		var pend = new Stack<int>();

		Func<int, int> Dfs = null;
		Dfs = v =>
		{
			back[v] = order[v] = ++oi;
			pend.Push(v);

			foreach (var nv in map[v])
			{
				if (g[nv] != 0) continue;
				back[v] = Math.Min(back[v], back[nv] == 0 ? Dfs(nv) : back[nv]);
			}

			if (back[v] == order[v])
			{
				gi++;
				var lv = -1;
				while (lv != v && pend.Count > 0) g[lv = pend.Pop()] = gi;
			}
			return back[v];
		};
		for (int v = 0; v < n; v++) if (g[v] == 0) Dfs(v);

		return g;
	}
}
