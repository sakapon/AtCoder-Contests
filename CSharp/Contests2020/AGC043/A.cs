using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public bool IsInRange => 0 <= i && i < h && 0 <= j && j < w;
		public P[] Nexts() => new[] { new P(i, j + 1), new P(i + 1, j) };
	}

	static int h, w;
	static string[] c;

	static void Main()
	{
		var z = Console.ReadLine().Split().Select(int.Parse).ToArray();
		h = z[0]; w = z[1];
		c = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var sp = new P();
		var gp = new P(h - 1, w - 1);
		Console.WriteLine(Search(sp, gp));
	}

	static int Search(P sp, P gp)
	{
		var u = new int[h, w];
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				u[i, j] = 1 << 30;
		var q = new Queue<P>();
		u[sp.i, sp.j] = c[sp.i][sp.j] == '.' ? 0 : 1;
		q.Enqueue(sp);

		while (q.Any())
		{
			var p = q.Dequeue();
			foreach (var x in p.Nexts())
			{
				if (!x.IsInRange) continue;
				// '#' < '.'
				var t = u[p.i, p.j] + (c[p.i][p.j] <= c[x.i][x.j] ? 0 : 1);
				if (u[x.i, x.j] <= t) continue;
				u[x.i, x.j] = t;
				q.Enqueue(x);
			}
		}
		return u[gp.i, gp.j];
	}
}
