using System;
using System.Collections.Generic;
using CoderLib8.Collections.Dynamics.Int;

class ES
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		// a の値をキャッシュします。
		var q = Array.ConvertAll(new bool[m + 1], _ => new List<int>());
		for (int i = 0; i < n; i++)
		{
			var (a, b) = ps[i];
			q[a].Add(-1);
			q[b].Add(a);
		}

		var set = new IntSegmentMultiSet(m + 1);
		var raq = new StaticRAQ1(m);

		for (int j = 1; j <= m; j++)
		{
			foreach (var a in q[j])
				if (a != -1) set.Add(a, -1);
			set.Add(j, q[j].Count);

			if (set.Count < n) continue;
			raq.Add(j - set.GetAt(0), j, 1);
		}

		var sum = raq.GetSum();
		return string.Join(" ", sum);
	}
}
