using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int h = a[0], w = a[1];
		var c = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var si = Array.FindIndex(c, x => x.Contains('s'));
		var sp = new P(si, c[si].IndexOf('s'));

		var used = new bool[h, w];
		var q = new Queue<P>();
		used[sp.i, sp.j] = true;
		q.Enqueue(sp);

		while (q.Any())
			foreach (var np in Nexts(q.Dequeue(), h, w))
			{
				if (used[np.i, np.j]) continue;
				used[np.i, np.j] = true;
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

	static IEnumerable<P> Nexts(P p, int h, int w)
	{
		if (p.i > 0) yield return new P(p.i - 1, p.j);
		if (p.i < h - 1) yield return new P(p.i + 1, p.j);
		if (p.j > 0) yield return new P(p.i, p.j - 1);
		if (p.j < w - 1) yield return new P(p.i, p.j + 1);
	}
}
