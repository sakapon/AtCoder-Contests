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
		var ps0 = new int[n].Select(_ => Read()).ToArray();
		var k = int.Parse(Console.ReadLine());
		var qs = new int[k].Select(_ => Read()).ToArray();

		var xs = new List<int>();
		var ys = new List<int>();
		foreach (var p in ps0)
		{
			xs.Add(p[0]);
			ys.Add(p[1]);
		}
		foreach (var q in qs)
		{
			xs.Add(q[0]);
			xs.Add(q[1]);
			ys.Add(q[2]);
			ys.Add(q[3]);
		}
		xs = xs.Distinct().OrderBy(x => x).ToList();
		ys = ys.Distinct().OrderBy(y => y).ToList();
		var xMap = Enumerable.Range(0, xs.Count).ToDictionary(i => xs[i]);
		var yMap = Enumerable.Range(0, ys.Count).ToDictionary(i => ys[i]);
		var ps = Array.ConvertAll(ps0, p => new P(xMap[p[0]], yMap[p[1]]));

		var size = (int)Math.Ceiling(Math.Sqrt(Math.Max(xs.Count, ys.Count)));
		var parts = Array.ConvertAll(new int[size * size], _ => new List<int>());
		Func<int, int, int> ToPartId = (x, y) => x * size + y;

		for (int i = 0; i < ps.Length; i++)
		{
			var p = ps[i];
			int x = p.i / size, y = p.j / size;
			parts[ToPartId(x, y)].Add(i);
		}

		foreach (var q in qs)
		{
			var sp = new P(xMap[q[0]], yMap[q[2]]);
			var gp = new P(xMap[q[1]], yMap[q[3]]);

			int sx = sp.i / size, tx = gp.i / size;
			int sy = sp.j / size, ty = gp.j / size;

			var r = new List<int>();
			for (int i = sx; i <= tx; i++)
				for (int j = sy; j <= ty; j++)
					foreach (var id in parts[ToPartId(i, j)])
					{
						var p = ps[id];
						if (sp.i <= p.i && p.i <= gp.i && sp.j <= p.j && p.j <= gp.j)
							r.Add(id);
					}
			r.Sort();
			foreach (var id in r) Console.WriteLine(id);
			Console.WriteLine();
		}

		Console.Out.Flush();
	}

	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
	}
}
