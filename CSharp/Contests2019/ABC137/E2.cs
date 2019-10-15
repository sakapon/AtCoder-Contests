using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var n = h[0];
		var rs = new int[h[1]].Select(_ => read()).Select(r => { r[2] -= h[2]; return r; }).ToArray();
		var map = rs.ToLookup(r => r[0]);

		var cs = Enumerable.Repeat(long.MinValue, n + 1).ToArray();
		cs[1] = 0;
		for (long c, i = 0; i < n; i++)
			foreach (var r in rs)
				if (cs[r[0]] != long.MinValue && cs[r[1]] < (c = cs[r[0]] + r[2])) cs[r[1]] = c;

		var u = new bool[n + 1];
		var q = new Stack<int>();
		foreach (var r in rs)
		{
			if (cs[r[0]] == cs[0] || cs[r[1]] >= cs[r[0]] + r[2]) continue;
			u[r[1]] = true;
			q.Push(r[1]);
			while (q.Any())
				foreach (var np in map[q.Pop()])
				{
					if (u[np[1]]) continue;
					u[np[1]] = true;
					q.Push(np[1]);
				}
		}
		Console.WriteLine(u[n] ? -1 : Math.Max(cs[n], 0));
	}
}
