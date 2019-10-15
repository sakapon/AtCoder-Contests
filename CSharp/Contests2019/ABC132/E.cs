using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var map = new int[h[1]].Select(_ => read()).ToLookup(r => r[0], r => r[1]);
		var st = read();

		var u = new bool[h[0] + 1, 3];
		var q = new List<int>();
		u[st[0], 0] = true;
		q.Add(st[0]);

		for (int i = 1; q.Any(); i++)
		{
			var ps = q.ToArray();
			q.Clear();

			var m = i % 3;
			foreach (var p in ps)
				foreach (var np in map[p])
				{
					if (u[np, m]) continue;
					if (np == st[1] && m == 0) { Console.WriteLine(i / 3); return; }
					u[np, m] = true;
					q.Add(np);
				}
		}
		Console.WriteLine(-1);
	}
}
