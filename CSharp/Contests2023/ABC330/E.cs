using System;
using System.Collections.Generic;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var cs = new int[200005];
		foreach (var x in a)
		{
			if (x < cs.Length) cs[x]++;
		}

		var set = new SortedSet<int>();
		for (int x = 0; x < cs.Length; x++)
		{
			if (cs[x] == 0) set.Add(x);
		}

		var r = new List<int>();
		foreach (var q in qs)
		{
			var i = q.Item1 - 1;
			var x = q.Item2;

			if (a[i] < cs.Length && --cs[a[i]] == 0) set.Add(a[i]);

			a[i] = x;
			if (a[i] < cs.Length && cs[a[i]]++ == 0) set.Remove(a[i]);
			r.Add(set.Min);
		}
		return string.Join("\n", r);
	}
}
