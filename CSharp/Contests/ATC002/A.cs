using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var a = read();
		int h = a[0], w = a[1];
		var s = read();
		var g = read();
		var sp = new P(s[0] - 1, s[1] - 1);
		var gp = new P(g[0] - 1, g[1] - 1);
		var c = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var t = new int[h, w];
		var q = new Queue<P>();
		t[sp.i, sp.j] = 1;
		q.Enqueue(sp);

		Func<P, P[]> nexts = p => Array.FindAll(new[] { new P(p.i, p.j - 1), new P(p.i, p.j + 1), new P(p.i - 1, p.j), new P(p.i + 1, p.j) }, x => 0 <= x.i && x.i < h && 0 <= x.j && x.j < w && t[x.i, x.j] == 0);
		while (q.Any())
		{
			var p = q.Dequeue();
			foreach (var np in nexts(p))
			{
				t[np.i, np.j] = c[np.i][np.j] == '#' ? int.MaxValue : t[p.i, p.j] + 1;
				if (np.i == gp.i && np.j == gp.j) { Console.WriteLine(t[p.i, p.j]); return; }
				if (c[np.i][np.j] == '.') q.Enqueue(np);
			}
		}
	}

	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
	}
}
