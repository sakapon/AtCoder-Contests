using System;
using System.Collections.Generic;
using System.Linq;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int l, int r) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var segs = new List<(int l, int r)>();
		var q = new Stack<int>();

		foreach (var (v, close) in ps.SelectMany(p => new[] { (p.l, false), (p.r, true) }).OrderBy(p => p))
		{
			if (!close)
			{
				q.Push(v);
			}
			else
			{
				var l = q.Pop();
				if (q.Count == 0) segs.Add((l, v));
			}
		}
		return string.Join("\n", segs.Select(p => $"{p.l} {p.r}"));
	}
}
