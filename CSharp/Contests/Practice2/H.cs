using System;
using System.Collections.Generic;

class H
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, d) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var map = Array.ConvertAll(new int[2 * n], _ => new List<int>());

		void AddConstraint(int x, bool xf, int y, bool yf)
		{
			var (a, a_) = (x, x + n);
			if (xf) (a, a_) = (a_, a);
			var (b, b_) = (y, y + n);
			if (yf) (b, b_) = (b_, b);
			map[a].Add(b_);
			map[b].Add(a_);
		}

		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
			{
				if (Math.Abs(ps[i][0] - ps[j][0]) < d) AddConstraint(i, false, j, false);
				if (Math.Abs(ps[i][0] - ps[j][1]) < d) AddConstraint(i, false, j, true);
				if (Math.Abs(ps[i][1] - ps[j][0]) < d) AddConstraint(i, true, j, false);
				if (Math.Abs(ps[i][1] - ps[j][1]) < d) AddConstraint(i, true, j, true);
			}

		var g = Scc(2 * n, map);
		var r = new int[n];

		for (int v = 0; v < n; v++)
		{
			if (g[v] == g[v + n]) { Console.WriteLine("No"); return; }
			r[v] = ps[v][g[v] < g[v + n] ? 0 : 1];
		}
		Console.WriteLine("Yes");
		Console.WriteLine(string.Join("\n", r));
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
