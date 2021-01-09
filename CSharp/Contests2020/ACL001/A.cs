using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var q = new Stack<(int s, int e)>();
		foreach (var p in ps.OrderBy(p => p[0]))
		{
			int s = p[1], e = s;
			while (q.TryPeek(out var qp) && qp.s < p[1])
			{
				q.Pop();
				s = Math.Min(s, qp.s);
				e = Math.Max(e, qp.e);
			}
			q.Push((s, e));
		}

		var g = new int[n + 1];
		foreach (var (s, e) in q)
			for (int i = s; i <= e; i++)
				g[i] = e - s + 1;

		Console.WriteLine(string.Join("\n", ps.Select(p => g[p[1]])));
	}
}
