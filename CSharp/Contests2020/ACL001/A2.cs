using System;
using System.Collections.Generic;
using System.Linq;

class A2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var l = new List<(int s, int e)>();
		foreach (var p in ps.OrderBy(p => p[0]))
		{
			if (!l.Any()) { l.Add((p[1], p[1])); continue; }

			var (s, e) = l.Last();
			if (p[1] < s)
			{
				l.Add((p[1], p[1]));
			}
			else
			{
				l.RemoveAt(l.Count - 1);
				l.Add((s, Math.Max(e, p[1])));
			}
		}
		l.Reverse();

		var l2 = new List<(int s, int e)>();
		foreach (var (s1, e1) in l)
		{
			if (!l2.Any()) { l2.Add((s1, e1)); continue; }

			var (s, e) = l2.Last();
			if (e < s1)
			{
				l2.Add((s1, e1));
			}
			else
			{
				l2.RemoveAt(l2.Count - 1);
				l2.Add((s, Math.Max(e, e1)));
			}
		}

		var g = new int[n + 1];
		foreach (var (s, e) in l2)
			for (int i = s; i <= e; i++)
				g[i] = e - s + 1;

		Console.WriteLine(string.Join("\n", ps.Select(p => g[p[1]])));
	}
}
