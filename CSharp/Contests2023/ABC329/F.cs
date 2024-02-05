using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var c = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var r = new List<int>();
		var bs = Enumerable.Range(0, n)
			.Select(i => new Dictionary<int, int> { { c[i], 1 } })
			.ToArray();

		foreach (var q in qs)
		{
			var (a, b) = q;
			a--;
			b--;

			var da = bs[a];
			var db = bs[b];

			Merge(ref db, ref da);
			da.Clear();

			bs[a] = da;
			bs[b] = db;

			r.Add(db.Count);
		}

		return string.Join("\n", r);
	}

	public static void Merge<TK>(ref Dictionary<TK, int> d1, ref Dictionary<TK, int> d2)
	{
		if (d1.Count < d2.Count) (d1, d2) = (d2, d1);
		foreach (var (k, v) in d2) d1[k] = d1.GetValueOrDefault(k) + v;
	}
}
