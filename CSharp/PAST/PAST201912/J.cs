using System;
using System.Collections.Generic;
using System.Linq;

class J
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var z = read();
		int h = z[0], w = z[1];
		var a = new int[h].Select(_ => read()).ToArray();

		var u1 = GetCost(h, w, a, new P(h - 1, 0));
		var u2 = GetCost(h, w, a, new P(h - 1, w - 1));
		var u3 = GetCost(h, w, a, new P(0, w - 1));
		Console.WriteLine(Enumerable.Range(0, h).SelectMany(i => Enumerable.Range(0, w).Select(j => u1[i, j] + u2[i, j] + u3[i, j] - 2 * a[i][j])).Min());
	}

	static int[,] GetCost(int h, int w, int[][] a, P p0)
	{
		var u = new int[h, w];
		var q = new Queue<P>();

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				u[i, j] = int.MaxValue;
		u[p0.i, p0.j] = 0;
		q.Enqueue(p0);

		Func<P, P[]> nexts = p => Array.FindAll(new[] { new P(p.i, p.j - 1), new P(p.i, p.j + 1), new P(p.i - 1, p.j), new P(p.i + 1, p.j) }, x => 0 <= x.i && x.i < h && 0 <= x.j && x.j < w);
		while (q.Any())
		{
			var p = q.Dequeue();
			foreach (var np in nexts(p))
			{
				var v = u[p.i, p.j] + a[np.i][np.j];
				if (v >= u[np.i, np.j]) continue;
				u[np.i, np.j] = v;
				q.Enqueue(np);
			}
		}
		return u;
	}

	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
	}
}
