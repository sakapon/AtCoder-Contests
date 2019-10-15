using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var n = int.Parse(Console.ReadLine());
		var rs = new int[n - 1].Select(_ => read()).ToArray();
		var c = read(); Array.Sort(c);

		var ps = Enumerable.Range(0, n + 1).Select(i => new { i, ps = new List<int>() }).ToArray();
		foreach (var r in rs)
		{
			ps[r[0]].ps.Add(r[1]);
			ps[r[1]].ps.Add(r[0]);
		}
		var sorted_ps = ps.OrderBy(_ => _.ps.Count).ToArray();

		var M = 0L;
		var d = new int[n + 1];
		var ended = 0;
		while (ended < n - 1)
			foreach (var p in sorted_ps)
			{
				if (p.ps.Count != 1) continue;
				M += d[p.i] = c[ended++];

				if (ended < n - 1)
				{
					ps[p.ps[0]].ps.Remove(p.i);
					p.ps.RemoveAt(0);
				}
				else
				{
					d[p.ps[0]] = c[n - 1];
					break;
				}
			}
		Console.WriteLine(M);
		Console.WriteLine(string.Join(" ", d.Skip(1)));
	}
}
