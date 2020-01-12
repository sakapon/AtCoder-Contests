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
	static string[] c;

	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var z = read();
		h = z[0]; w = z[1];
		c = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var M = 0;
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				if (c[i][j] == '.')
					M = Math.Max(M, CountMax(new P(i, j)));
		Console.WriteLine(M);
	}

	static int CountMax(P sp)
	{
		var u = new int[h, w];
		var q = new Queue<P>();
		u[sp.i, sp.j] = 1;
		q.Enqueue(sp);

		var M = 0;
		while (q.Any())
		{
			var p = q.Dequeue();
			foreach (var x in p.Nexts())
			{
				if (!x.IsInRange || u[x.i, x.j] > 0) continue;
				if (c[x.i][x.j] == '#') { u[x.i, x.j] = int.MaxValue; continue; }
				u[x.i, x.j] = u[p.i, p.j] + 1;
				M = Math.Max(M, u[x.i, x.j]);
				q.Enqueue(x);
			}
		}
		return M - 1;
	}
}
