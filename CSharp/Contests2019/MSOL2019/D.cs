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

		var ps = Enumerable.Range(1, n).Select(i => new { i, ps = new List<int>() }).ToArray();
		for (var i = 0; i < rs.Length; i++)
		{
			ps[rs[i][0] - 1].ps.Add(rs[i][1]);
			ps[rs[i][1] - 1].ps.Add(rs[i][0]);
		}
		var sorted_ps = ps.OrderBy(_ => _.ps.Count).ToArray();

		var M = 0L;
		var d = new int[n + 1];
		var ended = 0;
		while (ended < n - 1)
			for (var i = 0; i < n; i++)
			{
				if (sorted_ps[i].ps.Count != 1) continue;

				M += (d[sorted_ps[i].i] = c[ended++]);
				if (ended < n - 1)
				{
					ps[sorted_ps[i].ps[0] - 1].ps.Remove(sorted_ps[i].i);
					sorted_ps[i].ps.RemoveAt(0);
				}
				else
				{
					d[sorted_ps[i].ps[0]] = c[n - 1];
					break;
				}
			}
		Console.WriteLine(M);
		Console.WriteLine(string.Join(" ", d.Skip(1)));
	}
}
