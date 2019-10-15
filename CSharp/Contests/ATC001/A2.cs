using System;
using System.Collections.Generic;
using System.Linq;

class A2
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int h = a[0], w = a[1];
		var c = new int[h].Select(_ => Console.ReadLine()).ToArray();
		var u = new bool[h, w];
		var q = new Queue<P>();

		var si = Array.FindIndex(c, x => x.Contains('s'));
		var sp = new P(si, c[si].IndexOf('s'));
		u[sp.i, sp.j] = true;
		q.Enqueue(sp);

		Func<P, P[]> nexts = p => Array.FindAll(new[] { new P(p.i, p.j - 1), new P(p.i, p.j + 1), new P(p.i - 1, p.j), new P(p.i + 1, p.j) }, x => 0 <= x.i && x.i < h && 0 <= x.j && x.j < w && !u[x.i, x.j]);
		while (q.Any())
			foreach (var np in nexts(q.Dequeue()))
			{
				u[np.i, np.j] = true;
				if (c[np.i][np.j] == 'g') { Console.WriteLine("Yes"); return; }
				if (c[np.i][np.j] == '.') q.Enqueue(np);
			}
		Console.WriteLine("No");
	}

	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
	}
}
