using System;
using System.Collections.Generic;
using System.Linq;

class A3
{
	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public bool IsInRange => 0 <= i && i < h && 0 <= j && j < w;
		public P[] Nexts() => new[] { new P(i, j - 1), new P(i, j + 1), new P(i - 1, j), new P(i + 1, j) };
	}

	static int h, w;

	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var z = read();
		h = z[0]; w = z[1];
		var c = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var si = Array.FindIndex(c, x => x.Contains('s'));
		var sp = new P(si, c[si].IndexOf('s'));

		var u = new bool[h, w];
		var q = new Queue<P>();
		u[sp.i, sp.j] = true;
		q.Enqueue(sp);

		while (q.Any())
		{
			var p = q.Dequeue();
			foreach (var x in p.Nexts())
			{
				if (!x.IsInRange || u[x.i, x.j]) continue;
				if (c[x.i][x.j] == 'g') { Console.WriteLine("Yes"); return; }
				u[x.i, x.j] = true;
				if (c[x.i][x.j] == '.') q.Enqueue(x);
			}
		}
		Console.WriteLine("No");
	}
}
