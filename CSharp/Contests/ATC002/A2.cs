using System;
using System.Collections.Generic;
using System.Linq;

class A2
{
	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public bool IsInRange => 0 <= i && i < h && 0 <= j && j < w;
		public P[] Nexts() => new[] { new P(i, j - 1), new P(i, j + 1), new P(i - 1, j), new P(i + 1, j) };
	}

	static int h, w;
	static string[] c;

	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var z = read();
		h = z[0]; w = z[1];
		var s = read();
		var g = read();
		c = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var sp = new P(s[0] - 1, s[1] - 1);
		var gp = new P(g[0] - 1, g[1] - 1);
		Console.WriteLine(Search(sp, gp));
	}

	static int Search(P sp, P gp)
	{
		var u = new int[h, w];
		var q = new Queue<P>();
		u[sp.i, sp.j] = 1;
		q.Enqueue(sp);

		while (q.Any())
		{
			var p = q.Dequeue();
			foreach (var x in p.Nexts())
			{
				if (!x.IsInRange || u[x.i, x.j] > 0 || c[x.i][x.j] == '#') continue;
				u[x.i, x.j] = u[p.i, p.j] + 1;
				q.Enqueue(x);
			}
		}
		return u[gp.i, gp.j] - 1;
	}
}
