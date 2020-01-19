using System;
using System.Collections.Generic;
using System.Linq;

class J
{
	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public bool IsInRange => 0 <= i && i < h && 0 <= j && j < w;
		public P[] Nexts() => new[] { new P(i, j - 1), new P(i, j + 1), new P(i - 1, j), new P(i + 1, j) };
	}

	static int h, w;
	static int[][] a;

	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var z = read();
		h = z[0]; w = z[1];
		a = new int[h].Select(_ => read()).ToArray();

		var u1 = Search(new P(h - 1, 0));
		var u2 = Search(new P(h - 1, w - 1));
		var u3 = Search(new P(0, w - 1));
		Console.WriteLine(Enumerable.Range(0, h).SelectMany(i => Enumerable.Range(0, w).Select(j => u1[i, j] + u2[i, j] + u3[i, j] - 2 * a[i][j])).Min());
	}

	static int[,] Search(P sp)
	{
		var u = new int[h, w];
		var q = new Queue<P>();

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				u[i, j] = int.MaxValue;
		u[sp.i, sp.j] = 0;
		q.Enqueue(sp);

		while (q.Any())
		{
			var p = q.Dequeue();
			foreach (var x in p.Nexts())
			{
				if (!x.IsInRange) continue;
				var v = u[p.i, p.j] + a[x.i][x.j];
				if (v >= u[x.i, x.j]) continue;
				u[x.i, x.j] = v;
				q.Enqueue(x);
			}
		}
		return u;
	}
}
