using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public bool IsInRange => 0 <= i && i < h && 0 <= j && j < w;
		public P[] Nexts() => new[] { new P(i, j - 1), new P(i, j + 1), new P(i - 1, j), new P(i + 1, j), new P(i - 1, j + 1), new P(i + 1, j + 1) };
	}

	static int h = 403, w = 403;
	static bool[,] c;

	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var z = Read();
		int n = z[0], x = z[1], y = z[2];

		c = new bool[h, w];
		for (int i = 0; i < n; i++)
		{
			var p = Read();
			c[p[0] + 201, p[1] + 201] = true;
		}

		Console.WriteLine(Search(new P(201, 201), new P(x + 201, y + 201)));
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
				if (!x.IsInRange || u[x.i, x.j] > 0 || c[x.i, x.j]) continue;
				u[x.i, x.j] = u[p.i, p.j] + 1;
				q.Enqueue(x);
			}
		}
		return u[gp.i, gp.j] - 1;
	}
}
