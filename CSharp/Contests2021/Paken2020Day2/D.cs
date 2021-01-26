using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		const int PeopleCount = 200000;
		const int AnimalsCount = 500000;

		var n = int.Parse(Console.ReadLine());
		var map = Array.ConvertAll(new int[PeopleCount + AnimalsCount + 1], _ => new List<int>());

		for (int i = 1; i <= n; i++)
		{
			Console.ReadLine();
			foreach (var x in Read()) map[PeopleCount + x].Add(i);
			Console.ReadLine();
			foreach (var x in Read()) map[i].Add(PeopleCount + x);
		}

		var g = Scc(PeopleCount + AnimalsCount + 1, map)[1..(n + 1)];
		if (g.Distinct().Count() < n) { Console.WriteLine(-1); return; }
		Console.WriteLine(string.Join(" ", g.Select((gi, i) => (gi, i: i + 1)).OrderBy(t => -t.gi).Select(t => t.i)));
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
