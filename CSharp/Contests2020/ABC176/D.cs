using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public bool IsInRange => 0 <= i && i < h && 0 <= j && j < w;
		public P[] Nexts() => new[] { new P(i, j - 1), new P(i, j + 1), new P(i - 1, j), new P(i + 1, j) };
	}

	static int h, w;

	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var z = Read();
		h = z[0]; w = z[1];
		var s = Read();
		var g = Read();
		var c = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var sp = new P(s[0] - 1, s[1] - 1);
		var gp = new P(g[0] - 1, g[1] - 1);

		var u = new int[h, w];
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				u[i, j] = c[i][j] == '.' ? -1 : 1 << 30;
		var q = new Queue<P>();
		var l = new List<P>();

		u[sp.i, sp.j] = 0;
		q.Enqueue(sp);
		l.Add(sp);

		while (q.Any())
		{
			while (q.TryDequeue(out var p))
				foreach (var x in p.Nexts())
				{
					if (!x.IsInRange || u[x.i, x.j] >= 0) continue;
					u[x.i, x.j] = u[p.i, p.j];
					q.Enqueue(x);
					l.Add(x);
				}

			var lt = l.ToArray();
			l.Clear();

			foreach (var p in lt)
				for (int i = -2; i <= 2; i++)
					for (int j = -2; j <= 2; j++)
					{
						var x = new P(p.i + i, p.j + j);
						if (!x.IsInRange || u[x.i, x.j] >= 0) continue;
						u[x.i, x.j] = u[p.i, p.j] + 1;
						q.Enqueue(x);
						l.Add(x);
					}
		}
		Console.WriteLine(u[gp.i, gp.j]);
	}
}
