using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public bool IsInRange => 0 <= i && i < h && 0 <= j && j < w;
		public P[] Nexts() => new[] { new P(i, j - 1), new P(i, j + 1), new P(i - 1, j), new P(i + 1, j) };
	}
	static int h, w;
	static string[] s;
	static List<P>[] map;

	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var z = Read();
		h = z[0];
		w = z[1];
		s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		map = Array.ConvertAll(new bool[26], _ => new List<P>());

		P sp = default;
		P gp = default;
		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == 'S') sp = new P(i, j);
				if (s[i][j] == 'G') gp = new P(i, j);
				if (s[i][j] >= 'a' && s[i][j] <= 'z') map[s[i][j] - 'a'].Add(new P(i, j));
			}
		}

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
				if (!x.IsInRange || u[x.i, x.j] > 0 || s[x.i][x.j] == '#') continue;
				u[x.i, x.j] = u[p.i, p.j] + 1;
				q.Enqueue(x);
			}
			if (s[p.i][p.j] >= 'a' && s[p.i][p.j] <= 'z')
			{
				foreach (var x in map[s[p.i][p.j] - 'a'])
				{
					if (!x.IsInRange || u[x.i, x.j] > 0 || s[x.i][x.j] == '#') continue;
					u[x.i, x.j] = u[p.i, p.j] + 1;
					q.Enqueue(x);
				}
				map[s[p.i][p.j] - 'a'].Clear();
			}
		}
		return u[gp.i, gp.j] - 1;
	}
}
