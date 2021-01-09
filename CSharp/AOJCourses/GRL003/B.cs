using System;
using System.Collections.Generic;
using System.Linq;

class B
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

		var r = new List<Tuple<int, int>>();
		var u = new bool[n];
		var order = new int[n];
		var c = 0;

		Func<int, int, int> Dfs = null;
		Dfs = (v, v0) =>
		{
			u[v] = true;
			order[v] = c++;

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
					var b = Dfs(nv, v);
					back = Math.Min(back, b);
					if (b > order[v]) r.Add(Tuple.Create(Math.Min(v, nv), Math.Max(v, nv)));
				}
			}
			return back < order[v] ? back : max;
		};

		Dfs(0, -1);
		Console.Write(string.Join("", r.OrderBy(e => e).Select(e => $"{e.Item1} {e.Item2}\n")));
	}
}
