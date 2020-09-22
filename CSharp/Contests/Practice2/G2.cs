using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class G2
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

		var gs = Array.ConvertAll(new int[gi + 1], _ => new List<int>());
		for (int v = 0; v < n; v++) gs[g[v]].Add(v);

		Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		Console.WriteLine(gi);
		for (int i = gi; i > 0; i--)
			Console.WriteLine(string.Join(" ", gs[i].Prepend(gs[i].Count)));
		Console.Out.Flush();
	}
}
