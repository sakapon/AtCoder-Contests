using System;
using System.Collections.Generic;

class Q061
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var r = new List<int>();
		var l1 = new List<int>();
		var l2 = new List<int>();

		foreach (var (t, x) in qs)
		{
			if (t == 1)
			{
				l1.Add(x);
			}
			else if (t == 2)
			{
				l2.Add(x);
			}
			else
			{
				r.Add(x <= l1.Count ? l1[l1.Count - x] : l2[x - l1.Count - 1]);
			}
		}
		return string.Join("\n", r);
	}
}
