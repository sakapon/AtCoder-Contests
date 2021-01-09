using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		var map = Array.ConvertAll(new int[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
		}

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
				while (lv != v && pend.Any()) g[lv = pend.Pop()] = gi;
			}
			return back[v];
		};
		for (int v = 0; v < n; v++) if (g[v] == 0) Dfs(v);

		var k = int.Parse(Console.ReadLine());
		Console.WriteLine(string.Join("\n", new int[k].Select(_ => Read()).Select(r => g[r[0]] == g[r[1]] ? 1 : 0)));
	}
}
