using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var p = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read()[1..]);

		// 子の数
		var c = new int[n + 1];
		foreach (var v in p) c[v]++;

		return string.Join("\n", qs.Select(Query));

		long Query(int[] q)
		{
			var set = q.ToHashSet();
			var r = 0L;

			foreach (var v in q)
			{
				r += c[v];
				r += v != 1 && set.Contains(p[v - 2]) ? -1 : 1;
			}
			return r;
		}
	}
}
