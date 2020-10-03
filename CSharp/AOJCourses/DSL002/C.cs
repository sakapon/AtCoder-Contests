using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });

		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Read()).Select((p, i) => new P(i, p[0], p[1])).ToArray();
		var k = int.Parse(Console.ReadLine());
		var qs = new int[k].Select(_ => Read()).ToArray();

		var maxCount = 500;
		var ls = new List<PGroup> { new PGroup { xMin = -1 << 31, ps = Enumerable.Repeat(new P(-1, -1 << 31, -1 << 31), maxCount).ToList() } };

		foreach (var g in ps.GroupBy(p => p.i).OrderBy(g => g.Key))
		{
			var gps = g.ToArray();

			if (ls.Last().ps.Count + gps.Length > maxCount)
			{
				ls.Last().xMax = ls.Last().ps.Last().i;
				ls.Last().ps = ls.Last().ps.OrderBy(p => p.j).ToList();

				ls.Add(new PGroup { xMin = g.Key, ps = new List<P>() });
			}

			ls.Last().ps.AddRange(gps);
		}
		ls.Last().xMax = ls.Last().ps.Last().i;
		ls.Last().ps = ls.Last().ps.OrderBy(p => p.j).ToList();

		foreach (var q in qs)
		{
			var r = new List<int>();

			var si = First(0, ls.Count, li => q[0] <= ls[li].xMax);

			for (int i = si; i < ls.Count && ls[i].xMin <= q[1]; i++)
			{
				var pg = ls[i];
				var sj = First(0, pg.ps.Count, lj => q[2] <= pg.ps[lj].j);

				for (int j = sj; j < pg.ps.Count && pg.ps[j].j <= q[3]; j++)
				{
					var p = pg.ps[j];
					if (q[0] <= p.i && p.i <= q[1])
						r.Add(p.id);
				}
			}

			r.Sort();
			foreach (var id in r) Console.WriteLine(id);
			Console.WriteLine();
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

	class PGroup
	{
		public int xMin;
		public int xMax;
		public List<P> ps;
	}
}
