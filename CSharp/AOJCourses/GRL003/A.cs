using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var max = 1 << 30;
		var h = Read();
		var n = h[0];
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		var map = Array.ConvertAll(new int[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}

		var r = new bool[n];
		var u = new bool[n];
		var order = new int[n];
		var c = 0;

		Func<int, int, int> Dfs = null;
		Dfs = (v, v0) =>
		{
			u[v] = true;
			order[v] = c++;

			var childs = 0;
			var back = max;
			foreach (var nv in map[v])
			{
				if (nv == v0) continue;
				if (u[nv])
				{
					back = Math.Min(back, order[nv]);
				}
				else
				{
					childs++;
					var b = Dfs(nv, v);
					back = Math.Min(back, b);
					if (b >= order[v]) r[v] = true;
				}
			}
			if (v == 0) r[v] = childs > 1;
			return back < order[v] ? back : max;
		};

		Dfs(0, -1);
		Console.Write(string.Join("", Enumerable.Range(0, n).Where(v => r[v]).Select(v => $"{v}\n")));
	}
}
