using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });

		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Read()).Select((p, i) => new P(i, p[0], p[1])).GroupBy(p => p.i).OrderBy(g => g.Key).Select(g => g.OrderBy(p => p.j).ToArray()).ToArray();

		var r = new List<int>();
		var qc = int.Parse(Console.ReadLine());
		for (int k = 0; k < qc; k++)
		{
			var q = Read();

			var si = First(0, ps.Length, i => q[0] <= ps[i][0].i);
			for (int i = si; i < ps.Length && ps[i][0].i <= q[1]; i++)
			{
				var pg = ps[i];
				var sj = First(0, pg.Length, j => q[2] <= pg[j].j);
				for (int j = sj; j < pg.Length && pg[j].j <= q[3]; j++)
					r.Add(pg[j].id);
			}

			r.Sort();
			foreach (var id in r) Console.WriteLine(id);
			Console.WriteLine();
			r.Clear();
		}

		Console.Out.Flush();
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}

	struct P
	{
		public int id, i, j;
		public P(int _id, int _i, int _j) { id = _id; i = _i; j = _j; }
	}
}
